using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsole
{
    public class TimeData
    {
        [JsonProperty("countryCode")]
        public string countryCode { get; set; }
        [JsonProperty("countryName")]
        public string countryName { get; set; }
        [JsonProperty("zoneName")]
        public string zoneName { get; set; }
        [JsonProperty("gmtOffset")]
        public int gmtOffSet { get; set; }
        //[JsonProperty("timestamp")]
        public int blash { get; set; }
    }

    public class AllTimeData
    {
        [JsonProperty("zones")]
        public TimeData[] times { get; set; }
    }
}
