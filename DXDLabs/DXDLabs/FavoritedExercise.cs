using Newtonsoft.Json;
using SQLite;

namespace DXDLabs
{
    [Table("FavoritedExercises")]
    public class FavoritedExercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string BodyPart { get; set; }
        public string GifUrl { get; set; }
        public string Name { get; set; }
        public string Target { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public string TargetString
        {
            get
            {
                return "Main Muscle: " + BodyPart;
            }
        }
    }
}
