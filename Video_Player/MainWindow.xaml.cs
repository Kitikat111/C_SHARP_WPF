using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Video_Player
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
     public partial class MainWindow : Window
    {
        
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            player.MediaOpened += MediaOpened;
            player.MediaEnded += MediaEnded;

            volume.ValueChanged += Volume_ValueChanged;
            Time.ValueChanged += Time_ValueChanged;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(240); // Устанавливаем интервал в 1 миллисекунду
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (player.Source != null && player.NaturalDuration.HasTimeSpan)
            {
                Time.Value = player.Position.TotalMilliseconds; // Обновляем значение слайдера в соответствии с текущей позицией воспроизведения
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                player.Source = new Uri(openFileDialog.FileName);
            }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
            timer.Start();
        }

        private void Pauze_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
        }

        private void MediaOpened(object sender, RoutedEventArgs e)
        {
            Time.Maximum = player.NaturalDuration.TimeSpan.TotalMilliseconds;
            
        }

        private void MediaEnded(object sender, RoutedEventArgs e)
        {
            player.Stop();
        }
        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Устанавливаем громкость в соответствии со значением слайдера
            player.Volume = volume.Value / 10;
        }

        private void Time_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int SliderValue = (int)Time.Value;
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            player.Position = ts;
        }
    }


}

