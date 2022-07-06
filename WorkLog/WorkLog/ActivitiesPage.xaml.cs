using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace WorkLog
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum |
                AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property |
                AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
    public sealed class PreserveAttribute : Attribute
    {
        public bool AllMembers;
        public bool Conditional;
    }

    public partial class ActivitiesPage : ContentPage
    {
        public ObservableCollection<Activity> activities = new ObservableCollection<Activity>();
        ListView activeList;
        public ActivitiesPage()
        {
            foreach (var a in MainPage.conn.Table<Activity>().ToList())
                activities.Add(a);
            activeList = new ListView
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            ConfigureListView(out activeList, activities);
            activeList.ItemTapped += ActiveList_ItemTapped;
            InitializeComponent();
            ActivityLayout.Children.Add(activeList);
        }

        public void ConfigureListView(out ListView lv, ObservableCollection<Activity> activities)
        {
            lv = new ListView();
            lv.ItemsSource = activities;

            lv.ItemTemplate = new DataTemplate(() => {
                Label jobLabel = new Label();
                jobLabel.SetBinding(Label.TextProperty, "JobName");
                jobLabel.FontSize = 20;
                jobLabel.TextColor = Color.White;
                jobLabel.BackgroundColor = Color.DarkGray;

                Label dateLabel = new Label();
                dateLabel.SetBinding(Label.TextProperty, "ShortDate");
                dateLabel.FontSize = 10;

                Label hoursLabel = new Label();
                hoursLabel.SetBinding(Label.TextProperty, "ShortHours");
                hoursLabel.FontSize = 10;
                hoursLabel.TextColor = Color.DarkGreen;

                Grid g = new Grid
                {
                    RowDefinitions = { new RowDefinition { Height = new GridLength(1, GridUnitType.Star) } },
                    ColumnDefinitions = {   new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)}}
                };
                g.Children.Add(jobLabel, 0, 0);
                g.Children.Add(new StackLayout { Children = { dateLabel, hoursLabel }}, 1, 0);

                return new ViewCell
                {
                    View = g
                };
            });

        }

        void Add_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                Activity activity = new Activity
                {
                    JobName = Job.Text,
                    OddHours = CheckOdd.IsChecked,
                    Date = Date.Date,
                    Hours = new TimeSpan(Int32.Parse(Hours.Text), Int32.Parse(Minutes.Text), 0)
                };
                activities.Add(activity);
                MainPage.conn.Insert(activity);
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                DisplayAlert("Error","One or more of your inputs are invalid","Try again");
            }
        }

        void Update_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                Activity old = ((Activity)activeList.SelectedItem);
                Activity newActivity = new Activity
                {
                    Id = old.Id,
                    JobName = Job.Text,
                    OddHours = CheckOdd.IsChecked,
                    Date = Date.Date,
                    Hours = new TimeSpan(Int32.Parse(Hours.Text), Int32.Parse(Minutes.Text), 0)
                };
                MainPage.conn.Update(newActivity);
                activities.Insert(activities.IndexOf(old), newActivity);
                activities.Remove(old);
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                DisplayAlert("Error","One or more of your inputs are invalid","Try again");
            }
         }

        void Delete_Clicked(System.Object sender, System.EventArgs e)
        {
            Activity activity = activeList.SelectedItem as Activity;
            if (activity != null)
            {
                int v = MainPage.conn.Delete(activity);
                activities.Remove(activity);
                if (v > 0)
                {
                    activeList.SelectedItem = null;
                }
            }

        }

        private void ActiveList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Activity activity = (Activity)e.Item;
            if (activity != null)
            {
                Job.Text = activity.JobName;
                CheckOdd.IsChecked = activity.OddHours;
                Date.Date = activity.Date;
                Hours.Text = "" + activity.Hours.Hours;
                Minutes.Text = "" + activity.Hours.Minutes;
            }
        }
    }
}
