using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DXDLabs
{
    public partial class ExecAboutPage : ContentPage
    {
        private string[] names = new string[] { "Castor Grande", "Ryan Gallagher", "Sean Gallagher", "Nate Louk" };
        private string[] positions = new string[] { "CEO", "Creative Director", "CMO", "CTO & Designer" };
        private string[] urls = new string[] {
            "https://drivenxdiscipline.com/wp-content/uploads/2022/01/castor750.png",
            "https://drivenxdiscipline.com/wp-content/uploads/2022/01/202200658_5306887536048066_4726920941329437062_n-150x150.jpg",
            "https://drivenxdiscipline.com/wp-content/uploads/2022/01/202538993_124520763151322_1675395176911656763_n.jpg",
            "https://drivenxdiscipline.com/wp-content/uploads/2022/01/204447359_966265647440883_4172747929720612932_n-500x500.jpg"
        };

        public ExecAboutPage(Int32 index)
        {
            InitializeComponent();
            ExecName.Text = names[index];
            ExecPos.Text = positions[index];
            ExecHeadShot.Source = new Uri(urls[index]);
        }

        private async void OnInstagram(Object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://www.instagram.com/dxd.labs/");
        }
    }
}
