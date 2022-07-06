using System;

using Xamarin.Forms;

namespace StackNavigation
{
    public class LicenseInfoPage : ContentPage
    {
        public LicenseInfoPage(string lName)
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = lName + " information",
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                VerticalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };
        }
    }
}

