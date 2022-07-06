using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WorkLog
{
	public partial class MainPage : TabbedPage
	{
		public static SQLiteConnection conn;
		public MainPage()
		{
			DBGeneration();
			InitializeComponent();
		}

		private void DBGeneration()
		{
			string libFolder = FileSystem.AppDataDirectory;
			string fname = System.IO.Path.Combine(libFolder, "Personnel.db");
			conn = new SQLiteConnection(fname);
			conn.CreateTable<Activity>();
			conn.DeleteAll<Activity>();
			conn.CreateTable<Activity>();
			GenerateWorkData(2021, 3);
		}

		public static void GenerateWorkData(int startYear, int numYears)
		{
			Random rng = new Random(34);
			String[] sites = { "Walmart", "ACE", "Miami", "Dubois", "Chipolte" };
			for (int dy = 0; dy<numYears; dy++)
			{
				TimeSpan ts = new TimeSpan();
				int year = startYear + dy;
				for (int month = 1; month <= 12; month++)
				{
					int N = 3;
					if (month == 1 || year == 2021)
						N = 1;
					for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
					{
						int numJobs = rng.Next(N) + 1;
						DateTime date = new DateTime(year, month, day);
						if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
							numJobs = rng.Next(5) == 0 ? 1 : 0;
						for (int job = 0; job < numJobs; job++)
						{
							int hours = rng.Next(8);
							int minutes = rng.Next(4) * 15;
							String site = sites[rng.Next(sites.Length)];
							TimeSpan jobTime = new TimeSpan(hours, minutes, 0);
							bool isOdd = rng.Next(5) == 0 || (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);
							// Don’t print, but put it into a list (or DB).
							Activity activity = new Activity
							{
								JobName = site,
								Hours = jobTime,
								OddHours = isOdd,
								Date = date
							};
							conn.Insert(activity);
						}
					}
				}
			}

		}
	}
}
