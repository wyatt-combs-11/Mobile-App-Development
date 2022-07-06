using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DXDLabs
{
    public partial class MindsetClubPage : ContentPage
    {
        string dailyQuote;
        public MindsetClubPage()
        {
            InitializeComponent();
            dailyQuote = "\"For what a shame that a man should go life through without knowing what his body can achieve.\"";
            Quote.Text = dailyQuote;
        }

        private async void MindsetClicked(System.Object sender, System.EventArgs e)
        {
            await Browser.OpenAsync("https://linktr.ee/dxdlabs");
        }
    }
}
