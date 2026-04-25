using Microsoft.Win32;
using slidingpuzzlebeginner;
using System.Windows;

namespace SlidingPuzzleGUI
{
    public partial class MainWindow : Window
    {
        private string imagePath = "";
        private string musicPath = "";

        public MainWindow()
        {
            InitializeComponent();

            // Önceden müzik seçilmişse, menüye dönünce textte göster
            if (MusicManager.CurrentMusicPath != "")
            {
                musicPath = MusicManager.CurrentMusicPath;
                TxtMusic.Text = musicPath;
            }
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (dlg.ShowDialog() == true)
            {
                imagePath = dlg.FileName;
                TxtImage.Text = imagePath;
            }
        }

        private void SelectMusic_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Audio Files|*.mp3;*.wav;*.wma";

            if (dlg.ShowDialog() == true)
            {
                musicPath = dlg.FileName;
                TxtMusic.Text = musicPath;

                MusicManager.Play(musicPath);
            }
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            if (musicPath != "")
            {
                MusicManager.Play(musicPath);
            }

            GameWindow game = new GameWindow(imagePath);
            game.Show();
            this.Close();
        }
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MusicManager.SetVolume(VolumeSlider.Value);
        }

        private void MuteButton_Click(object sender, RoutedEventArgs e)
        {
            MusicManager.ToggleMute();

            if (MusicManager.IsMuted())
                MuteButton.Content = "Sesi Aç";
            else
                MuteButton.Content = "Sesi Kapat";
        }
    }
}