using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static WorkLog.TotalsPage;

namespace WorkLog
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthTotals : ContentPage
    {
        public MonthTotals(int year)
        {
            Year = year;
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Back");
            ResetTotalsList();
            MonthTotalsList.ItemTapped += OnMonthTotals;
        }
        public int Year { get; set; }
        public string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
        MonthActivities ma;
        double[] totals;

        private async void OnMonthTotals(object sender, ItemTappedEventArgs e)
        {
            ListView listView = (ListView)sender;
            Anon obj = (Anon)listView.SelectedItem;
            ma = new MonthActivities(Year, obj.YearMonth);
            await Navigation.PushAsync(ma, true);
        }

        private void ResetTotalsList()
        {
            var table = MainPage.conn.Table<Activity>().ToList();
            ConfigureListView(out MonthTotalsList, (from a in table
                                               where a.Date.Year == Year
                                               group a by a.Date.Month into dategroup
                                               select new Anon { Key = "" + months[dategroup.Key - 1] + " " + Year, YearMonth = dategroup.Key, Total = (dategroup.Sum(s => s.Hours.TotalHours) + " hours") }).ToList());

            view1.InvalidateSurface();
            monthsLabels = new Grid
            {
                RowDefinitions = { new RowDefinition { Height = 20 } },
                ColumnDefinitions = {       new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)}}
            };
            for (int i = 0; i < 12; i++)
            {
                monthsLabels.Children.Add(new Label { Text = months[i].Substring(0,1), FontSize = 8, HorizontalOptions = LayoutOptions.CenterAndExpand }, 2 * i + 1, 0);
            }

            Content = new StackLayout
            {
                Children =
                {
                    MonthTotalsList,
                    view1,
                    monthsLabels
                }
            };
        }

        public void ConfigureListView(out ListView lv, List<Anon> activities)
        {
            lv = new ListView();
            lv.ItemsSource = activities;

            lv.ItemTemplate = new DataTemplate(() => {
                Label jobLabel = new Label();
                jobLabel.SetBinding(Label.TextProperty, "Key");
                jobLabel.FontSize = 20;
                jobLabel.TextColor = Color.White;
                jobLabel.VerticalOptions = LayoutOptions.Center;

                Label dateLabel = new Label();
                dateLabel.SetBinding(Label.TextProperty, "Total");
                dateLabel.FontSize = 20;
                dateLabel.VerticalOptions = LayoutOptions.Center;

                Grid g = new Grid
                {
                    BackgroundColor = Color.DarkGray,
                    RowDefinitions = { new RowDefinition { Height = new GridLength(1, GridUnitType.Star) } },
                    ColumnDefinitions = {   new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)}}
                };
                g.Children.Add(jobLabel, 0, 0);
                g.Children.Add(dateLabel, 1, 0);

                return new ViewCell
                {
                    View = g
                };
            });

        }

        private void view1_PaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            var table = MainPage.conn.Table<Activity>().ToList();
            totals =    (from a in table
                         where a.Date.Year == Year
                         group a by a.Date.Month into dategroup
                         select dategroup.Sum(s => s.Hours.TotalHours)).ToArray();

            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Red.ToSKColor(),
                StrokeWidth = 3,
                IsStroke = false
            };

            float numItems = 1 / (2.0f * totals.Length + 1);
            float max = (float)totals.Max();
            for (int i = 0; i < totals.Length; i++)
            {
                float num = (float)totals[i];
                float perc = (num / max) * info.Height;
                canvas.DrawRect(info.Width * (2 * i + 1) * numItems, info.Height - perc, info.Width * numItems, perc, paint);
            }
        }
    }
}