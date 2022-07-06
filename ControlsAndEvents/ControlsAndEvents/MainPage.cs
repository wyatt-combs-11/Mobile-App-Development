using System;

using Xamarin.Forms;

namespace ControlsAndEvents {
    public class MainPage : ContentPage {

        // Properties
        Label entryLabel, buttonLabel, checkLabel, switchLabel, stepLabel, slideLabel;
        Entry entry;
        Button button;
        CheckBox checkBox;
        Switch sw;
        Stepper stepper;
        Slider slider;
        int cnt;

        public MainPage() {
            StackLayout topLevel = new StackLayout { Padding = 30};
            StackLayout entryLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
            StackLayout buttonLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
            StackLayout checkLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
            StackLayout switchLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
            StackLayout slideLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
            StackLayout stepLayout = new StackLayout { Orientation = StackOrientation.Horizontal };

            entryLabel = new Label { Text = "0" };
            buttonLabel = new Label { Text = "0" };
            checkLabel = new Label { Text = "checked" };
            switchLabel = new Label { Text = "on" };
            stepLabel = new Label { Text = "0.5" };
            slideLabel = new Label { Text = "0.5" };
            cnt = 0;

            entry = new Entry() { WidthRequest = 100 };
            button = new Button { Text = "Click me" };
            checkBox = new CheckBox { IsChecked = true };
            sw = new Switch { IsToggled = true };
            stepper = new Stepper { Value = 0.5, Minimum = 0, Maximum = 1, Increment = 0.1 };
            slider = new Slider { Value = 0.5, Minimum = 0, Maximum = 1, WidthRequest = 250 };

            entry.TextChanged += entryChanged;
            button.Clicked += onClicked;
            checkBox.CheckedChanged += onChecked;
            sw.Toggled += onToggled;
            stepper.ValueChanged += onValueChangedStepper;
            slider.ValueChanged += OnValueChangedSlider;

            entryLayout.Children.Add(entry);
            entryLayout.Children.Add(entryLabel);
            buttonLayout.Children.Add(button);
            buttonLayout.Children.Add(buttonLabel);
            checkLayout.Children.Add(checkBox);
            checkLayout.Children.Add(checkLabel);
            switchLayout.Children.Add(sw);
            switchLayout.Children.Add(switchLabel);
            stepLayout.Children.Add(stepper);
            stepLayout.Children.Add(stepLabel);
            slideLayout.Children.Add(slider);
            slideLayout.Children.Add(slideLabel);

            topLevel.Children.Add(entryLayout);
            topLevel.Children.Add(buttonLayout);
            topLevel.Children.Add(checkLayout);
            topLevel.Children.Add(switchLayout);
            topLevel.Children.Add(stepLayout);
            topLevel.Children.Add(slideLayout);

            this.Content = topLevel;
        }

        private void entryChanged(object sender, TextChangedEventArgs e) {
            entryLabel.Text = "" + entry.Text.Length;
        }

        private void onClicked(object sender, EventArgs e) {
            cnt++;
            buttonLabel.Text = "" + cnt;
        }

        private void onChecked(object sender, CheckedChangedEventArgs e) {
            if (checkBox.IsChecked)
                checkLabel.Text = "checked";
            else
                checkLabel.Text = "unchecked";
        }

        private void onToggled(object sender, ToggledEventArgs e) {
            if (sw.IsToggled)
                switchLabel.Text = "on";
            else
                switchLabel.Text = "off";
        }

        private void onValueChangedStepper(object sender, ValueChangedEventArgs e) {
            stepLabel.Text = "" + stepper.Value;
        }

        private void OnValueChangedSlider(object sender, ValueChangedEventArgs e) {
            slideLabel.Text = "" + Math.Round(slider.Value, 1);
        }
    }
}
