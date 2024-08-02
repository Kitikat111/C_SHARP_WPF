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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _2D
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Line hours;
        Line minutes;
        Line seconds;
        public MainWindow()
        {
            InitializeComponent();
            // Создание объекта DispatcherTimer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Установка интервала обновления в 1 секунду
            timer.Tick += Timer_Tick; // Добавление обработчика события Tick
            timer.Start();

            
            Line hours = new Line();
            hours.Stroke = System.Windows.Media.Brushes.LightGray;
            hours.X1 = 250;
            hours.Y1 = 250;
            hours.X2 = 250;
            hours.Y2 = 120;
            hours.StrokeThickness = 10;
            scene.Children.Add(hours);

         
            Line minutes = new Line();
            minutes.Stroke = System.Windows.Media.Brushes.LightGray;
            minutes.X1 = 250;
            minutes.Y1 = 250;
            minutes.X2 = 250;
            minutes.Y2 = 100;
            minutes.StrokeThickness = 8;
            scene.Children.Add(minutes);

            Line seconds = new Line();
            seconds.Stroke = System.Windows.Media.Brushes.LightGray;
            seconds.X1 = 250;
            seconds.Y1 = 250;
            seconds.X2 = 250;
            seconds.Y2 = 70;
            seconds.StrokeThickness = 5;
            scene.Children.Add(seconds);
            
            
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Удаление существующих линий
            scene.Children.Remove(hours);
            scene.Children.Remove(minutes);
            scene.Children.Remove(seconds);

            // Получение текущего времени
            DateTime currentTime = DateTime.Now;

            // Расчет углов поворота стрелок часов, минут и секунд
            double Lseconds = (currentTime.Second / 60.0) * 360;
            double Lminutes = ((currentTime.Minute + currentTime.Second / 60.0) / 60.0) * 360;
            double Lhours = ((currentTime.Hour % 12 + currentTime.Minute / 60.0) / 12.0) * 360;

            // Создание новых линий с обновленными координатами
            seconds = new Line();
            seconds.Stroke = System.Windows.Media.Brushes.Black;
            seconds.X1 = 250;
            seconds.Y1 = 250;
            seconds.X2 = 250 + 180 * Math.Sin(Lseconds * (Math.PI / 180));
            seconds.Y2 = 250 - 180 * Math.Cos(Lseconds * (Math.PI / 180));
            seconds.StrokeThickness = 5;
            scene.Children.Add(seconds);

            minutes = new Line();
            minutes.Stroke = System.Windows.Media.Brushes.Black;
            minutes.X1 = 250;
            minutes.Y1 = 250;
            minutes.X2 = 250 + 150 * Math.Sin(Lminutes * (Math.PI / 180));
            minutes.Y2 = 250 - 150 * Math.Cos(Lminutes * (Math.PI / 180));
            minutes.StrokeThickness = 8;
            scene.Children.Add(minutes);

            hours = new Line();
            hours.Stroke = System.Windows.Media.Brushes.Black;
            hours.X1 = 250;
            hours.Y1 = 250;
            hours.X2 = 250 + 100 * Math.Sin(Lhours * (Math.PI / 180));
            hours.Y2 = 250 - 100 * Math.Cos(Lhours * (Math.PI / 180));
            hours.StrokeThickness = 10;
            scene.Children.Add(hours);
        }





    }
}
