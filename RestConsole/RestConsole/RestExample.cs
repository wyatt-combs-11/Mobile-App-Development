using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RestConsole.Helpers;

namespace RestConsole
{
	public class RestExample
	{
		/*
		 * Documentation on weather API: https://openweathermap.org/current
		 * You will need to get your own keys:
		 * https://home.openweathermap.org/users/sign_up
		 * https://timezonedb.com/register
		 */

		public static string OpenTimeZoneEndpoint = "http://api.timezonedb.com/v2.1/list-time-zone";
		// YOU WILL NEED TO REGISTER FROM YOUR OWN KEY AND INSERT THAT KEY INTO secrets.json
		public static string OpenTimeZoneAPIKey = Secrets.APIKEY;
		public HttpClient client = new HttpClient();
		public string CreateTimeQuery(string timeZone)
		{
			string requestUri = OpenTimeZoneEndpoint;
			requestUri += $"?zone={timeZone}";
			requestUri += $"&key={OpenTimeZoneAPIKey}";
			requestUri += "&format=json";
			return requestUri;
		}
		public async Task<string> GetTimeQueryResult()
		{
			Console.Write("Please enter a time zone: ");
			string timeZone = Console.ReadLine().Trim();
			string query = CreateTimeQuery(timeZone);
			string result = null;

			try
			{
				var response = await client.GetAsync(query);
				if (response.IsSuccessStatusCode)
				{
					result = await response.Content.ReadAsStringAsync();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("\t\tERROR {0}", ex.Message);
				Environment.Exit(0);
			}

			return result;
		}
		public void ProcessTimeQuery()
		{
			string response = GetTimeQueryResult().Result;
			AllTimeData atd = JsonConvert.DeserializeObject<AllTimeData>(response);
			Console.WriteLine("Using TimeData class");
			foreach (var t in atd.times)
			{
				Console.WriteLine(JsonConvert.SerializeObject(t));
			}

			//var smallDef = new { name = "", visibility = 0 };
			//var smallObj = JsonConvert.DeserializeAnonymousType(response, smallDef);
			//Console.WriteLine("\nUsing small anonymous class");
			//Console.WriteLine(smallObj.name + " " + smallObj.visibility);
		}
		public static void Main(string[] args)
		{
			//Console.WriteLine(WithinTriangle(new int[] { 1, 7 }, new int[] { 2, 4 }, new int[] { 9, 3 }, new int[] { 6, 5 }));
			Console.WriteLine(ThreeSum(new int[] { -1, 0, 1, 2, -1, -4 }));
		}

		public static bool WithinTriangle(int[] p1, int[] p2, int[] p3, int[] test)
		{
			return InsideLine(p1, p2, p3, test) &&
							InsideLine(p2, p3, p1, test) &&
							InsideLine(p3, p1, p2, test);
		}

		private static bool InsideLine(int[] p1, int[] p2, int[] p3, int[] test)
		{
			int dir1, dir2;
			double slope = ((p2[1]-p1[1]) * 1.0)/(p2[0]-p1[0]);
			double test3y = p2[1] + (p3[0]-p2[0])*slope;
			double test4y = p2[1] + (test[0]-p2[0])*slope;
			Console.WriteLine(test3y + "," + p3[1]);
			Console.WriteLine(test4y + "," + test[1]);
			if (p3[1] < test3y)
				dir1 = -1;
			else
				dir1 = 1;
			if (test[1] < test4y)
				dir2 = -1;
			else
				dir2 = 1;

			Console.WriteLine(dir1);
			Console.WriteLine(dir2);

			return dir1 == dir2;
		}
		private static IList<IList<int>> ThreeSum(int[] nums)
		{
			List<List<int>> ret = new List<List<int>>();
			if (nums.Length < 3)
				return ret as IList<IList<int>>;
			for (int i = 0; i < nums.Length-2; i++)
			{
				for (int j = i+1; j < nums.Length-1; j++)
				{
					for (int k = j+1; k < nums.Length; k++)
					{
						Console.WriteLine(nums[i] + "," + nums[j] + "," + nums[k]);
						if (nums[i] + nums[j] + nums[k] == 0 && !ret.Contains(new List<int>(new int[] { nums[i], nums[j], nums[k] })))
						{
							Console.WriteLine("T");
							List<int> ins = new List<int>(new int[] { nums[i], nums[j], nums[k] });
							foreach (int num in ins) Console.Write(num);
							ret.Add(new List<int>(new int[] { i, j, k }));
							Console.WriteLine("ret length: " + ret.Count);
						}
					}
				}
			}

			return ret as IList<IList<int>>;
		}
	}
}