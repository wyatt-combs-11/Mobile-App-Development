using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkLog
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthActivities : ContentPage
    {
        public MonthActivities(int year, int month)
        {
            Year = year;
            Month = month;
            InitializeComponent();
            ResetTotalsList();
        }
        public int Year { get; set; }
        public int Month { get; set; }

        private void ResetTotalsList()
        {
            var table = MainPage.conn.Table<Activity>().ToList();
            ConfigureListView(out MonthActivitiesList, (from a in table
                                               where a.Date.Year == Year && a.Date.Month == Month
                                               select a).ToList());
            Content = MonthActivitiesList;
        }

        public void ConfigureListView(out ListView lv, List<Activity> activities)
        {
            lv = new ListView();
            lv.ItemsSource = activities;

            lv.ItemTemplate = new DataTemplate(() => {
                Label jobLabel = new Label();
                jobLabel.SetBinding(Label.TextProperty, "JobName");
                jobLabel.FontSize = 20;
                jobLabel.TextColor = Color.White;
                jobLabel.VerticalOptions = LayoutOptions.CenterAndExpand;

                Label dateLabel = new Label();
                dateLabel.SetBinding(Label.TextProperty, "ShortDate");
                dateLabel.FontSize = 10;

                Label hoursLabel = new Label();
                hoursLabel.SetBinding(Label.TextProperty, "ShortHours");
                hoursLabel.FontSize = 10;
                hoursLabel.TextColor = Color.DarkGreen;

                Image i = new Image {HeightRequest = 20};
                i.SetBinding(Image.SourceProperty, "Source");

                Grid g = new Grid
                {
                    BackgroundColor = Color.DarkGray,
                    RowDefinitions = { new RowDefinition { Height = new GridLength(1, GridUnitType.Star) } },
                    ColumnDefinitions = {   new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)}}
                };
                StackLayout leftStack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = {i, jobLabel}
                };
                g.Children.Add(leftStack, 0, 0);
                g.Children.Add(new StackLayout { Children = { dateLabel, hoursLabel } }, 1, 0);

                return new ViewCell
                {
                    View = g
                };
            });

        }
    }
}