using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System;
using System.Windows;

namespace Section
{
    /// <summary>
    /// Основное окно приложения, которое позволяет работать с отрезком.
    /// </summary>
    public partial class MainWindow : Window
    {
        private LineSegment currentSegment;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Создаёт новый отрезок на основе введённых значений.
        /// </summary>
        private void CreateSegment(object sender, RoutedEventArgs e)
        {
            try
            {
                if (double.TryParse(XBox.Text, out double x) && double.TryParse(YBox.Text, out double y))
                {
                    currentSegment = new LineSegment(x, y);
                    SegmentInfo.Text = currentSegment.ToString();
                    MessageBox.Show("Отрезок успешно создан.");
                }
                else
                {
                    throw new FormatException("Введите корректные значения для X и Y.");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Генерирует случайный отрезок.
        /// </summary>
        private void GenerateRandomSegment(object sender, RoutedEventArgs e)
        {
            try
            {
                Random rnd = new Random();
                double x = Math.Round(rnd.NextDouble() * 100, 3);
                double y = Math.Round(x + rnd.NextDouble() * 100, 3);

                currentSegment = new LineSegment(x, y);
                XBox.Text = x.ToString("F3");
                YBox.Text = y.ToString("F3");
                SegmentInfo.Text = currentSegment.ToString();

                MessageBox.Show("Случайный отрезок сгенерирован.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при генерации случайного отрезка: {ex.Message}");
            }
        }

        /// <summary>
        /// Вычисляет и отображает длину отрезка.
        /// </summary>
        private void CalculateLength(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentSegment != null)
                {
                    double length = !currentSegment;
                    MessageBox.Show($"Длина отрезка: {length:F3}");
                }
                else
                {
                    throw new InvalidOperationException("Сначала создайте отрезок.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Увеличивает отрезок на 1.
        /// </summary>
        private void AddOneToSegment(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentSegment != null)
                {
                    currentSegment++;
                    SegmentInfo.Text = currentSegment.ToString();
                    MessageBox.Show("К отрезку прибавлено 1.");
                }
                else
                {
                    throw new InvalidOperationException("Сначала создайте отрезок.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Преобразует значение X в тип int и отображает результат.
        /// </summary>
        private void CastToInt(object sender, RoutedEventArgs e)
        {
            try
            {
                if (double.TryParse(XBox.Text, out double value))
                {
                    int intValue = (int)value;
                    XBox.Text = intValue.ToString();
                    MessageBox.Show("Значение X преобразовано в int.");
                }
                else
                {
                    throw new FormatException("Введите корректное число для X.");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Преобразует значение Y в тип double с округлением до 3 знаков после запятой.
        /// </summary>
        private void CastToDouble(object sender, RoutedEventArgs e)
        {
            try
            {
                string input = YBox.Text;

                if (double.TryParse(input, out double result))
                {
                    result = Math.Round(result, 3);
                    YBox.Text = result.ToString("F3");
                    MessageBox.Show("Значение Y преобразовано в double с 3 знаками после запятой.");
                }
                else
                {
                    throw new FormatException("Введите корректное число для Y.");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Добавляет указанное значение к отрезку, с проверкой на превышение допустимых значений.
        /// </summary>
        private void AddValue(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentSegment == null)
                {
                    throw new InvalidOperationException("Сначала создайте отрезок.");
                }

                if (!double.TryParse(AddValueBox.Text, out double value))
                {
                    throw new FormatException("Введите корректное число для добавления.");
                }

                double newX = currentSegment.X + value;
                double newY = currentSegment.Y + value;

                if (Math.Abs(newX) > 10_000_000_000 || Math.Abs(newY) > 10_000_000_000)
                {
                    throw new InvalidOperationException("Результат выходит за пределы ±10 млрд. Операция отменена.");
                }

                currentSegment = new LineSegment(newX, newY);
                SegmentInfo.Text = currentSegment.ToString();
                MessageBox.Show($"К отрезку добавлено значение {value}.");
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Проверяет, принадлежит ли точка отрезку.
        /// </summary>
        private void CheckPoint(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentSegment == null)
                {
                    throw new InvalidOperationException("Сначала создайте отрезок.");
                }

                if (double.TryParse(CheckPointBox.Text, out double point))
                {
                    bool result = currentSegment.Contains(point);
                    string message = result ? "Точка принадлежит отрезку." : "Точка не принадлежит отрезку.";
                    MessageBox.Show(message);
                }
                else
                {
                    throw new FormatException("Введите корректную точку.");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
