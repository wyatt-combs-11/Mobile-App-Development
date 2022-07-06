using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise___Zipcodes_from_Webservice
{
    public class Zipcode
    {
        [JsonProperty("post code")]
        public int Zip { get; set; }
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
        [JsonProperty("place name")]
        public string Placename { get; set; }
        public override string ToString()
        {
            return Placename + ", " + Zip + "(" + Latitude + ", " + Longitude + ")";
        }
    }

    public class Places
    {
        [JsonProperty("places")]
        public Zipcode[] Zipcodes { get; set; }
    }
}
