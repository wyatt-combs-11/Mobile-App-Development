using SQLite;
using System;

namespace DBIntro
{
    [Table("Person")]
    public class Person
    {
        // Id (int), Name (String), Gender (bool), DOB (DateTime), and SSN (String)
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public bool Gender { get; set; }
        public DateTime DOB { get; set; }
        [Unique]
        public string SSN { get; set; }
        public int Income { get; set; }
        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", Id, Name, Gender ? "Male": "Female", DOB.ToShortDateString(), SSN, Income);
        }
    }
}
