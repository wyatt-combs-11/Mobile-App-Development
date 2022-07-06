using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StackNavigation
{
    public partial class UnitsPage : ContentPage
    {
        public UnitsPage()
        {
            InitializeComponent();
            if (!Preferences.ContainsKey("Unit"))
                Preferences.Set("Unit", false);
            Units.IsToggled = (Preferences.Get("Unit", false));
        }

        private void UnitToggle(Object sender, ToggledEventArgs e)
        {
            Preferences.Set("Unit", Units.IsToggled);
        }
    }
}
