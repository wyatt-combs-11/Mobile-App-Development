using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DXDLabs
{
    public partial class AccountSettingsPage : ContentPage
    {
        public AccountSettingsPage()
        {
            InitializeComponent();
            RefreshListView();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshListView();
            if(Preferences.ContainsKey("username"))
            {
                Username.Text = Preferences.Get("username", "");
                Password.Text = Preferences.Get("password", "");
            }
        }

        private void RefreshListView()
        {
            WorkoutPage.ConfigureListView(out FavoriteEx, DB.conn.Table<FavoritedExercise>().ToList());
            FavFrame.Content = FavoriteEx;
        }

        private void LogInClicked(object s, EventArgs e)
        {
            Preferences.Set("username", Username.Text);
            Preferences.Set("password", Password.Text);
        }

        private void LogOutClicked(object s, EventArgs e)
        {
            Preferences.Remove("username");
            Preferences.Remove("password");
            Username.Text = "";
            Password.Text = "";
        }

        private void RemoveClicked(object s, EventArgs e)
        {
            if (FavoriteEx.SelectedItem != null)
            {
                FavoritedExercise fx = (FavoritedExercise)FavoriteEx.SelectedItem;
                Exercise ex = DB.conn.Table<Exercise>().FirstOrDefault(c => c.Name == fx.Name);
                ex.Favorited = false;
                DB.conn.Update(ex);
                DB.conn.Delete(fx);
                RefreshListView();
            }
        }
    }
}
