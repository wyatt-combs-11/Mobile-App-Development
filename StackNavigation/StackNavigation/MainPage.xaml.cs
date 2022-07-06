using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StackNavigation
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Back");
        }
        PrivacyPage pp;
        UnitsPage up;
        AboutPage ap;
        LicensesPage lp;

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private async void PrivacyClicked(object sender, EventArgs e)
        {
            pp = new PrivacyPage();
            await Navigation.PushAsync(pp, true);
        }
        private async void UnitsClicked(object sender, EventArgs e)
        {
            up = new UnitsPage();
            await Navigation.PushAsync(up, true);
        }
        private async void AboutClicked(object sender, EventArgs e)
        {
            ap = new AboutPage();
            await Navigation.PushModalAsync(ap, true);
        }
        private async void LicenseClicked(object sender, EventArgs e)
        {
            lp = new LicensesPage();
            await Navigation.PushAsync(lp, true);
        }
    }
}
