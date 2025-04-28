using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section
{
    /// <summary>
    /// Класс для работы с отрезком на координатной прямой.
    /// </summary>
    public class LineSegment
    {
        // Приватные поля координат начала и конца отрезка
        private double _x;
        private double _y;

        // Публичные свойства для доступа к координатам
        public double X { get => _x; }
        public double Y { get => _y; }

        /// <summary>
        /// Конструктор отрезка. Координаты упорядочиваются: X <= Y.
        /// </summary>
        public LineSegment(double x, double y)
        {
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
        }

        /// <summary>
        /// Проверяет, принадлежит ли точка отрезку.
        /// </summary>
        public bool Contains(double point)
        {
            return point >= _x && point <= _y;
        }

        /// <summary>
        /// Перегрузка оператора "!" для вычисления длины отрезка.
        /// </summary>
        public static double operator !(LineSegment segment)
        {
            return Math.Abs(segment.Y - segment.X);
        }

        /// <summary>
        /// Перегрузка унарного оператора "++" для увеличения координат на 1.
        /// </summary>
        public static LineSegment operator ++(LineSegment segment)
        {
            segment._x += 1;
            segment._y += 1;
            return segment;
        }

        /// <summary>
        /// Перегрузка явного преобразования в int (берется X).
        /// </summary>
        public static explicit operator int(LineSegment segment)
        {
            return (int)segment.X;
        }

        /// <summary>
        /// Перегрузка неявного преобразования в double (берется Y).
        /// </summary>
        public static implicit operator double(LineSegment segment)
        {
            return segment.Y;
        }

        /// <summary>
        /// Перегрузка бинарного оператора "+" для увеличения координат на число.
        /// </summary>
        public static LineSegment operator +(LineSegment segment, int d)
        {
            return new LineSegment(segment.X + d, segment.Y + d);
        }

        /// <summary>
        /// Перегрузка обратного сложения (int + LineSegment).
        /// </summary>
        public static LineSegment operator +(int d, LineSegment segment)
        {
            return new LineSegment(segment.X + d, segment.Y + d);
        }

        /// <summary>
        /// Переопределение метода ToString().
        /// </summary>
        public override string ToString()
        {
            return $"Отрезок: [{_x:F3}, {_y:F3}]";
        }
    }
}
