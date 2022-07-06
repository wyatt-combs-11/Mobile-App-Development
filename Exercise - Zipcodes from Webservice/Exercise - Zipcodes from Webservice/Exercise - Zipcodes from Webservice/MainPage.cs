using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Exercise___Zipcodes_from_Webservice
{
	public class MainPage : ContentPage
    {
        public HttpClient client = new HttpClient();
        Entry state, city;
        Button b;
        ListView lv;
        public MainPage()
        {
            state = new Entry { HorizontalOptions = LayoutOptions.StartAndExpand };
            city = new Entry { WidthRequest = 100 };
            b = new Button { Text = "Go", WidthRequest = 50};
            b.Clicked += (s, e) =>
            {
                ResetListView();
            };

            lv = new ListView { VerticalOptions = LayoutOptions.FillAndExpand};

            Content = new StackLayout
            {
                Padding = new Thickness(25),
                Children = {
                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            Children =
                            {
                                new Label { Text = "State"},
                                state
                            }
                        },
                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                new Label { Text = "City"},
                                city
                            }
                        },
                        b,
                        lv
                    }
            };
        }

        private void ResetListView()
        {
            try
            {
                string ret = GetQueryResult().Result;
                Places p = JsonConvert.DeserializeObject<Places>(ret);
                lv.ItemsSource = p.Zipcodes.ToList();
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public string CreateQuery(string state, string city)
        {
            string requestUri = "https://api.zippopotam.us/us/";
            requestUri += $"{state}/{city}";
            Console.WriteLine(requestUri);
            return requestUri;
        }
        public async Task<string> GetQueryResult()
        {
            string query = CreateQuery(state.Text, city.Text);
            string result = null;

            try
            {
                var response = await client.GetAsync(query).ConfigureAwait(false);
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

            Console.WriteLine(result);
            return result;
        }
    }
}