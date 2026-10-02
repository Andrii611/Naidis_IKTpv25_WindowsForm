using System;
using System.Drawing;
using System.Windows.Forms;

namespace Naidis_IKTpv25_WindowsForm

{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            Text = "Kolm rakendust";
            Size = new Size(500, 400);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.WhiteSmoke;

            Label title = new Label();
            title.Text = "Vali rakendus";
            title.Font = new Font("Arial", 20, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(145, 30);

            Button pictureButton = new Button();
            pictureButton.Text = "Pildi vaatamine";
            pictureButton.Size = new Size(250, 45);
            pictureButton.Location = new Point(115, 90);
            pictureButton.Font = new Font("Arial", 11);
            pictureButton.BackColor = Color.LightBlue;
            pictureButton.Click += (s, e) =>
            {
                PictureViewerForm form = new PictureViewerForm();
                form.Show();
            };

            Button mathButton = new Button();
            mathButton.Text = "Matemaatiline mäng";
            mathButton.Size = new Size(250, 45);
            mathButton.Location = new Point(115, 145);
            mathButton.Font = new Font("Arial", 11);
            mathButton.BackColor = Color.LightGreen;
            mathButton.Click += (s, e) =>
            {
                MathGameForm form = new MathGameForm();
                form.Show();
            };

            Button matchingButton = new Button();
            matchingButton.Text = "Sarnaste piltide mäng";
            matchingButton.Size = new Size(250, 45);
            matchingButton.Location = new Point(115, 200);
            matchingButton.Font = new Font("Arial", 11);
            matchingButton.BackColor = Color.LightYellow;
            matchingButton.Click += (s, e) =>
            {
                MatchingGameForm form = new MatchingGameForm();
                form.Show();
            };

            Button closeButton = new Button();
            closeButton.Text = "Sulge";
            closeButton.Size = new Size(120, 40);
            closeButton.Location = new Point(180, 280);
            closeButton.Font = new Font("Arial", 10);
            closeButton.BackColor = Color.LightCoral;
            closeButton.Click += (s, e) => Close();

            Controls.Add(title);
            Controls.Add(pictureButton);
            Controls.Add(mathButton);
            Controls.Add(matchingButton);
            Controls.Add(closeButton);
        }
    }
}