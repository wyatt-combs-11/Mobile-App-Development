using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DBIntro
{
    public partial class MyPage : ContentPage
    {
        SQLiteConnection conn;
        public MyPage()
        {
            InitializeComponent();
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, "Personnel.db");
            conn = new SQLiteConnection(fname);
            conn.CreateTable<User>();
            conn.CreateTable<Person>();
            lv.ItemsSource = conn.Table<User>().ToList();
            lv2.ItemsSource = conn.Table<Person>().ToList();
        }
        private void AddUser(object sender, EventArgs e)
        {
            User newUser = new User { Username = name.Text };
            conn.Insert(newUser);
            lv.ItemsSource = conn.Table<User>().ToList();
        }

        private void AddPerson(object sender, EventArgs e)
        {
            Person newPerson = new Person { Name = perName.Text, Gender = gender.IsChecked, DOB = dob.Date, SSN = ssn.Text, Income = Int32.Parse(income.Text) };
            conn.Insert(newPerson);
            lv2.ItemsSource = conn.Table<Person>().ToList();
        }

        private void ClearAll(object sender, EventArgs e)
        {
            conn.DeleteAll<User>();
            conn.DeleteAll<Person>();
            lv.ItemsSource = conn.Table<User>().ToList();
            lv2.ItemsSource = conn.Table<Person>().ToList();
        }
    }
}
