using System;
using System.Windows.Media;

namespace SlidingPuzzleGUI
{
    public static class MusicManager
    {
        private static MediaPlayer player = new MediaPlayer();
        private static string currentMusicPath = "";
        private static bool mediaEndedAttached = false;

        private static double volume = 0.5;
        private static bool isMuted = false;

        public static string CurrentMusicPath
        {
            get { return currentMusicPath; }
        }

        public static void Play(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            if (currentMusicPath == path)
                return;

            currentMusicPath = path;

            try
            {
                player.Open(new Uri(path, UriKind.Absolute));

                if (!mediaEndedAttached)
                {
                    player.MediaEnded += Player_MediaEnded;
                    mediaEndedAttached = true;
                }

                ApplyVolume();
                player.Play();
            }
            catch
            {
                currentMusicPath = "";
            }
        }

        private static void Player_MediaEnded(object sender, EventArgs e)
        {
            player.Position = TimeSpan.Zero;
            player.Play();
        }

        public static void SetVolume(double value)
        {
            volume = value;
            ApplyVolume();
        }

        public static void ToggleMute()
        {
            isMuted = !isMuted;
            ApplyVolume();
        }

        private static void ApplyVolume()
        {
            if (isMuted)
                player.Volume = 0;
            else
                player.Volume = volume;
        }

        public static bool IsMuted()
        {
            return isMuted;
        }

        public static double GetVolume()
        {
            return volume;
        }
    }
}