using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;


namespace Tag
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int gridSize;
        private Button[,] buttons;

        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(gridSizeInput.Text, out int inputSize))
            {
                gridSize = inputSize;
                StartNewGame(gridSize);
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное число для размера поля.", "Ошибка", MessageBoxButton.OK);
            }
        }
       
        private void Game()
        {
            buttons = new Button[gridSize, gridSize];

            UniformGrid uniformGrid = new UniformGrid();
            uniformGrid.Rows = gridSize +1;
            uniformGrid.Columns = gridSize;
            Random random = new Random();
            HashSet<int> usedNumbers = new HashSet<int>();

            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    int currentRow = row;
                    int currentCol = col;

                    buttons[row, col] = new Button();
                    if (row != gridSize - 1 || col != gridSize - 1)
                    {
                        int randomNumber;
                        do
                        {
                          randomNumber = random.Next(1, gridSize * gridSize);
                        } while (usedNumbers.Contains(randomNumber));

                        usedNumbers.Add(randomNumber);
                        buttons[row, col].Content = randomNumber.ToString();
                        buttons[row, col].Background = Brushes.LightGreen;
                        buttons[row, col].FontSize = 36;
                        buttons[row, col].FontFamily = new FontFamily("MV Boli");
                    }
                    else
                    {
                        buttons[row, col].Content = "";
                        buttons[row, col].Background = Brushes.LightGreen;
                        buttons[row, col].FontSize = 36;
                    }                    
                    
                    buttons[row, col].Click += (sender, e) => MoveTile(currentRow, currentCol);
                    uniformGrid.Children.Add(buttons[row, col]);                   
                }
            }
            
            Button restartButton = new Button();           
            restartButton.Content = "Restart";           
            restartButton.FontSize = 32;
            restartButton.FontFamily = new FontFamily("MV Boli");
            restartButton.Background = Brushes.LightPink;
            uniformGrid.Children.Add(restartButton);
            restartButton.Click += RestartButton_Click;

           Content = uniformGrid;
        }
       
        private void StartNewGame(int size)
        {
            gridSize = size;
            buttons = new Button[gridSize, gridSize];
            Game();
        }
        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(gridSize);
        }
        private void CheckWin()
        {
            bool win = true;
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    if (row == gridSize - 1 && col == gridSize - 1)
                    {
                        if (buttons[row, col].Content.ToString() != "")
                        {
                            win = false;
                            break;
                        }
                    }
                    else
                    {
                        if (buttons[row, col].Content.ToString() != $"{row * gridSize + col + 1}")
                        {
                            win = false;
                            break;
                        }
                    }
                }
                if (!win)
                {
                    break;
                }
            }
            if (win)
            {
                MessageBox.Show("Поздравляем, вы победили!", "Победа", MessageBoxButton.OK);
            }
        }
        private void MoveTile(int row, int col)
        {            
            if (row > 0 && buttons[row - 1, col].Content.ToString() == "") // Верхняя клетка
            {
                int selectedNumber = int.Parse(buttons[row, col].Content.ToString());
                buttons[row, col].Content = "";
                buttons[row - 1, col].Content = selectedNumber.ToString();
                CheckWin();
                return;
            }
            if (row < gridSize - 1 && buttons[row + 1, col].Content.ToString() == "") // Нижняя клетка
            {
                int selectedNumber = int.Parse(buttons[row, col].Content.ToString());
                buttons[row, col].Content = "";
                buttons[row + 1, col].Content = selectedNumber.ToString();
                CheckWin();
                return;
            }
            if (col > 0 && buttons[row, col - 1].Content.ToString() == "") // Левая клетка
            {
                int selectedNumber = int.Parse(buttons[row, col].Content.ToString());
                buttons[row, col].Content = "";
                buttons[row, col - 1].Content = selectedNumber.ToString();
                CheckWin();
                return;
            }
            if (col < gridSize - 1 && buttons[row, col + 1].Content.ToString() == "") // Правая клетка
            {
                int selectedNumber = int.Parse(buttons[row, col].Content.ToString());
                buttons[row, col].Content = "";
                buttons[row, col + 1].Content = selectedNumber.ToString();
                CheckWin();
                return;
            }           
        }
    }
}


