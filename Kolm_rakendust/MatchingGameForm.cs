using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KolmRakendust
{
    public class MatchingGameForm : Form
    {
        private Button firstButton;
        private Button secondButton;

        private Timer timer;

        private List<string> symbols =
            new List<string>()
            {
                "★", "★",
                "●", "●",
                "■", "■",
                "▲", "▲",
                "♥", "♥",
                "☀", "☀",
                "♣", "♣",
                "♦", "♦"
            };

        public MatchingGameForm()
        {
            Text = "Sarnaste piltide mäng";
            Size = new Size(460, 520);
            StartPosition = FormStartPosition.CenterScreen;

            timer = new Timer();
            timer.Interval = 700;
            timer.Tick += HideSymbols;

            Random random = new Random();

            symbols = symbols
                .OrderBy(x => random.Next())
                .ToList();

            int index = 0;

            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    Button button = new Button();

                    button.Size = new Size(90, 90);

                    button.Location =
                        new Point(
                            20 + column * 100,
                            20 + row * 100
                        );

                    button.Font =
                        new Font("Arial", 28);

                    button.Tag = symbols[index];

                    button.Click += CardClick;

                    Controls.Add(button);

                    index++;
                }
            }

            Button restartButton = new Button();

            restartButton.Text = "Uus mäng";
            restartButton.Size = new Size(120, 35);
            restartButton.Location = new Point(160, 425);

            restartButton.Click += (s, e) =>
            {
                Close();

                MatchingGameForm newGame =
                    new MatchingGameForm();

                newGame.Show();
            };

            Controls.Add(restartButton);
        }

        private void CardClick(object sender, EventArgs e)
        {
            if (timer.Enabled)
                return;

            Button clickedButton =
                sender as Button;

            if (clickedButton.Text != "")
                return;

            clickedButton.Text =
                clickedButton.Tag.ToString();

            if (firstButton == null)
            {
                firstButton = clickedButton;

                return;
            }

            secondButton = clickedButton;

            if (firstButton.Tag.ToString()
                == secondButton.Tag.ToString())
            {
                firstButton.Enabled = false;
                secondButton.Enabled = false;

                firstButton = null;
                secondButton = null;

                CheckWinner();
            }
            else
            {
                timer.Start();
            }
        }

        private void HideSymbols(
            object sender,
            EventArgs e)
        {
            timer.Stop();

            firstButton.Text = "";
            secondButton.Text = "";

            firstButton = null;
            secondButton = null;
        }

        private void CheckWinner()
        {
            foreach (Control control in Controls)
            {
                if (control is Button button &&
                    button.Tag != null &&
                    button.Enabled)
                {
                    return;
                }
            }

            MessageBox.Show(
                "Palju õnne! Sa võitsid!"
            );
        }
    }
}
