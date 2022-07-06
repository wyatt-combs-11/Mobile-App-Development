using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace TabbedCarousel
{
    public class MonthPage : ContentPage
    {
        public MonthPage(string month)
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = month, Padding = new Thickness(0,30,0,0) }
                }
            };
        }
    }
}