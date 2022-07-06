using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GridXAML
{
    public partial class MyPage : ContentPage
    {
        public MyPage()
        {
            InitializeComponent();
        }

        private void OnClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (!(button.Text.Equals("0") && Number.Text.Equals("0")))
                if (Number.Text.Equals("0"))
                    Number.Text = button.Text;
                else
                    Number.Text += button.Text;
        }

        private void OnClear(object sender, EventArgs e)
        {
            Number.Text = "0";
        }

    }
}
