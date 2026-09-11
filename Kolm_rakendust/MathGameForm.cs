using System;
using System.Drawing;
using System.Windows.Forms;

namespace KolmRakendust
{
    public class MathGameForm : Form
    {
        private Label questionLabel;
        private TextBox answerBox;
        private Label scoreLabel;

        private Random random = new Random();

        private int number1;
        private int number2;
        private int answer;

        private int score = 0;

        public MathGameForm()
        {
            Text = "Matemaatiline mäng";
            Size = new Size(450, 300);
            StartPosition = FormStartPosition.CenterScreen;

            Label title = new Label();
            title.Text = "Matemaatiline mäng";
            title.Font = new Font("Arial", 18);
            title.AutoSize = true;
            title.Location = new Point(110, 20);

            questionLabel = new Label();
            questionLabel.Font = new Font("Arial", 20);
            questionLabel.AutoSize = true;
            questionLabel.Location = new Point(130, 80);

            answerBox = new TextBox();
            answerBox.Location = new Point(270, 80);
            answerBox.Width = 80;

            Button checkButton = new Button();
            checkButton.Text = "Kontrolli";
            checkButton.Size = new Size(100, 35);
            checkButton.Location = new Point(120, 140);

            checkButton.Click += CheckAnswer;

            Button nextButton = new Button();
            nextButton.Text = "Uus tehe";
            nextButton.Size = new Size(100, 35);
            nextButton.Location = new Point(230, 140);

            nextButton.Click += (s, e) =>
            {
                CreateQuestion();
            };

            scoreLabel = new Label();
            scoreLabel.Text = "Punktid: 0";
            scoreLabel.AutoSize = true;
            scoreLabel.Location = new Point(180, 200);

            Controls.Add(title);
            Controls.Add(questionLabel);
            Controls.Add(answerBox);
            Controls.Add(checkButton);
            Controls.Add(nextButton);
            Controls.Add(scoreLabel);

            CreateQuestion();
        }

        private void CreateQuestion()
        {
            number1 = random.Next(1, 20);
            number2 = random.Next(1, 20);

            int operation = random.Next(0, 3);

            if (operation == 0)
            {
                questionLabel.Text =
                    $"{number1} + {number2} =";

                answer = number1 + number2;
            }
            else if (operation == 1)
            {
                questionLabel.Text =
                    $"{number1} - {number2} =";

                answer = number1 - number2;
            }
            else
            {
                questionLabel.Text =
                    $"{number1} × {number2} =";

                answer = number1 * number2;
            }

            answerBox.Clear();
        }

        private void CheckAnswer(object sender, EventArgs e)
        {
            if (!int.TryParse(answerBox.Text, out int userAnswer))
            {
                MessageBox.Show("Sisesta number!");
                return;
            }

            if (userAnswer == answer)
            {
                MessageBox.Show("Õige!");

                score++;
            }
            else
            {
                MessageBox.Show(
                    "Vale! Õige vastus on " + answer
                );
            }

            scoreLabel.Text =
                "Punktid: " + score;

            CreateQuestion();
        }
    }
}
