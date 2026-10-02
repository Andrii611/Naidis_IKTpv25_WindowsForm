using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Naidis_IKTpv25_WindowsForm

{
    public partial class MatchingGameForm : Form
    {
        private Button firstButton;
        private Button secondButton;

        private Timer hideTimer;
        private Timer gameTimer;

        private Label timeLabel;
        private Label movesLabel;
        private Label scoreLabel;
        private Label recordLabel;

        private ComboBox difficultyBox;
        private Button startButton;

        private List<Button> cards = new List<Button>();

        private int seconds = 0;
        private int moves = 0;
        private int score = 0;
        private int bestScore = 0;

        private bool gameStarted = false;

        private Random random = new Random();

        private List<string> allSymbols =
            new List<string>()
            {
                "★", "●", "■", "▲",
                "♥", "☀", "♣", "♦",
                "☺", "♫", "☂", "☕",
                "⚽", "✈", "☎", "☁",
                "♠", "♪"
            };

        public MatchingGameForm()
        {
            Text = "Sarnaste piltide mäng";
            Size = new Size(700, 750);
            StartPosition = FormStartPosition.CenterScreen;

            Label title = new Label();
            title.Text = "Sarnaste piltide mäng";
            title.Font = new Font("Arial", 18);
            title.AutoSize = true;
            title.Location = new Point(210, 20);

            Label difficultyLabel = new Label();
            difficultyLabel.Text = "Raskusaste:";
            difficultyLabel.Location = new Point(30, 70);
            difficultyLabel.AutoSize = true;

            difficultyBox = new ComboBox();
            difficultyBox.Location = new Point(120, 67);
            difficultyBox.Size = new Size(120, 30);
            difficultyBox.DropDownStyle = ComboBoxStyle.DropDownList;
            difficultyBox.Items.Add("Lihtne");
            difficultyBox.Items.Add("Raske");
            difficultyBox.SelectedIndex = 0;

            startButton = new Button();
            startButton.Text = "Alusta mängu";
            startButton.Location = new Point(260, 65);
            startButton.Size = new Size(120, 30);
            startButton.Click += StartButton_Click;

            timeLabel = new Label();
            timeLabel.Text = "Aeg: 0 s";
            timeLabel.Location = new Point(30, 110);
            timeLabel.AutoSize = true;

            movesLabel = new Label();
            movesLabel.Text = "Käigud: 0";
            movesLabel.Location = new Point(150, 110);
            movesLabel.AutoSize = true;

            scoreLabel = new Label();
            scoreLabel.Text = "Punktid: 0";
            scoreLabel.Location = new Point(280, 110);
            scoreLabel.AutoSize = true;

            recordLabel = new Label();
            recordLabel.Text = "Rekord: 0";
            recordLabel.Location = new Point(410, 110);
            recordLabel.AutoSize = true;

            hideTimer = new Timer();
            hideTimer.Interval = 700;
            hideTimer.Tick += HideSymbols;

            gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;

            Controls.Add(title);
            Controls.Add(difficultyLabel);
            Controls.Add(difficultyBox);
            Controls.Add(startButton);
            Controls.Add(timeLabel);
            Controls.Add(movesLabel);
            Controls.Add(scoreLabel);
            Controls.Add(recordLabel);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void StartGame()
        {
            foreach (Button card in cards)
            {
                Controls.Remove(card);
                card.Dispose();
            }

            cards.Clear();

            firstButton = null;
            secondButton = null;

            seconds = 0;
            moves = 0;
            score = 0;

            gameStarted = true;

            timeLabel.Text = "Aeg: 0 s";
            movesLabel.Text = "Käigud: 0";
            scoreLabel.Text = "Punktid: 0";

            difficultyBox.Enabled = false;
            startButton.Enabled = false;

            if (difficultyBox.SelectedItem.ToString() == "Raske")
                CreateHardGame();
            else
                CreateEasyGame();

            gameTimer.Start();
        }

        private void CreateEasyGame()
        {
            List<string> symbols = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                symbols.Add(allSymbols[i]);
                symbols.Add(allSymbols[i]);
            }

            symbols = symbols.OrderBy(x => random.Next()).ToList();

            int index = 0;

            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    Button button = new Button();
                    button.Size = new Size(90, 90);
                    button.Location = new Point(
                        140 + column * 100,
                        150 + row * 100);
                    button.Font = new Font("Arial", 26);
                    button.Tag = symbols[index];
                    button.Click += CardClick;

                    cards.Add(button);
                    Controls.Add(button);

                    index++;
                }
            }
        }

        private void CreateHardGame()
        {
            List<string> symbols = new List<string>();

            for (int i = 0; i < 18; i++)
            {
                symbols.Add(allSymbols[i]);
                symbols.Add(allSymbols[i]);
            }

            symbols = symbols.OrderBy(x => random.Next()).ToList();

            int index = 0;

            for (int row = 0; row < 6; row++)
            {
                for (int column = 0; column < 6; column++)
                {
                    Button button = new Button();
                    button.Size = new Size(75, 75);
                    button.Location = new Point(
                        100 + column * 80,
                        150 + row * 80);
                    button.Font = new Font("Arial", 20);
                    button.Tag = symbols[index];
                    button.Click += CardClick;

                    cards.Add(button);
                    Controls.Add(button);

                    index++;
                }
            }
        }

        private void CardClick(object sender, EventArgs e)
        {
            if (!gameStarted)
                return;

            if (hideTimer.Enabled)
                return;

            Button clickedButton = sender as Button;

            if (clickedButton == null)
                return;

            if (clickedButton.Text != "")
                return;

            clickedButton.Text = clickedButton.Tag.ToString();

            if (firstButton == null)
            {
                firstButton = clickedButton;
                return;
            }

            secondButton = clickedButton;

            moves++;
            movesLabel.Text = "Käigud: " + moves;

            if (firstButton.Tag.ToString() ==
                secondButton.Tag.ToString())
            {
                firstButton.Enabled = false;
                secondButton.Enabled = false;

                score += 10;
                scoreLabel.Text = "Punktid: " + score;

                firstButton = null;
                secondButton = null;

                CheckWinner();
            }
            else
            {
                hideTimer.Start();
            }
        }

        private void HideSymbols(object sender, EventArgs e)
        {
            hideTimer.Stop();

            if (firstButton != null)
                firstButton.Text = "";

            if (secondButton != null)
                secondButton.Text = "";

            firstButton = null;
            secondButton = null;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            seconds++;
            timeLabel.Text = "Aeg: " + seconds + " s";
        }

        private void CheckWinner()
        {
            foreach (Button card in cards)
            {
                if (card.Enabled)
                    return;
            }

            WinGame();
        }

        private void WinGame()
        {
            gameTimer.Stop();
            gameStarted = false;

            if (score > bestScore)
            {
                bestScore = score;
                recordLabel.Text = "Rekord: " + bestScore;
            }

            MessageBox.Show(
                "Palju õnne! Sa võitsid!\n\n" +
                "Aeg: " + seconds + " sekundit\n" +
                "Käigud: " + moves + "\n" +
                "Punktid: " + score + "\n" +
                "Rekord: " + bestScore
            );

            difficultyBox.Enabled = true;
            startButton.Enabled = true;
        }
    }
}
