using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Naidis_IKTpv25_WindowsForm

{
    public partial class PictureViewerForm : Form
    {
        PictureBox pictureBox;
        Panel picturePanel;
        CheckBox stretchCheckBox, drawCheckBox;
        Label zoomLabel, fileNameLabel, imageSizeLabel, fileSizeLabel;

        Image originalImage;
        float zoom = 1;
        bool drawing;
        Point lastPoint;

        public PictureViewerForm()
        {
            Text = "Pildi vaatamine";
            Size = new Size(800, 650);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.WhiteSmoke;
            AutoScroll = true;
            AutoScrollMinSize = new Size(0, 850);

            Label title = new Label();
            title.Text = "Pildi vaatamine";
            title.Font = new Font("Arial", 18, FontStyle.Bold);
            title.Location = new Point(20, 15);
            title.AutoSize = true;
            Controls.Add(title);

            // Pildi ala
            picturePanel = new Panel();
            picturePanel.Location = new Point(20, 55);
            picturePanel.Size = new Size(740, 390);
            picturePanel.BorderStyle = BorderStyle.FixedSingle;
            picturePanel.BackColor = Color.White;
            picturePanel.AutoScroll = true;

            pictureBox = new PictureBox();
            pictureBox.Size = new Size(735, 385);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += (s, e) => drawing = false;

            picturePanel.Controls.Add(pictureBox);
            Controls.Add(picturePanel);

            // Esimene rida
            AddButton("Ava pilt", 20, 470, Color.LightBlue, OpenImage);
            AddButton("Puhasta", 130, 470, Color.LightGray, ClearImage);
            AddButton("Taustavärv", 240, 470, Color.LightYellow, ChangeColor);

            stretchCheckBox = new CheckBox();
            stretchCheckBox.Text = "Venita pilti";
            stretchCheckBox.Location = new Point(360, 478);
            stretchCheckBox.AutoSize = true;
            stretchCheckBox.CheckedChanged += (s, e) =>
                pictureBox.SizeMode = stretchCheckBox.Checked
                ? PictureBoxSizeMode.StretchImage
                : PictureBoxSizeMode.Zoom;
            Controls.Add(stretchCheckBox);

            // Pööramine ja suum
            AddButton("← Pööra", 20, 525, Color.LightCyan, RotateLeft);
            AddButton("Pööra →", 130, 525, Color.LightCyan, RotateRight);
            AddButton("Suumi +", 260, 525, Color.LightGreen, ZoomIn);
            AddButton("Suumi -", 370, 525, Color.LightGreen, ZoomOut);

            zoomLabel = AddLabel("100%", 480, 535);

            drawCheckBox = new CheckBox();
            drawCheckBox.Text = "Joonista";
            drawCheckBox.Location = new Point(550, 533);
            drawCheckBox.AutoSize = true;
            Controls.Add(drawCheckBox);

            // Filtrid
            Label filterTitle = AddLabel("Filtrid:", 20, 585);
            filterTitle.Font = new Font("Arial", 10, FontStyle.Bold);

            AddButton("Must-valge", 20, 610, Color.LightGray, GrayFilter);
            AddButton("Punane", 130, 610, Color.LightCoral, RedFilter);
            AddButton("Sinine", 240, 610, Color.LightBlue, BlueFilter);
            AddButton("Originaal", 350, 610, Color.LightGreen, OriginalImage);

            AddButton("Heledam", 480, 610, Color.LightYellow,
                (s, e) => ChangeBrightness(20));

            AddButton("Tumedam", 590, 610, Color.LightGray,
                (s, e) => ChangeBrightness(-20));

            // Info
            Label infoTitle = AddLabel("Pildi info:", 20, 675);
            infoTitle.Font = new Font("Arial", 10, FontStyle.Bold);

            fileNameLabel = AddLabel("Fail: -", 20, 705);
            imageSizeLabel = AddLabel("Mõõdud: -", 20, 730);
            fileSizeLabel = AddLabel("Suurus: -", 20, 755);

            AddButton("Sulge", 640, 735, Color.LightCoral,
                (s, e) => Close());
        }

        // Loob nupu
        private void AddButton(string text, int x, int y,
            Color color, EventHandler click)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = new Point(x, y);
            button.Size = new Size(100, 35);
            button.BackColor = color;
            button.Click += click;
            Controls.Add(button);
        }

        // Loob teksti
        private Label AddLabel(string text, int x, int y)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = new Point(x, y);
            label.AutoSize = true;
            Controls.Add(label);
            return label;
        }

        // Pildi avamine
        private void OpenImage(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Pildid|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(dialog.FileName);

                pictureBox.Image = new Bitmap(image);
                originalImage = new Bitmap(image);
                image.Dispose();

                zoom = 1;
                UpdateZoom();

                FileInfo file = new FileInfo(dialog.FileName);

                fileNameLabel.Text = "Fail: " + file.Name;
                imageSizeLabel.Text = "Mõõdud: " +
                    originalImage.Width + " x " + originalImage.Height;

                fileSizeLabel.Text = "Suurus: " +
                    (file.Length / 1024) + " KB";
            }
        }

        // Puhastamine
        private void ClearImage(object sender, EventArgs e)
        {
            pictureBox.Image = null;
            zoom = 1;
            UpdateZoom();

            fileNameLabel.Text = "Fail: -";
            imageSizeLabel.Text = "Mõõdud: -";
            fileSizeLabel.Text = "Suurus: -";
        }

        // Taustavärv
        private void ChangeColor(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox.BackColor = dialog.Color;
                picturePanel.BackColor = dialog.Color;
            }
        }

        // Pööramine
        private void RotateLeft(object sender, EventArgs e)
        {
            Rotate(RotateFlipType.Rotate270FlipNone);
        }

        private void RotateRight(object sender, EventArgs e)
        {
            Rotate(RotateFlipType.Rotate90FlipNone);
        }

        private void Rotate(RotateFlipType direction)
        {
            if (pictureBox.Image == null)
                return;

            pictureBox.Image.RotateFlip(direction);
            pictureBox.Refresh();
        }

        // Suum
        private void ZoomIn(object sender, EventArgs e)
        {
            if (pictureBox.Image != null && zoom < 2)
            {
                zoom += 0.2f;
                UpdateZoom();
            }
        }

        private void ZoomOut(object sender, EventArgs e)
        {
            if (pictureBox.Image != null && zoom > 0.4f)
            {
                zoom -= 0.2f;
                UpdateZoom();
            }
        }

        private void UpdateZoom()
        {
            pictureBox.Size = new Size(
                (int)(735 * zoom),
                (int)(385 * zoom));

            zoomLabel.Text = (int)(zoom * 100) + "%";
        }

        // Joonistamine
        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (drawCheckBox.Checked && pictureBox.Image != null)
            {
                drawing = true;
                lastPoint = e.Location;
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!drawing || pictureBox.Image == null)
                return;

            float x = (float)pictureBox.Image.Width / pictureBox.Width;
            float y = (float)pictureBox.Image.Height / pictureBox.Height;

            Point start = new Point(
                (int)(lastPoint.X * x),
                (int)(lastPoint.Y * y));

            Point end = new Point(
                (int)(e.X * x),
                (int)(e.Y * y));

            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            using (Pen pen = new Pen(Color.Red, 4))
                g.DrawLine(pen, start, end);

            lastPoint = e.Location;
            pictureBox.Refresh();
        }

        // Must-valge
        private void GrayFilter(object sender, EventArgs e)
        {
            if (pictureBox.Image == null)
                return;

            Bitmap bmp = new Bitmap(pictureBox.Image);

            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int gray = (c.R + c.G + c.B) / 3;
                    bmp.SetPixel(x, y,
                        Color.FromArgb(gray, gray, gray));
                }

            pictureBox.Image = bmp;
        }

        // Punane filter
        private void RedFilter(object sender, EventArgs e)
        {
            ColorFilter(true);
        }

        // Sinine filter
        private void BlueFilter(object sender, EventArgs e)
        {
            ColorFilter(false);
        }

        private void ColorFilter(bool red)
        {
            if (pictureBox.Image == null)
                return;

            Bitmap bmp = new Bitmap(pictureBox.Image);

            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);

                    if (red)
                        bmp.SetPixel(x, y,
                            Color.FromArgb(c.R, c.G / 3, c.B / 3));
                    else
                        bmp.SetPixel(x, y,
                            Color.FromArgb(c.R / 3, c.G / 3, c.B));
                }

            pictureBox.Image = bmp;
        }

        // Originaalpilt
        private void OriginalImage(object sender, EventArgs e)
        {
            if (originalImage != null)
                pictureBox.Image = new Bitmap(originalImage);
        }

        // Heledus
        private void ChangeBrightness(int amount)
        {
            if (pictureBox.Image == null)
                return;

            Bitmap bmp = new Bitmap(pictureBox.Image);

            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);

                    int r = Math.Max(0, Math.Min(255, c.R + amount));
                    int g = Math.Max(0, Math.Min(255, c.G + amount));
                    int b = Math.Max(0, Math.Min(255, c.B + amount));

                    bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                }

            pictureBox.Image = bmp;
        }
    }
}