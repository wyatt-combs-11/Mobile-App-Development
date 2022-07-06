using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DXDLabs
{
    public partial class WorkoutPage : ContentPage
    {
        public WorkoutPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Exercises");
            ConfigureListView(out ExerciseList, DB.conn.Table<Exercise>().ToList());
            ExerciseList.ItemTapped += OnExerciseTapped;
            ExFrame.Content = ExerciseList;
        }

        private async void OnExerciseTapped(object sender, ItemTappedEventArgs e)
        {
            ListView listView = (ListView)sender;
            Exercise ex = (Exercise)listView.SelectedItem;
            ExercisePage ep = new ExercisePage(ex);
            await Navigation.PushAsync(ep, true);
        }

        private async void APIButtonClicked(System.Object sender, System.EventArgs e)
        {
            await Browser.OpenAsync("https://rapidapi.com/justin-WFnsXH_t6/api/exercisedb/details");
        }

        public static void ConfigureListView<T>(out ListView lv, List<T> exercises)
        {
            lv = new ListView();
            lv.ItemsSource = exercises;

            lv.ItemTemplate = new DataTemplate(() => {
                Label nameLabel = new Label();
                nameLabel.SetBinding(Label.TextProperty, "Name");
                nameLabel.FontSize = 12;
                nameLabel.TextColor = Color.Black;
                nameLabel.FontAttributes = FontAttributes.Bold;
                nameLabel.TextTransform = TextTransform.Uppercase;
                nameLabel.VerticalOptions = LayoutOptions.CenterAndExpand;

                Label targetLabel = new Label();
                targetLabel.SetBinding(Label.TextProperty, "TargetString");
                targetLabel.FontSize = 10;
                targetLabel.FontAttributes = FontAttributes.Bold;
                targetLabel.TextColor = Color.Red;
                targetLabel.VerticalOptions = LayoutOptions.CenterAndExpand;

                LinearGradientBrush gb = new LinearGradientBrush();
                gb.StartPoint = new Point(0, 0);
                gb.EndPoint = new Point(1, 1);
                gb.GradientStops.Add(new GradientStop(Color.White, 0.0f));
                gb.GradientStops.Add(new GradientStop(Color.FromHex("#35c5d5"), 0.9f));

                Grid g = new Grid
                {
                    Background = gb,
                    RowDefinitions = { new RowDefinition { Height = new GridLength(1, GridUnitType.Star) } },
                    ColumnDefinitions = {   new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)}}
                };
                g.Children.Add(targetLabel, 1, 0);
                g.Children.Add(nameLabel, 0, 0);

                return new ViewCell
                {
                    View = g
                };
            });

        }
    }
}
