using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Media;
using Microsoft.Win32;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MP3_Player
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer player = new MediaPlayer();
        private List<string> selectedFiles = new List<string>();
        private int currentFileIndex = 0; // Добавляем переменную для отслеживания файла

        private Random random = new Random();
        private TimeSpan pausePosition;
        private DispatcherTimer timer;
        public int k = 0;




        public MainWindow()
        {
            InitializeComponent();

            
            // Привязываем события изменения громкости и позиции к их обработчикам
            volume.ValueChanged += Volume_ValueChanged;
            Time.ValueChanged += Time_ValueChanged;
            player.MediaOpened += Player_Open;
            player.MediaEnded += Player_Ended;
            
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(240);
            timer.Tick += Timer_Tick;



            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (player.Source != null && player.NaturalDuration.HasTimeSpan)
            {
                Time.Value = player.Position.TotalMilliseconds; // Обновляем значение слайдера в соответствии с текущей позицией воспроизведения
                TimeSpan timeSpan = TimeSpan.FromMilliseconds(player.Position.TotalMilliseconds);
                MS.Text = $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
            }
        }

        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Устанавливаем громкость в соответствии со значением слайдера
            player.Volume = volume.Value/ 10;
        }

        private void Time_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int SliderValue = (int)Time.Value;
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            player.Position = ts;
        }

        private void Player_Open(object sender, EventArgs e)
        {
            // Устанавливаем максимальное значение слайдера времени в соответствии с общей длительностью аудиозаписи
            Time.Maximum = player.NaturalDuration.TimeSpan.TotalMilliseconds;
            MS.Text = player.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
        }

        private void PlayAudio(string audioFile)
        {
           
            player.Open(new Uri(audioFile, UriKind.Relative));
            player.Play();
            timer.Start();
            SystemSounds.Beep.Play(); 
        }

        private void Player_Ended(object sender, EventArgs e)
        {
            if (Sel.IsChecked == true)
            {
                int selectedAudio = ListBox.SelectedIndex;
                selectedAudio++;
                player.Stop();
                
                Time.Value = 0;
                if (selectedAudio < selectedFiles.Count)
                {
                    PlayAudio(selectedFiles[selectedAudio]);
                }
            }
            else
            {
                player.Stop();
                k++;
                Time.Value = 0;
                if (k < selectedFiles.Count)
                {
                    PlayAudio(selectedFiles[k]);
                }
            }
              
            
            SystemSounds.Exclamation.Play();


        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
           
            //рандом
            if (rnd.IsChecked == true)
            {
                int n = selectedFiles.Count;
                while (n > 1)
                {
                    n--;
                    int k = random.Next(n + 1);
                    string value = selectedFiles[k];
                    selectedFiles[k] = selectedFiles[n];
                    selectedFiles[n] = value;
                }
                PlayAudio(selectedFiles[n]);  
            }

            //по списку
            if (obo.IsChecked == true)
            {
                PlayAudio(selectedFiles[k]);
            }
            
           // выбранный
            if (Sel.IsChecked == true)
            {
                int selectedAudio = ListBox.SelectedIndex;
                PlayAudio(selectedFiles[selectedAudio]);
            }
            
            // если  ничего не выбрано то двигаемся по списку
            if (rnd.IsChecked == false && obo.IsChecked == false && Sel.IsChecked == false)
            {
                PlayAudio(selectedFiles[k]);
            }

            if (pausePosition != null)
            {
                player.Position = (TimeSpan)pausePosition;
            }

            
            SystemSounds.Asterisk.Play();
            pausePosition = TimeSpan.Zero;
            


        }

        


        private void Pauze_Click(object sender, RoutedEventArgs e)
        {
            if (player.Source != null && player.NaturalDuration.HasTimeSpan)
            {
                if (player.Position == player.NaturalDuration.TimeSpan) // Проверяем, достигнут ли конец аудиозаписи
                {
                    player.Stop();
                }
                else
                {
                    pausePosition = player.Position; // Сохраняем текущую позицию воспроизведения
                    player.Pause();
                    timer.Stop(); 
                }
                SystemSounds.Asterisk.Play();
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            Time.Value = 0;
            pausePosition = TimeSpan.Zero;
            SystemSounds.Asterisk.Play();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                selectedFiles.Clear();
                ListBox.Items.Clear();
                foreach (string file in openFileDialog.FileNames)
                {
                    selectedFiles.Add(file);
                    ListBox.Items.Add(System.IO.Path.GetFileName(file));
                }
               
                SystemSounds.Asterisk.Play();
            }
            
        }


    }

}
