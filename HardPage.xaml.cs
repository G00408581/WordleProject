using System;
using System.Collections.Generic;
using ThreadingTimer = System.Threading.Timer;

namespace Wordle
{
    public partial class HardPage : ContentPage
    {
        private List<Entry> row1; //entries for rows to be checked & locked once a letter has been entered
        private List<Entry> row2;
        private List<Entry> row3;
        private List<Entry> row4;
        private List<Entry> row5;
        private int clickCounter = 0; //using this for locking rows based on how many times it's been pressed and if the previous row was filled or not.
        private int timer = 0;
        private ThreadingTimer Timer; //adding the timer 

        public HardPage()
        {
            InitializeComponent();
            this.Appearing += OnPageAppearing;

            // Initialize the timer
            Timer = new ThreadingTimer(Timer_Elapsed, null, 1000, 1000);
        }

        private void OnPageAppearing(object sender, EventArgs e) //initializing list of rows
        {
            row1 = new List<Entry>
            {
                (Entry)FindByName("Row1_0"),
                (Entry)FindByName("Row1_1"),
                (Entry)FindByName("Row1_2"),
                (Entry)FindByName("Row1_3"),
                (Entry)FindByName("Row1_4")
            };
            row2 = new List<Entry>
            {
                (Entry)FindByName("Row2_0"),
                (Entry)FindByName("Row2_1"),
                (Entry)FindByName("Row2_2"),
                (Entry)FindByName("Row2_3"),
                (Entry)FindByName("Row2_4")
            };
            row3 = new List<Entry>
            {
                (Entry)FindByName("Row3_0"),
                (Entry)FindByName("Row3_1"),
                (Entry)FindByName("Row3_2"),
                (Entry)FindByName("Row3_3"),
                (Entry)FindByName("Row3_4")
            };
            row4 = new List<Entry>
            {
                (Entry)FindByName("Row4_0"),
                (Entry)FindByName("Row4_1"),
                (Entry)FindByName("Row4_2"),
                (Entry)FindByName("Row4_3"),
                (Entry)FindByName("Row4_4")
            };
            row5 = new List<Entry>
            {
                (Entry)FindByName("Row5_0"),
                (Entry)FindByName("Row5_1"),
                (Entry)FindByName("Row5_2"),
                (Entry)FindByName("Row5_3"),
                (Entry)FindByName("Row5_4")
            };
        }


        private void setWordClicked(object sender, EventArgs e) //set word button locks row one and reveals the submit button
        {
            LockRow(row1);
            setButton.IsVisible = false;
            submitButton.IsVisible = true;
            Timer.Change(0, 6000); 
        }

        private void submitClicked(object sender, EventArgs e) //locks rows based on how many times submit was clicked
        {
            clickCounter++;
            switch (clickCounter)
            {
                case 1:
                    LockRow(row2);
                    break;
                case 2:
                    LockRow(row3);
                    break;
                case 3:
                    LockRow(row4);
                    break;
                case 4:
                    LockRow(row5);
                    break;
            }
        }

        // List<string> words = FiveLetterWords("fiveletterwords.txt");


        private async Task FiveLetterWords(string filePath) //supposed to read the all the text from the file but kept breaking i didn't know how to fix it
        {
            string[] lines = await Task.Run(() => System.IO.File.ReadAllLines(filePath));

        }

        private void LockRow(List<Entry> row) //checks that ever row has an entry and changes colour to green once row is locked
        {
            bool allEntriesHaveText = row.All(entry => !string.IsNullOrEmpty(entry.Text));

            if (allEntriesHaveText)
            {
                foreach (var entry in row)
                {
                    entry.IsEnabled = false;
                    entry.TextColor = Color.FromRgb(0, 0, 0);
                    entry.BackgroundColor = Color.FromRgb(43, 209, 21);
                }
            }
        }

        private void returnClicked(object sender, EventArgs e) //sends user back to mainpage
        {
            Navigation.PushAsync(new MainPage());
        }

        private void Timer_Elapsed(object state) //timer that updates the label on hardpage.xaml and goes up to 60 seconds after which it sends the user back to the mainpage
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                timer++;
                TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
                timerLabel.Text = timeSpan.ToString(@"mm\:ss");

                if (timer >= 60)
                {
                    Timer.Change(Timeout.Infinite, Timeout.Infinite); // Stop the timer
                    Navigation.PushAsync(new MainPage());
                }
            });
        }
    }
}
