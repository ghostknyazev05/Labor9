using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Section
{
    /// <summary>
    /// Класс для работы с отрезком на координатной прямой.
    /// </summary>
    public class LineSegment
    {
        private double _x;
        private double _y;

        public double X { get => _x; }

        public double Y { get => _y; }

        private const double MinValue = -10000000000;
        private const double MaxValue = 10000000000;

        /// <summary>
        /// Конструктор для создания отрезка.
        /// </summary>
        /// <param name="x">Начальная точка отрезка</param>
        /// <param name="y">Конечная точка отрезка</param>
        /// <exception cref="ArgumentException">Исключение выбрасывается, если X и Y равны или выходят за пределы допустимого диапазона.</exception>
        public LineSegment(double x, double y)
        {
            // Проверка на равенство X и Y
            if (x == y)
            {
                throw new ArgumentException("X и Y не могут быть равны.");
            }

            // Установим точки отрезка в правильном порядке (по возрастанию)
            if (x < y)
            {
                _x = x;
                _y = y;
            }
            else
            {
                _x = y;
                _y = x;
            }

            // Проверка на пределы
            if (_x < MinValue || _x > MaxValue || _y < MinValue || _y > MaxValue)
            {
                throw new ArgumentOutOfRangeException($"Значения отрезка выходят за пределы диапазона: {MinValue} до {MaxValue}.");
            }
        }

        /// <summary>
        /// Проверяет, принадлежит ли точка отрезку.
        /// </summary>
        /// <param name="point">Точка для проверки</param>
        /// <returns>Возвращает true, если точка принадлежит отрезку, иначе false.</returns>
        public bool Contains(double point)
        {
            return point >= _x && point <= _y;
        }

        /// <summary>
        /// Возвращает длину отрезка.
        /// </summary>
        /// <param name="segment">Отрезок, длину которого нужно вычислить</param>
        /// <returns>Длину отрезка</returns>
        public static double operator !(LineSegment segment)
        {
            return Math.Abs(segment.Y - segment.X);
        }

        /// <summary>
        /// Операция увеличения отрезка на 1.
        /// </summary>
        /// <param name="segment">Отрезок, к которому прибавляется 1</param>
        /// <returns>Изменённый отрезок</returns>
        public static LineSegment operator ++(LineSegment segment)
        {
            segment._x += 1;
            segment._y += 1;
            return segment;
        }

        /// <summary>
        /// Операция приведения отрезка к типу int.
        /// </summary>
        /// <param name="segment">Отрезок</param>
        /// <returns>Целочисленное значение координаты X отрезка</returns>
        public static explicit operator int(LineSegment segment)
        {
            return (int)segment.X;
        }

        /// <summary>
        /// Операция приведения отрезка к типу double.
        /// </summary>
        /// <param name="segment">Отрезок</param>
        /// <returns>Значение координаты Y отрезка</returns>
        public static implicit operator double(LineSegment segment)
        {
            return segment.Y;
        }

        /// <summary>
        /// Операция сложения отрезка с числом.
        /// </summary>
        /// <param name="segment">Отрезок</param>
        /// <param name="d">Число для прибавления</param>
        /// <returns>Новый отрезок с обновлёнными координатами</returns>
        /// <exception cref="InvalidOperationException">Если результат сложения выходит за пределы диапазона</exception>
        public static LineSegment operator +(LineSegment segment, double d)
        {
            double newX = segment.X + d;
            double newY = segment.Y + d;

            if (newX < MinValue || newX > MaxValue || newY < MinValue || newY > MaxValue)
            {
                throw new InvalidOperationException($"Результат сложения выходит за пределы допустимого диапазона: {MinValue} до {MaxValue}.");
            }

            return new LineSegment(newX, newY);
        }

        /// <summary>
        /// Операция сложения числа с отрезком.
        /// </summary>
        /// <param name="d">Число для прибавления</param>
        /// <param name="segment">Отрезок</param>
        /// <returns>Новый отрезок с обновлёнными координатами</returns>
        /// <exception cref="InvalidOperationException">Если результат сложения выходит за пределы диапазона</exception>
        public static LineSegment operator +(double d, LineSegment segment)
        {
            double newX = segment.X + d;
            double newY = segment.Y + d;

            if (newX < MinValue || newX > MaxValue || newY < MinValue || newY > MaxValue)
            {
                throw new InvalidOperationException($"Результат сложения выходит за пределы допустимого диапазона: {MinValue} до {MaxValue}.");
            }

            return new LineSegment(newX, newY);
        }

        /// <summary>
        /// Строковое представление отрезка.
        /// </summary>
        /// <returns>Строка, представляющая отрезок</returns>
        public override string ToString()
        {
            return $"Отрезок: [{_x.ToString("F3", new System.Globalization.CultureInfo("ru-RU"))} ; {_y.ToString("F3", new System.Globalization.CultureInfo("ru-RU"))}]";
        }
    }
}
