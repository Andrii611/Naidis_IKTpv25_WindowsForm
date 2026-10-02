using System;
using System.Drawing;
using System.Windows.Forms;

namespace Naidis_IKTpv25_WindowsForm

{
    public partial class MathGameForm : Form
    {
        Random random = new Random();

        Label exampleLabel;
        Label correctLabel;
        Label wrongLabel;
        Label timeLabel;

        TextBox answerBox;
        ComboBox difficultyBox;

        Button checkButton;
        Button startButton;

        Timer timer;

        int correct = 0;
        int wrong = 0;
        int seconds = 60;
        int correctAnswer;

        bool gameStarted = false;

        public MathGameForm()
        {
            Text = "Matemaatiline mäng";
            Size = new Size(500, 480);
            StartPosition = FormStartPosition.CenterScreen;

            Label title = new Label();
            title.Text = "Matemaatiline mäng";
            title.Font = new Font("Arial", 18, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(125, 25);

            Label difficultyLabel = new Label();
            difficultyLabel.Text = "Raskusaste:";
            difficultyLabel.Location = new Point(80, 85);
            difficultyLabel.AutoSize = true;

            difficultyBox = new ComboBox();
            difficultyBox.Location = new Point(180, 80);
            difficultyBox.Size = new Size(150, 30);
            difficultyBox.DropDownStyle = ComboBoxStyle.DropDownList;
            difficultyBox.Items.Add("Lihtne");
            difficultyBox.Items.Add("Keskmine");
            difficultyBox.Items.Add("Raske");
            difficultyBox.SelectedIndex = 0;

            startButton = new Button();
            startButton.Text = "Alusta mängu";
            startButton.Location = new Point(180, 125);
            startButton.Size = new Size(130, 35);
            startButton.Click += StartGame;

            exampleLabel = new Label();
            exampleLabel.Text = "Vali raskusaste ja alusta mängu";
            exampleLabel.Font = new Font("Arial", 15, FontStyle.Bold);
            exampleLabel.AutoSize = true;
            exampleLabel.Location = new Point(80, 190);

            answerBox = new TextBox();
            answerBox.Location = new Point(140, 235);
            answerBox.Size = new Size(100, 30);
            answerBox.Enabled = false;

            checkButton = new Button();
            checkButton.Text = "Kontrolli";
            checkButton.Location = new Point(250, 233);
            checkButton.Size = new Size(100, 30);
            checkButton.Enabled = false;
            checkButton.Click += CheckAnswer;

            correctLabel = new Label();
            correctLabel.Text = "Õiged: 0";
            correctLabel.Location = new Point(100, 290);
            correctLabel.AutoSize = true;

            wrongLabel = new Label();
            wrongLabel.Text = "Valed: 0";
            wrongLabel.Location = new Point(210, 290);
            wrongLabel.AutoSize = true;

            timeLabel = new Label();
            timeLabel.Text = "Aeg: 60 s";
            timeLabel.Location = new Point(320, 290);
            timeLabel.AutoSize = true;

            Button newGameButton = new Button();
            newGameButton.Text = "Uus mäng";
            newGameButton.Location = new Point(180, 345);
            newGameButton.Size = new Size(120, 35);
            newGameButton.Click += NewGame;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            Controls.Add(title);
            Controls.Add(difficultyLabel);
            Controls.Add(difficultyBox);
            Controls.Add(startButton);
            Controls.Add(exampleLabel);
            Controls.Add(answerBox);
            Controls.Add(checkButton);
            Controls.Add(correctLabel);
            Controls.Add(wrongLabel);
            Controls.Add(timeLabel);
            Controls.Add(newGameButton);
        }

        private void StartGame(object sender, EventArgs e)
        {
            correct = 0;
            wrong = 0;
            seconds = 60;

            correctLabel.Text = "Õiged: 0";
            wrongLabel.Text = "Valed: 0";
            timeLabel.Text = "Aeg: 60 s";

            gameStarted = true;

            difficultyBox.Enabled = false;
            startButton.Enabled = false;

            answerBox.Enabled = true;
            checkButton.Enabled = true;

            timer.Start();

            NewQuestion();
        }

        private void NewQuestion()
        {
            int maxNumber = 10;

            if (difficultyBox.SelectedItem.ToString() == "Keskmine")
                maxNumber = 30;

            if (difficultyBox.SelectedItem.ToString() == "Raske")
                maxNumber = 100;

            int operation = random.Next(1, 5);

            int number1;
            int number2;

            if (operation == 4)
            {
                number2 = random.Next(1, maxNumber + 1);
                correctAnswer = random.Next(1, maxNumber + 1);
                number1 = number2 * correctAnswer;

                exampleLabel.Text =
                    number1 + " ÷ " + number2 + " = ?";
            }
            else
            {
                number1 = random.Next(1, maxNumber + 1);
                number2 = random.Next(1, maxNumber + 1);

                if (operation == 1)
                {
                    correctAnswer = number1 + number2;

                    exampleLabel.Text =
                        number1 + " + " + number2 + " = ?";
                }

                if (operation == 2)
                {
                    correctAnswer = number1 - number2;

                    exampleLabel.Text =
                        number1 + " - " + number2 + " = ?";
                }

                if (operation == 3)
                {
                    correctAnswer = number1 * number2;

                    exampleLabel.Text =
                        number1 + " × " + number2 + " = ?";
                }
            }

            answerBox.Clear();
            answerBox.Focus();
        }

        private void CheckAnswer(object sender, EventArgs e)
        {
            if (!gameStarted)
                return;

            int answer;

            if (!int.TryParse(answerBox.Text, out answer))
            {
                MessageBox.Show("Sisesta number!");
                return;
            }

            if (answer == correctAnswer)
            {
                correct++;
                MessageBox.Show("Õige!");
            }
            else
            {
                wrong++;

                MessageBox.Show(
                    "Vale! Õige vastus: " +
                    correctAnswer);
            }

            correctLabel.Text = "Õiged: " + correct;
            wrongLabel.Text = "Valed: " + wrong;

            NewQuestion();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            seconds--;

            timeLabel.Text =
                "Aeg: " + seconds + " s";

            if (seconds <= 0)
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            timer.Stop();

            gameStarted = false;

            answerBox.Enabled = false;
            checkButton.Enabled = false;

            timeLabel.Text = "Aeg: 0 s";
            exampleLabel.Text = "Aeg läbi!";

            MessageBox.Show(
                "Aeg läbi!\n\n" +
                "Õiged: " + correct + "\n" +
                "Valed: " + wrong);

            difficultyBox.Enabled = true;
            startButton.Enabled = true;
        }

        private void NewGame(object sender, EventArgs e)
        {
            timer.Stop();

            correct = 0;
            wrong = 0;
            seconds = 60;

            gameStarted = false;

            difficultyBox.Enabled = true;
            startButton.Enabled = true;

            answerBox.Enabled = false;
            checkButton.Enabled = false;

            answerBox.Clear();

            correctLabel.Text = "Õiged: 0";
            wrongLabel.Text = "Valed: 0";
            timeLabel.Text = "Aeg: 60 s";

            exampleLabel.Text =
                "Vali raskusaste ja alusta mängu";
        }
    }
}