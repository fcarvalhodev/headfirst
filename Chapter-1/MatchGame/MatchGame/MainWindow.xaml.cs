using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MatchGame
{
    public partial class MainWindow : Window
    {
        private TextBlock lastTextBlockClicked;
        private bool findingMatch = false;

        private DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 10)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play Again ?";
            }
        }

        private void SetUpGame()
        {
            Random rnd = new Random();
            var mainGridTextBlockChildren = mainGrid.Children.OfType<TextBlock>();
            List<string> animalEmoji = new List<string>()
            {
                "🐕‍🦺","🐕‍🦺",
                "🐖", "🐖",
                "🦙", "🦙",
                "🐂","🐂",
                "🐐", "🐐",
                "🐏", "🐏",
                "🧸", "🧸",
                "🦘", "🦘",
                "🐍", "🐍",
                "🐄", "🐄"
            };

            foreach (TextBlock textBlock in mainGridTextBlockChildren)
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = rnd.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }

            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (matchesFound == 10)
            {
                SetUpGame();
            }
        }
    }
}
