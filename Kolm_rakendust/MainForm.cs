using System;
using System.Drawing;
using System.Windows.Forms;

namespace KolmRakendust
{
    public class MainForm : Form
    {
        public MainForm()
        {
            Text = "Kolm rakendust";
            Size = new Size(450, 300);
            StartPosition = FormStartPosition.CenterScreen;

            Label title = new Label();
            title.Text = "Vali rakendus";
            title.Font = new Font("Arial", 18);
            title.AutoSize = true;
            title.Location = new Point(140, 30);

            Button pictureButton = new Button();
            pictureButton.Text = "Pildi vaatamine";
            pictureButton.Size = new Size(200, 40);
            pictureButton.Location = new Point(115, 90);

            pictureButton.Click += (s, e) =>
            {
                PictureViewerForm form = new PictureViewerForm();
                form.Show();
            };

            Button mathButton = new Button();
            mathButton.Text = "Matemaatiline mäng";
            mathButton.Size = new Size(200, 40);
            mathButton.Location = new Point(115, 140);

            mathButton.Click += (s, e) =>
            {
                MathGameForm form = new MathGameForm();
                form.Show();
            };

            Button matchingButton = new Button();
            matchingButton.Text = "Sarnaste piltide mäng";
            matchingButton.Size = new Size(200, 40);
            matchingButton.Location = new Point(115, 190);

            matchingButton.Click += (s, e) =>
            {
                MatchingGameForm form = new MatchingGameForm();
                form.Show();
            };

            Controls.Add(title);
            Controls.Add(pictureButton);
            Controls.Add(mathButton);
            Controls.Add(matchingButton);
        }
    }
}