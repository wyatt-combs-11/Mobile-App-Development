using System;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Essentials;

namespace DXDLabs
{
    public class DB
    {
        public static SQLiteConnection conn;

        public static void OpenConnection()
        {
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, "DXDLabs.db");
            conn = new SQLiteConnection(fname);
            conn.CreateTable<Exercise>();
            if (conn.Table<Exercise>().Count() < 1)
            {
                Task res = ExerciseAPI.CreateQueryStringAsync();
                res.Wait();
                foreach (Exercise e in ExerciseAPI.exercises)
                    conn.Insert(e);
            }
            conn.CreateTable<FavoritedExercise>();
        }
    }
}
