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

namespace Section
{
    /// <summary>
    /// Главное окно приложения для работы с отрезком.
    /// Позволяет создавать отрезок вручную или случайно, выполнять различные операции над отрезком
    /// (вычисление длины, инкремент, преобразование типов, добавление значения, проверка принадлежности точки).
    /// </summary>
    public partial class MainWindow : Window
    {
        private LineSegment _segment;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateRandomSegment(object sender, RoutedEventArgs e)
        {
            // Генерация двух чисел, где первое всегда меньше второго
            (double x, double y) = RandomGenerator.GenerateDouble(-10, 10);

            // Создаем новый отрезок
            _segment = new LineSegment(x, y);
            UpdateSegmentInfo();

            // Отображаем значения в текстовых полях
            XBox.Text = x.ToString("F2");
            YBox.Text = y.ToString("F2");
        }

        private void CreateSegment(object sender, RoutedEventArgs e)
        {
            if (InputValidator.IsValidDouble(XBox.Text, out double x) &&
                InputValidator.IsValidDouble(YBox.Text, out double y))
            {
                _segment = new LineSegment(x, y);
                UpdateSegmentInfo();
            }
            else
            {
                MessageBox.Show("Введите корректные значения X и Y!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CalculateLength(object sender, RoutedEventArgs e)
        {
            if (_segment == null)
            {
                MessageBox.Show("Отрезок не создан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            double length = !_segment;
            MessageBox.Show($"Длина отрезка: {length:F3}", "Результат");
        }

        private void IncrementSegment(object sender, RoutedEventArgs e)
        {
            if (_segment == null)
            {
                MessageBox.Show("Отрезок не создан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _segment++;
            UpdateSegmentInfo();
        }

        private void CastToInt(object sender, RoutedEventArgs e)
        {
            if (_segment == null)
            {
                MessageBox.Show("Отрезок не создан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int xValue = (int)_segment;
            MessageBox.Show($"X в int: {xValue}", "Результат");
        }

        private void CastToDouble(object sender, RoutedEventArgs e)
        {
            if (_segment == null)
            {
                MessageBox.Show("Отрезок не создан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            double yValue = _segment;
            MessageBox.Show($"Y в double: {yValue:F3}", "Результат");
        }

        private void AddValue(object sender, RoutedEventArgs e)
        {
            if (_segment == null)
            {
                MessageBox.Show("Отрезок не создан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (InputValidator.IsValidDouble(AddValueBox.Text, out double value))
            {
                _segment = _segment + (int)value;
                UpdateSegmentInfo();
            }
            else
            {
                MessageBox.Show("Введите корректное число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CheckPoint(object sender, RoutedEventArgs e)
        {
            if (_segment == null)
            {
                MessageBox.Show("Отрезок не создан!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (InputValidator.IsValidDouble(CheckPointBox.Text, out double point))
            {
                if (_segment.Contains(point))
                {
                    MessageBox.Show("Точка принадлежит отрезку.", "Результат");
                }
                else
                {
                    MessageBox.Show("Точка НЕ принадлежит отрезку.", "Результат");
                }
            }
            else
            {
                MessageBox.Show("Введите корректное число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void UpdateSegmentInfo()
        {
            SegmentInfo.Text = _segment.ToString();
        }
    }
}
