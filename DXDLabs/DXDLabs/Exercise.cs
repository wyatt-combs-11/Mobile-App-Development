using Newtonsoft.Json;
using SQLite;

namespace DXDLabs
{
    [Table("Exercises")]
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [JsonProperty("bodyPart")]
        public string BodyPart { get; set; }
        [JsonProperty("gifUrl")]
        public string GifUrl { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("target")]
        public string Target { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public bool Favorited { get; set; }
        public string TargetString
        {
            get
            {
                return "Main Muscle: " + BodyPart;
            }
        }
    }
}
