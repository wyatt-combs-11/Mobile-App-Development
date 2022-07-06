using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace WordMobileApp
{
    public class MainPage : ContentPage
    {
        EnglishDictionary dict;
        Frame f1, f2;
        Label wordCount, guess;
        Button l1, l2, l3, l4, l5, l6, l7, shuffle;
        Button done, backspace, fill, newGame;
        ListView wordsGuessed;
        int cnt;
        string[] letters;
        List<Button> buttons;
        ObservableCollection<string> words;
        HashSet<string> possible;

        public MainPage()
        {
            dict = new EnglishDictionary();

            // h1
            f1 = new Frame { CornerRadius = 15, BackgroundColor = Color.LightSalmon };
            f2 = new Frame { CornerRadius = 15, BackgroundColor = Color.LightSalmon };
            wordCount = new Label { Text = "0", WidthRequest = 50, BackgroundColor = Color.Transparent };
            guess = new Label { WidthRequest = 100, BackgroundColor = Color.Transparent };
            f1.Content = wordCount;
            f2.Content = guess;

            // h2
            buttons = new List<Button>();
            words = new ObservableCollection<string>();
            l1 = new Button { BackgroundColor = Color.LightCoral, WidthRequest = 30, FontSize = 20 };
            l2 = new Button { BackgroundColor = Color.LightCoral, WidthRequest = 30, FontSize = 20 };
            l3 = new Button { BackgroundColor = Color.LightCoral, WidthRequest = 30, FontSize = 20 };
            l4 = new Button { BackgroundColor = Color.LightCoral, WidthRequest = 30, FontSize = 20 };
            l5 = new Button { BackgroundColor = Color.LightCoral, WidthRequest = 30, FontSize = 20 };
            l6 = new Button { BackgroundColor = Color.LightCoral, WidthRequest = 30, FontSize = 20 };
            l7 = new Button { BackgroundColor = Color.LightCoral, WidthRequest = 30, FontSize = 20 };
            shuffle = new Button { BackgroundColor = Color.LightCoral, WidthRequest = 70, FontSize = 20, Text = "Shuffle"};
            l1.Clicked += LetterClicked;
            l2.Clicked += LetterClicked;
            l3.Clicked += LetterClicked;
            l4.Clicked += LetterClicked;
            l5.Clicked += LetterClicked;
            l6.Clicked += LetterClicked;
            l7.Clicked += LetterClicked;
            shuffle.Clicked += ShuffleLetters;
            cnt = 0;
            letters = new string[7];

            // h3
            ResetLetters();
            done = new Button { Text = "DONE", BorderColor = Color.Black, BackgroundColor = Color.LightGray, Padding = 5 };
            backspace = new Button { Text = "BACKSPACE", BorderColor = Color.Black, BackgroundColor = Color.LightGray, Padding = 5 };
            newGame = new Button { Text = "NEW GAME", BorderColor = Color.Black, BackgroundColor = Color.LightGray, Padding = 5 };
            fill = new Button { Text = "FILL", BorderColor = Color.Black, BackgroundColor = Color.LightGray, Padding = 5 };
            ScrollView sv = new ScrollView { HeightRequest = 250 };
            wordsGuessed = new ListView();
            sv.Content = wordsGuessed;
            wordsGuessed.ItemsSource = words;
            done.Clicked += DoneClicked;
            backspace.Clicked += BackspaceClicked;
            fill.Clicked += FillClicked;
            newGame.Clicked += (s, e) =>
            {
                ResetLetters();
            };

            StackLayout topLevel = new StackLayout { Padding = 20 };
            StackLayout h1 = new StackLayout { Orientation = StackOrientation.Horizontal, Padding = new Thickness(0, 50, 0, 0) };
            StackLayout h2 = new StackLayout { Orientation = StackOrientation.Horizontal, Padding = 5 };
            StackLayout h3 = new StackLayout { Orientation = StackOrientation.Horizontal, Padding = 5, HeightRequest = 30 };

            h1.Children.Add(f1);
            h1.Children.Add(f2);
            h2.Children.Add(l1);
            h2.Children.Add(l2);
            h2.Children.Add(l3);
            h2.Children.Add(l4);
            h2.Children.Add(l5);
            h2.Children.Add(l6);
            h2.Children.Add(l7);
            h2.Children.Add(shuffle);
            h3.Children.Add(done);
            h3.Children.Add(backspace);
            h3.Children.Add(fill);
            h3.Children.Add(newGame);

            topLevel.Children.Add(h1);
            topLevel.Children.Add(h2);
            topLevel.Children.Add(h3);
            topLevel.Children.Add(sv);

            Content = topLevel;
        }

        private void ShuffleLetters(object sender, EventArgs e)
        {
            l7.Text = letters[0];
            l6.Text = letters[1];
            l5.Text = letters[2];
            l4.Text = letters[3];
            l3.Text = letters[4];
            l2.Text = letters[5];
            l1.Text = letters[6];

            letters[0] = l1.Text;
            letters[1] = l2.Text;
            letters[2] = l3.Text;
            letters[3] = l4.Text;
            letters[4] = l5.Text;
            letters[5] = l6.Text;
            letters[6] = l7.Text;
        }

        private void ResetLetters()
        {
            Random random = new Random();
            char[] vowels = new char[] { 'A', 'E', 'I', 'O', 'U' };
            l1.Text = "" + ((char)(random.Next(26) + 65));
            l2.Text = "" + vowels[random.Next(5)];
            l3.Text = "" + ((char)(random.Next(26) + 65));
            l4.Text = "" + ((char)(random.Next(26) + 65));
            l5.Text = "" + vowels[random.Next(5)];
            l6.Text = "" + ((char)(random.Next(26) + 65));
            l7.Text = "" + ((char)(random.Next(26) + 65));

            letters[0] = l1.Text;
            letters[1] = l2.Text;
            letters[2] = l3.Text;
            letters[3] = l4.Text;
            letters[4] = l5.Text;
            letters[5] = l6.Text;
            letters[6] = l7.Text;

            possible = new HashSet<string>();
            for (int i = 0; i < 7; i++)
            {
                if (dict.possibleWord(letters[i]))
                    possible.Add(letters[i]);
                for (int j = 0; j < 7; j++)
                {
                    if (j == i)
                        continue;
                    if (dict.possibleWord(letters[i] + letters[j]))
                        possible.Add(letters[i] + letters[j]);
                    for (int k = 0; k < 7; k++)
                    {
                        if (k == i || k == j)
                            continue;
                        if (dict.possibleWord(letters[i] + letters[j] + letters[k]))
                            possible.Add(letters[i] + letters[j] + letters[k]);
                        for (int l = 0; l < 7; l++)
                        {
                            if(l == i || l == j || l == k)
                            continue;
                            if (dict.possibleWord(letters[i] + letters[j] + letters[k] + letters[l]))
                                possible.Add(letters[i] + letters[j] + letters[k] + letters[l]);
                            for (int m = 0; m < 7; m++)
                            {
                                if (m == i || m == j || m == k || m == l)
                                    continue;
                                if (dict.possibleWord(letters[i] + letters[j] + letters[k] + letters[l] + letters[m]))
                                    possible.Add(letters[i] + letters[j] + letters[k] + letters[l] + letters[m]);
                            }
                        }
                    }
                }
            }
            wordCount.Text = "0/" + possible.Count;
            words.Clear();
        }

        private void LetterClicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.IsEnabled)
            {
                guess.Text += b.Text;
                b.IsEnabled = false;
                buttons.Add(b);
            }
        }

        private void DoneClicked(object sender, EventArgs e)
        {
            Console.WriteLine(dict.possibleWord(guess.Text));
            Console.WriteLine(guess.Text);
            if (dict.possibleWord(guess.Text) && !words.Contains(guess.Text))
            {
                words.Add(guess.Text);
                possible.Remove(guess.Text);
                cnt++;
                wordCount.Text = "" + cnt + "/" + (possible.Count+cnt);
                done.TextColor = Color.Green;
            } else
            {
                done.TextColor = Color.Red;
            }
            l1.IsEnabled = true;
            l2.IsEnabled = true;
            l3.IsEnabled = true;
            l4.IsEnabled = true;
            l5.IsEnabled = true;
            l6.IsEnabled = true;
            l7.IsEnabled = true;
            guess.Text = "";
        }

        private void BackspaceClicked(object sender, EventArgs e)
        {
            int len = guess.Text.Length;
            if (len > 0)
            {
                guess.Text = guess.Text.Substring(0, len - 1);
                buttons[buttons.Count - 1].IsEnabled = true;
                buttons.RemoveAt(buttons.Count - 1);
            }
        }

        private void FillClicked(object sender, EventArgs e)
        {
            foreach (var s in possible)
                words.Add(s);
            wordCount.Text = (possible.Count+cnt) + "/" + (possible.Count+cnt);
        }
    }
}
