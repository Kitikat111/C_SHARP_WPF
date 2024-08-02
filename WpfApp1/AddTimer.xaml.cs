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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddTimer.xaml
    /// </summary>
    public partial class AddTimer : Window
    {
        public DateTime Date; //введенное дата-время - публичная переменная для доступа из основной формм
        public string TName; //название даты

        public int hourss;
        public int min;
        public int sec;
        

        public AddTimer()
        {
            InitializeComponent();
        }

        //закрытие окна
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            //убеждаемся в корректности введенного времени
            int hours, minutes, seconds;
            if (int.TryParse(Hour.Text, out hours) && hours >= 0 && hours <= 23)
            {
                if (int.TryParse(Minute.Text, out minutes) && minutes >= 0 && minutes <= 59)
                {
                    if (int.TryParse(Second.Text, out seconds) && seconds >= 0 && seconds <= 59)
                    {
                        // все данные корректны
                        if (calendar.SelectedDate.HasValue)
                        {
                            DateTime selectedDate = calendar.SelectedDate.Value; //дата из календаря
                            //в текущую дату записываем дату + время
                            this.Date = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, hours, minutes, seconds);
                            this.TName = Name.Text; //считываем название
                            this.DialogResult = true;

                            hourss = int.Parse(Hour.Text);
                            min = int.Parse(Minute.Text);
                            sec = int.Parse(Second.Text);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, введите корректное значение для секунд (0-59).");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, введите корректное значение для минут (0-59).");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное значение для часов (0-23)");
                return;
            }

            


        }
    }
}


