using System;
using System.Drawing;
using System.Windows.Forms;

namespace KolmRakendust
{
    public class PictureViewerForm : Form
    {
        private PictureBox pictureBox;
        private CheckBox stretchCheckBox;

        public PictureViewerForm()
        {
            Text = "Pildi vaatamise programm";
            Size = new Size(700, 550);
            StartPosition = FormStartPosition.CenterScreen;

            pictureBox = new PictureBox();
            pictureBox.Location = new Point(20, 20);
            pictureBox.Size = new Size(640, 400);
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            Button openButton = new Button();
            openButton.Text = "Ava pilt";
            openButton.Location = new Point(20, 440);
            openButton.Size = new Size(100, 35);

            openButton.Click += OpenButton_Click;

            Button clearButton = new Button();
            clearButton.Text = "Puhasta";
            clearButton.Location = new Point(130, 440);
            clearButton.Size = new Size(100, 35);

            clearButton.Click += (s, e) =>
            {
                pictureBox.Image = null;
            };

            stretchCheckBox = new CheckBox();
            stretchCheckBox.Text = "Venita pilti";
            stretchCheckBox.Location = new Point(250, 447);
            stretchCheckBox.AutoSize = true;

            stretchCheckBox.CheckedChanged += (s, e) =>
            {
                if (stretchCheckBox.Checked)
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                else
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            };

            Button backgroundButton = new Button();
            backgroundButton.Text = "Taustavärv";
            backgroundButton.Location = new Point(380, 440);
            backgroundButton.Size = new Size(100, 35);

            backgroundButton.Click += ChangeBackground;

            Button closeButton = new Button();
            closeButton.Text = "Sulge";
            closeButton.Location = new Point(560, 440);
            closeButton.Size = new Size(100, 35);

            closeButton.Click += (s, e) => Close();

            Controls.Add(pictureBox);
            Controls.Add(openButton);
            Controls.Add(clearButton);
            Controls.Add(stretchCheckBox);
            Controls.Add(backgroundButton);
            Controls.Add(closeButton);
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter =
                "Pildid|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image =
                    Image.FromFile(dialog.FileName);
            }
        }

        private void ChangeBackground(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                BackColor = dialog.Color;
            }
        }
    }
}