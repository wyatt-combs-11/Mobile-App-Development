using System;
using System.IO;
using System.Reflection;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DXDLabs
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DB.OpenConnection();
            MainPage = new MainPage();
        }

        private void PlaySound(string file)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            String thisNameSpace = "DXDLabs";
            Stream audioStream = assembly.GetManifestResourceStream(thisNameSpace + "." + file);
            ISimpleAudioPlayer player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            player.Load(audioStream);
            player.Play();
        }

        protected override void OnStart()
        {
            PlaySound("Ronnie.mp3");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
