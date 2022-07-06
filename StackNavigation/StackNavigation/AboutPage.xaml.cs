using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StackNavigation
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }
        async private void OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }

}
