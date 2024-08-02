using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        
        Dictionary<string, DateTime> dateList = new Dictionary<string, DateTime>();

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

       
        public MainWindow()
        {
            InitializeComponent();
        }

        

        //обработчик таймера
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            UpdateTimeLeft();
        }

        //обновить оставшееся время выбранной даты
        void UpdateTimeLeft()
        {
            if (listBox.SelectedIndex != -1)
            {
                KeyValuePair<string, DateTime> selectedPair = dateList.ElementAt(listBox.SelectedIndex);
                DateTime selectedDateTime = selectedPair.Value;
                
                //получаем разницу во времени для таймера
                TimeSpan ts = selectedDateTime - DateTime.Now;
                
                //выбираем формат для вывода
                string formattedTime = "";
                if (rb1 != null && rb1.IsChecked == true) //дни минуты часы секунды
                {
                    formattedTime = $"{ts.Days} д/ {ts.Hours} ч/ {ts.Minutes} м/ {ts.Seconds} с";
                }
                else if (rb2 != null && rb2.IsChecked == true) //дни минуты часы секунды
                {
                    formattedTime = $"{(int)ts.TotalHours} ч/ {ts.Minutes} м/ {ts.Seconds} с";
                }
                else if (rb3 != null && rb3.IsChecked == true) //дни минуты часы секунды
                {
                    formattedTime = $"{(int)ts.TotalMinutes} м/ {ts.Seconds} с";
                }
                else //просто секунды
                {
                    formattedTime = (int)ts.TotalSeconds + " c";
                }
                textBoxTimeLeft.Text = formattedTime;
            }
            else
                dispatcherTimer.Stop(); //останавливаем таймер
        }

        //обновить список дат на форме
        public void UpdateListBox()
        {
            listBox.Items.Clear();
            foreach (var pair in dateList)
            {
                string formattedDate = $"{pair.Key} - {pair.Value.ToString("dd.MM.yyyy HH:mm")}";
                listBox.Items.Add(formattedDate);
            }
        }

        private void buttonAddTimer_Click(object sender, RoutedEventArgs e)
        {
            //создание нового окна 
            AddTimer timerWnd = new AddTimer();
            //вызов окна + проверка, отработало ли окно корректно
            if (timerWnd.ShowDialog() == true)
            {
                //из свойств окна получаем имя и дату
                DateTime date = timerWnd.Date;
                string name = timerWnd.TName;
                //заносим в словарь
                dateList.Add(name, date);
                UpdateListBox(); //обновляем на форме
            }
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //выбор даты в списке
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTimeLeft();
            dispatcherTimer.Start();
        }

        //удалить выбранное событие
        private void buttonRemoveTimer_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                KeyValuePair<string, DateTime> selectedPair = dateList.ElementAt(listBox.SelectedIndex);
                dateList.Remove(selectedPair.Key); //удаляем по имени события
                textBoxTimeLeft.Text = "";
                UpdateListBox();
            }
        }


        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text  (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (var pair in dateList)
                    {
                        writer.WriteLine($"{pair.Key}!{pair.Value.ToString("yyyy-MM-dd HH:mm:ss")}");
                    }
                }
            }
        }

        private void laodButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                if (File.Exists(fileName))
                {
                    
                    string[] lines = File.ReadAllLines(fileName);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('!'); 
                        if (parts.Length == 2)
                        {
                            string eventName = parts[0];
                            if (DateTime.TryParseExact(parts[1], "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime eventDate))
                            {
                                dateList.Add(eventName, eventDate);
                                UpdateListBox();
                            }
                        }
                    }
                }                
            }
            
        }


        private void UpdateMenuItem_Click(object sender, RoutedEventArgs e)
        {
            UpdateListBox();
        }

        //редактирование выбранного таймера
        private void izm_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                AddTimer add = new AddTimer();

                KeyValuePair<string, DateTime> selectedPair = dateList.ElementAt(listBox.SelectedIndex);
                DateTime selectedDateTime = selectedPair.Value;

                // Устанавливаем значения времени и названия из выбранного элемента списка
                add.Name.Text = selectedPair.Key;
                add.Hour.Text = selectedDateTime.Hour.ToString();
                add.Minute.Text = selectedDateTime.Minute.ToString();
                add.Second.Text = selectedDateTime.Second.ToString();
                add.calendar.SelectedDate = selectedDateTime.Date;

                add.ShowDialog();

                if (add.DialogResult == true)
                {
                    string name = add.TName;
                    DateTime dt = add.Date;

                    dateList.Add(name, dt);
                    listBox.Items.Add(name);

                    dateList.Remove(selectedPair.Key);

                    // Обновляем ListBox для отражения изменений
                    UpdateListBox();
                }
                else
                {
                    System.Windows.MessageBox.Show("Всё сломалось", "ОШИБКА!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

