using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WorkLog
{
    public partial class TotalsPage : ContentPage
    {
        public class Anon
        {
            public string Key { get; set; }
            public string Total { get; set; }
            public int YearMonth { get; set; }
        }

        public TotalsPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Back");
            ResetTotalsList();
            TotalsList.ItemTapped += OnMonthTotals;
        }
        MonthTotals mt;
        double[] totals;

        private async void OnMonthTotals(object sender, ItemTappedEventArgs e)
        {
            ListView listView = (ListView)sender;
            Anon obj = (Anon)listView.SelectedItem;
            mt = new MonthTotals(Int32.Parse(obj.Key));
            await Navigation.PushAsync(mt, true);
        }

        private void ResetTotalsList()
        {
            var table = MainPage.conn.Table<Activity>().ToList();
            ConfigureListView(out TotalsList, ( from a in table
                                                group a by a.Date.Year into dategroup
                                                select new Anon { Key = "" + dategroup.Key, Total = (dategroup.Sum(s => s.Hours.TotalHours) + " hours") }).ToList());
            totals =    (from a in table
                        group a by a.Date.Year into dategroup
                        select dategroup.Sum(s => s.Hours.TotalHours)).ToArray();

            view1.InvalidateSurface();
            years = new Grid
            {
                RowDefinitions = { new RowDefinition { Height = 20 } },
                ColumnDefinitions = {   new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)}}
            };
            years.Children.Add(new Label { Text = "2021", FontSize = 15, HorizontalOptions = LayoutOptions.CenterAndExpand }, 1, 0);
            years.Children.Add(new Label { Text = "2022", FontSize = 15, HorizontalOptions = LayoutOptions.CenterAndExpand }, 3, 0);
            years.Children.Add(new Label { Text = "2023", FontSize = 15, HorizontalOptions = LayoutOptions.CenterAndExpand }, 5, 0);

            Content = new StackLayout
            {
                Children =
                {
                    TotalsList,
                    view1,
                    years
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
                    HeightRequest = 40,
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

        public void OnCanvas1ViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            var table = MainPage.conn.Table<Activity>().ToList();
            totals =    (from a in table
                         group a by a.Date.Year into dategroup
                         select dategroup.Sum(s => s.Hours.TotalHours)).ToArray();

            Debug.WriteLine("Painted");
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
