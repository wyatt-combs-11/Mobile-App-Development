using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DXDLabs
{
    public partial class AboutPage : CarouselPage
    {
        public AboutPage()
        {
            InitializeComponent();
            foreach (int i in new int[] { 0, 1, 2, 3 })
                Children.Add(new ExecAboutPage(i));
        }
    }
}
