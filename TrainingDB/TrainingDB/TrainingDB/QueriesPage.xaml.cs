using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrainingDB {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QueriesPage : ContentPage {
		public QueriesPage() {
			InitializeComponent();
		}

		private void HourOrMore (object sender, EventArgs e)
		{
			TimeSpan hour = new TimeSpan(1, 0, 0);
			lv.ItemsSource = from activity in DB.conn.Table<Activity>()
							 where activity.Sport.Equals("Running") && activity.Duration >= hour
							 select activity;
		}

		private void DateHourOrMore(object sender, EventArgs e)
		{
			TimeSpan hour = new TimeSpan(1, 0, 0);
			lv.ItemsSource = from activity in DB.conn.Table<Activity>()
							 where activity.Sport.Equals("Running") && activity.Duration >= hour
							 select activity.DatePerformed;
		}

		private void DateDurationHourOrMore(object sender, EventArgs e)
		{
			TimeSpan hour = new TimeSpan(1, 0, 0);
			lv.ItemsSource = from activity in DB.conn.Table<Activity>()
							 where activity.Sport.Equals("Running") && activity.Duration >= hour
							 select new Tuple<string, string>(activity.DatePerformed.ToString(), activity.Duration.ToString());
		}

		private void LongActivities(object sender, EventArgs e)
		{
			var dur = DB.conn.Table<LongActivityDefinition>().SingleOrDefault(s => s.Sport.Equals("Running"));
			lv.ItemsSource = from activity in DB.conn.Table<Activity>()
							 where activity.Sport.Equals("Running") && activity.Duration >= dur.Duration
							 select activity;
		}

		private void FiveHundredPlusCals(object sender, EventArgs e)
		{
			var list = DB.conn.Table<Activity>().ToList();
			List<Activity> acts = new List<Activity>();
			foreach(Activity activity in list)
            {
				if (activity.Calories >= 500)
					acts.Add(activity);
            }
			lv.ItemsSource = acts;
		}
	}
}