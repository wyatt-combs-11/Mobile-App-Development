using System;
using SQLite;

namespace WorkLog
{
    [Preserve(AllMembers = true)]
    [Table("activities")]
    public class Activity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string JobName { get; set; }
        public TimeSpan Hours { get; set; }
        public bool OddHours { get; set; }
        public DateTime Date { get; set; }
        public string ShortDate { get { return Date.ToShortDateString(); } }
        public string ShortHours { get { return Math.Round(Hours.TotalHours,2) + " " + (OddHours? "Odd": "Normal") + " Hours Worked"; } }
        public string Source {
            get {
                return (Hours.Hours >= 4)?
                        "Tough.jpg"
                    :   "https://cdn-icons-png.flaticon.com/512/4456/4456893.png";
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1} ,{2}, {3}", JobName, Hours.TotalHours, OddHours ? "Odd": "Regular", Date.ToShortDateString());
        }
    }
}
