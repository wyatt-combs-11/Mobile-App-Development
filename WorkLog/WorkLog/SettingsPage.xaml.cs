using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WorkLog
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            List<string> options = new List<string>();
            options.Add("By Date");
            options.Add("By Job Name");
            TotalOption.ItemsSource = options;
            if (!Preferences.ContainsKey("option"))
                Preferences.Set("option", 0);
            TotalOption.SelectedIndex = Preferences.Get("option", 0);
        }

        void OptionsChanged(System.Object sender, System.EventArgs e)
        {
            Preferences.Set("option", TotalOption.SelectedIndex);
        }

        private async void OpenCredits(System.Object sender, System.EventArgs e)
        {
            await Browser.OpenAsync("https://miamioh.edu/", BrowserLaunchMode.External);
        }
    }
}
