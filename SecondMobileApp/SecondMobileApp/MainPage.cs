using System;

using Xamarin.Forms;

namespace SecondMobileApp {
    class MainPage : ContentPage {
        Entry firstNum, secondNum;
        Label numLabel1, numLabel2, resultLabel;
        Label dpLabel, progBarLabel, slideLabel, swLabel, stepsLabel;
        Button calcButton;
        DatePicker dates;
        ProgressBar progBar;
        Slider slide;
        Switch sw;
        Stepper steps;

        public MainPage() {
            firstNum = new Entry { Text = "0" };
            secondNum = new Entry { Text = "0" };
            dates = new DatePicker() { Date = new DateTime(1999, 12, 31) };
            progBar = new ProgressBar();
            slide = new Slider();
            sw = new Switch() { IsToggled = true };
            steps = new Stepper();

            resultLabel = new Label { Text = "0" };
            numLabel1 = new Label() { Text = "First Number" };
            numLabel2 = new Label() { Text = "First Number" };
            dpLabel = new Label() { Text = "Date Picker" };
            progBarLabel = new Label() { Text = "Progress Bar" };
            slideLabel = new Label() { Text = "Slider" };
            swLabel = new Label() { Text = "Switch" };
            stepsLabel = new Label() { Text = "Stepper" };
            calcButton = new Button();
            calcButton.Text = "Calculate";

            StackLayout panel = new StackLayout();
            panel.Children.Add(resultLabel);
            panel.Children.Add(numLabel1);
            panel.Children.Add(firstNum);
            panel.Children.Add(numLabel2);
            panel.Children.Add(secondNum);
            panel.Children.Add(calcButton);
            panel.Children.Add(dpLabel);
            panel.Children.Add(dates);
            panel.Children.Add(progBarLabel);
            panel.Children.Add(progBar);
            panel.Children.Add(slideLabel);
            panel.Children.Add(slide);
            panel.Children.Add(swLabel);
            panel.Children.Add(sw);
            panel.Children.Add(stepsLabel);
            panel.Children.Add(steps);
            panel.Padding = new Thickness(0, 40, 0, 0);

            this.Content = panel;

            calcButton.Clicked += OnClick;
        }

        private void OnClick(object sender, EventArgs e) {
            try {
                resultLabel.Text = "" + (Int32.Parse(firstNum.Text) + Int32.Parse(secondNum.Text));
            } catch (Exception ex) {
                resultLabel.Text = "One or more numbers is invalid";
            }
        }
    }

}

