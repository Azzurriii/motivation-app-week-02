using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MotivationTestApp
{
    public sealed partial class QuotesTestPage : Window
    {
        private List<Quotes> quotes;
        private int currentIndex = 0;
        private int score = 0;

        public QuotesTestPage()
        {
            this.InitializeComponent();
            LoadQuotes();
            ShowQuote(currentIndex);
        }

        private void LoadQuotes()
        {
            quotes = new List<Quotes>
            {
                new Quotes
                {
                    Text = "Before software can be reusable it first has to be usable.",
                    ImagePath = "Questions/img-1.jpg",
                    MissingWord = "reusable"
                },
                new Quotes
                {
                    Text = "I'm not a great programmer, I'm just a good programmer with great habits.",
                    ImagePath = "Questions/img-2.jpg",
                    MissingWord = "habits"
                },
                new Quotes
                {
                    Text = "It's harder to read code than to write it.",
                    ImagePath = "Questions/img-3.jpg",
                    MissingWord = "harder"
                },
                new Quotes
                {
                    Text = "There are two ways to write error-free programs; only the third one works.",
                    ImagePath = "Questions/img-4.jpg",
                    MissingWord = "third"
                },
                new Quotes
                {
                    Text = "Sometimes it pays to stay in bed on Monday rather than spending the rest of the week debugging Monday's code.",
                    ImagePath = "Questions/img-5.jpg",
                    MissingWord = "rather"
                },
                new Quotes
                {
                    Text = "In my mind, I'm always the best.",
                    ImagePath = "Questions/img-6.jpg",
                    MissingWord = "best"
                },
                new Quotes
                {
                    Text = "Just fucking try.",
                    ImagePath = "Questions/img-7.jpg",
                    MissingWord = "try"
                },
                new Quotes
                {
                    Text = "Remember why you started.",
                    ImagePath = "Questions/img-8.jpg",
                    MissingWord = "started"
                },
                new Quotes
                {
                    Text = "Tomorrow is a new day.",
                    ImagePath = "Questions/img-9.jpg",
                    MissingWord = "Tomorrow"
                },
                new Quotes
                {
                    Text = "You weren't born to just pay bills and die.",
                    ImagePath = "Questions/img-10.jpg",
                    MissingWord = "born"
                }
            };
        }

        private void ShowQuote(int index)
        {
            if (index >= 0 && index < quotes.Count)
            {
                var quote = quotes[index];
                QuoteTextBlock.Text = GeneratePartialQuote(quote.Text, quote.MissingWord);

                var imageUri = new Uri($"ms-appx:///Assets/{quote.ImagePath}");
                System.Diagnostics.Debug.WriteLine("Image URI: " + imageUri);
                QuoteImage.Source = new BitmapImage(imageUri);
                try
                {
                    QuoteImage.Source = new BitmapImage(imageUri);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading image: {ex.Message}");
                    QuoteImage.Source = null; // Clear source if there's an error
                }
            }
        }

        private string GeneratePartialQuote(string fullText, string missingWord)
        {
            return fullText.Replace(missingWord, "______");
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var quote = quotes[currentIndex];

            string userWord = MissingWordTextBox.Text;

            if (userWord.Equals(quote.MissingWord, StringComparison.OrdinalIgnoreCase))
            {
                score++;
            }

            currentIndex++;
            if (currentIndex < quotes.Count)
            {
                ShowQuote(currentIndex);
            }
            else
            {
                ShowFinalScore();
            }

            MissingWordTextBox.Text = String.Empty;
        }

        private void ShowFinalScore()
        {
            QuoteTextBlock.Text = $"You scored {score} out of {quotes.Count}!";
            QuoteImage.Visibility = Visibility.Collapsed;
            MissingWordTextBox.Visibility = Visibility.Collapsed;
            SubmitButton.Visibility = Visibility.Collapsed;
            SkipButton.Visibility = Visibility.Collapsed;
        }

        private void ReturnDashBoard_Click(object sender, RoutedEventArgs e)
        {
            var dashBoard = new DashBoard();
            dashBoard.Activate();
            this.Close();
        }

        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            currentIndex++;
            if (currentIndex < quotes.Count)
            {
                ShowQuote(currentIndex);
            }
            else
            {
                ShowFinalScore();
            }
        }
    }
}
