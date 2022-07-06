using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StackNavigation
{
    public partial class LicensesPage : ContentPage
    {
        public LicensesPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Back");
        }
        LicenseInfoPage lip;

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private async void OnClicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            lip = new LicenseInfoPage(b.Text);
            await Navigation.PushAsync(lip, true);
        }
    }
}
