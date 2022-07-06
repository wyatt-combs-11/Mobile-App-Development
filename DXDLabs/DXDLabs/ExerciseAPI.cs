using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DXDLabs
{
    public class ExerciseAPI
    {
        public static List<Exercise> exercises;
        public static string endpoint = "https://exercisedb.p.rapidapi.com/exercises";
        private static string APIKey = "09f896f142mshf6d06a306e9d11fp165098jsneeabc7818588";

        public static async Task CreateQueryStringAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(endpoint),
                Headers =
                {
                    { "X-RapidAPI-Key", APIKey },
                    { "X-RapidAPI-Host", "exercisedb.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                exercises = JsonConvert.DeserializeObject<List<Exercise>>(body);
            }
        }
    }
}
