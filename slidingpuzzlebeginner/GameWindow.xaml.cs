using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SlidingPuzzleGUI
{
    public partial class GameWindow : Window
    {
        private int[,] board = new int[3, 3];
        private Button[,] buttons = new Button[3, 3];

        private string bgImage;

        private DispatcherTimer timer = new DispatcherTimer();
        private int seconds = 0;

        public GameWindow(string image)
        {
            InitializeComponent();

            bgImage = image;

            SetBackground();
            CreateButtons();
            Shuffle();
            UpdateUI();
            StartTimer();
        }

        private void SetBackground()
        {
            if (bgImage != "")
            {
                try
                {
                    ImageBrush brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri(bgImage, UriKind.Absolute));
                    brush.Stretch = Stretch.UniformToFill;
                    GameArea.Background = brush;
                }
                catch
                {
                    MessageBox.Show("Resim yüklenemedi.");
                }
            }
        }

        private void StartTimer()
        {
            seconds = 0;
            TimerText.Text = "Süre: 0";

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            seconds++;
            TimerText.Text = "Süre: " + seconds;
        }

        private void CreateButtons()
        {
            GameGrid.Children.Clear();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button btn = new Button();
                    btn.FontSize = 24;
                    btn.Click += Tile_Click;

                    btn.Background = Brushes.Transparent;
                    btn.BorderBrush = Brushes.Gold;
                    btn.BorderThickness = new Thickness(2);
                    btn.Foreground = Brushes.White;

                    buttons[i, j] = btn;
                    GameGrid.Children.Add(btn);
                }
            }
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Content == null || btn.Content.ToString() == "")
                return;

            int number = int.Parse(btn.Content.ToString());

            if (Move(number))
            {
                UpdateUI();

                if (IsSolved())
                {
                    timer.Stop();

                    MessageBox.Show("Kazandın! Süre: " + seconds);

                    MainWindow menu = new MainWindow();
                    menu.Show();
                    this.Close();
                }
            }
        }

        private bool Move(int number)
        {
            int numRow = -1;
            int numCol = -1;
            int zeroRow = -1;
            int zeroCol = -1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == number)
                    {
                        numRow = i;
                        numCol = j;
                    }

                    if (board[i, j] == 0)
                    {
                        zeroRow = i;
                        zeroCol = j;
                    }
                }
            }

            if ((Math.Abs(numRow - zeroRow) == 1 && numCol == zeroCol) ||
                (Math.Abs(numCol - zeroCol) == 1 && numRow == zeroRow))
            {
                board[zeroRow, zeroCol] = number;
                board[numRow, numCol] = 0;
                return true;
            }

            return false;
        }

        private void UpdateUI()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 0)
                    {
                        buttons[i, j].Content = "";
                        buttons[i, j].Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        buttons[i, j].Content = board[i, j].ToString();
                        buttons[i, j].Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void Shuffle()
        {
            Random rnd = new Random();
            int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

            do
            {
                for (int i = arr.Length - 1; i > 0; i--)
                {
                    int j = rnd.Next(i + 1);

                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
            while (!Solvable(arr) || IsSolvedArray(arr));

            int index = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = arr[index];
                    index++;
                }
            }
        }

        private bool Solvable(int[] arr)
        {
            int inv = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] != 0 && arr[j] != 0 && arr[i] > arr[j])
                    {
                        inv++;
                    }
                }
            }

            return inv % 2 == 0;
        }

        private bool IsSolvedArray(int[] arr)
        {
            int[] solved = { 1, 2, 3, 4, 5, 6, 7, 8, 0 };

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != solved[i])
                    return false;
            }

            return true;
        }

        private bool IsSolved()
        {
            int count = 1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == 2 && j == 2)
                        return board[i, j] == 0;

                    if (board[i, j] != count)
                        return false;

                    count++;
                }
            }

            return true;
        }
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            Shuffle();
            UpdateUI();

            seconds = 0;
            TimerText.Text = "Süre: 0";

            timer.Stop();
            timer.Start();
        }
    }
}