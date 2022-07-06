using System;
using System.Collections.Generic;
using System.Threading;
using Xamarin.Forms;

namespace DXDLabs
{
    public partial class ExercisePage : ContentPage
    {
        public Exercise exercise;
        public bool IsPlaying { get; set; }
        public ExercisePage(Exercise ex)
        {
            exercise = ex;
            InitializeComponent();
            BindingContext = this;
            DeclareElements();
            ExerciseGif.SetValue(Image.SourceProperty, new Uri(exercise.GifUrl));
            Device.StartTimer(new TimeSpan(0,0,0,0,50), CheckPlayHeight);
        }

        private void DeclareElements()
        {
            ExerciseName.Text = exercise.Name;
            ExerciseTarget.Text = "Main Muscle: " + exercise.Target;
            RefreshView();

            AddButton.Clicked += (s, e) =>
            {
                DB.conn.Insert(new FavoritedExercise
                {
                    BodyPart = exercise.BodyPart,
                    Name = exercise.Name,
                    GifUrl = exercise.GifUrl,
                    Target = exercise.Target
                });
                exercise.Favorited = true;
                DB.conn.Update(exercise);
                AddButton.IsEnabled = false;
                AddButton.Text = "Added!";
            };
        }

        private void Button_Clicked(object s, EventArgs e)
        {
            ExerciseGif.IsAnimationPlaying = false;
            ExerciseGif.IsAnimationPlaying = true;
            ExerciseGif.IsVisible = false;
            ExerciseGif.IsVisible = true;
        }

        private bool CheckPlayHeight()
        {
            if (Play.Y > Height * .5)
            {
                Play.IsVisible = false;
                return false;
            }
            return true;
        }

        private void RefreshView()
        {
            exercise = DB.conn.Table<Exercise>().FirstOrDefault(c => c.Name == exercise.Name);
            if (exercise.Favorited)
            {
                AddButton.IsEnabled = false;
                AddButton.Text = "Added!";
            } else
            {
                AddButton.IsEnabled = true;
                AddButton.Text = "Favorite Exercise";
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshView();
        }
    }
}
