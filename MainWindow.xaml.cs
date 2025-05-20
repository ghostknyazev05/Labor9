using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Section
{
    /// <summary>
    /// Основное окно приложения для работы с числовыми отрезками.
    /// Максимальное значение координаты — ±10 миллиардов.
    /// Формат ввода: до 10 цифр до запятой и до 3 знаков после.
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double MAX_VALUE = 10_000_000_000;
        private LineSegment currentSegment;

        public MainWindow()
        {
            InitializeComponent();
        }

        private double? TryParseValidNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            string pattern = @"^-?\d{1,10}([,.]\d{1,3})?$";
            if (!Regex.IsMatch(input, pattern))
                return null;

            string normalized = input.Replace(',', '.');

            if (!double.TryParse(normalized, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                return null;

            if (Math.Abs(value) > MAX_VALUE)
                return null;

            return value;
        }

        private void NumberBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            string currentText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            e.Handled = !Regex.IsMatch(currentText, @"^-?\d{0,10}([,.]\d{0,3})?$");
        }

        private void CreateSegment(object sender, RoutedEventArgs e)
        {
            double? x = TryParseValidNumber(XBox.Text);
            double? y = TryParseValidNumber(YBox.Text);

            if (x == null || y == null)
            {
                MessageBox.Show("Введите корректные значения для X и Y:\n— до 10 цифр до запятой\n— до 3 после\n— диапазон ±10 млрд.");
                return;
            }

            if (x == y)
            {
                currentSegment = null;
                SegmentInfo.Text = "Отрезок: нет";
                MessageBox.Show("X и Y не могут быть равны. Отрезок сброшен.");
                return;
            }

            currentSegment = new LineSegment(x.Value, y.Value);
            SegmentInfo.Text = currentSegment.ToString();
            MessageBox.Show("Отрезок успешно создан.");
        }

        private void GenerateRandomSegment(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            double x = Math.Round(rnd.NextDouble() * 100, 3);
            double y = Math.Round(x + rnd.NextDouble() * 100, 3);

            currentSegment = new LineSegment(x, y);
            XBox.Text = x.ToString("F3", CultureInfo.InvariantCulture);
            YBox.Text = y.ToString("F3", CultureInfo.InvariantCulture);
            SegmentInfo.Text = currentSegment.ToString();

            MessageBox.Show("Случайный отрезок сгенерирован.");
        }

        private void CalculateLength(object sender, RoutedEventArgs e)
        {
            if (currentSegment == null)
            {
                MessageBox.Show("Сначала создайте отрезок.");
                return;
            }

            double length = !currentSegment;
            MessageBox.Show($"Длина отрезка: {length:F3}");
        }

        private void AddOneToSegment(object sender, RoutedEventArgs e)
        {
            if (currentSegment == null)
            {
                MessageBox.Show("Сначала создайте отрезок.");
                return;
            }

            double newX = currentSegment.X + 1;
            double newY = currentSegment.Y + 1;

            if (Math.Abs(newX) > MAX_VALUE || Math.Abs(newY) > MAX_VALUE)
            {
                MessageBox.Show("Прибавление 1 приведёт к выходу за пределы ±10 млрд.");
                return;
            }

            currentSegment++;
            SegmentInfo.Text = currentSegment.ToString();
            MessageBox.Show("К отрезку прибавлено 1.");
        }

        private void CastToInt(object sender, RoutedEventArgs e)
        {
            if (TryParseValidNumber(XBox.Text) is double value)
            {
                XBox.Text = ((int)value).ToString();
                MessageBox.Show("Значение X преобразовано в int.");
            }
            else
            {
                MessageBox.Show("Введите корректное число для X.");
            }
        }

        private void CastToDouble(object sender, RoutedEventArgs e)
        {
            if (TryParseValidNumber(YBox.Text) is double result)
            {
                YBox.Text = result.ToString("F3", CultureInfo.InvariantCulture);
                MessageBox.Show("Значение Y преобразовано в double с 3 знаками после запятой.");
            }
            else
            {
                MessageBox.Show("Введите корректное число для Y.");
            }
        }

        private void AddValue(object sender, RoutedEventArgs e)
        {
            if (currentSegment == null)
            {
                MessageBox.Show("Сначала создайте отрезок.");
                return;
            }

            double? value = TryParseValidNumber(AddValueBox.Text);
            if (value == null)
            {
                MessageBox.Show("Введите число в пределах ±10 млрд (до 10 цифр до запятой, до 3 после).");
                return;
            }

            double newX = currentSegment.X + value.Value;
            double newY = currentSegment.Y + value.Value;

            if (Math.Abs(newX) > MAX_VALUE || Math.Abs(newY) > MAX_VALUE)
            {
                MessageBox.Show("Результат выходит за пределы ±10 млрд. Операция отменена.");
                return;
            }

            currentSegment = new LineSegment(newX, newY);
            SegmentInfo.Text = currentSegment.ToString();
            MessageBox.Show($"К отрезку добавлено значение {value.Value:F3}.");
        }

        private void CheckPoint(object sender, RoutedEventArgs e)
        {
            if (currentSegment == null)
            {
                MessageBox.Show("Сначала создайте отрезок.");
                return;
            }

            double? point = TryParseValidNumber(CheckPointBox.Text);
            if (point == null)
            {
                MessageBox.Show("Введите корректную точку (до 3 знаков после запятой, в диапазоне ±10 млрд).");
                return;
            }

            bool result = currentSegment.Contains(point.Value);
            string message = result ? "Точка принадлежит отрезку." : "Точка не принадлежит отрезку.";
            MessageBox.Show(message);
        }
    }
}
