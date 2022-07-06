using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace TabbedCarousel
{
    public class MonthsPage : CarouselPage
    {
        public MonthsPage()
        {
            string[] months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            foreach(string m in months)
            {
                this.Children.Add(new MonthPage(m));
            }
        }
    }
}