using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Media;
using System;

namespace XamarinAudioSample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        MediaPlayer player;        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);            

            Button btnPlayAudio = (Button)FindViewById(Resource.Id.play_audio);
            Button btnStopAudio = (Button)FindViewById(Resource.Id.stop_audio);
            Button btnPauseAudio = (Button)FindViewById(Resource.Id.pause_audio);

            btnPlayAudio.Click += PlayAudio;
            btnStopAudio.Click += StopAudio;
            btnPauseAudio.Click += PauseAudio;
        }

        private void PauseAudio(object sender, EventArgs e)
        {    
            if(player != null && player.IsPlaying) player.Pause();
        }

        private void StopAudio(object sender, EventArgs e)
        {
            if (player != null && player.IsPlaying)
            {
                player.Stop();
                player.Release();
                player = null;                
            }
        }

        private void PlayAudio(object sender, EventArgs e)
        {
            if (player == null)
            {
                player = MediaPlayer.Create(this, Resource.Raw.noise);
                player.Start();
            }
            else
            {
                if (!player.IsPlaying)
                {
                    player.Start();
                }
            }
        }        

        protected override void OnPause()
        {
            base.OnPause();
            PauseAudio(this, new EventArgs());
        }        

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}