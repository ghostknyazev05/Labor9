using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Section
{
    /// <summary>
    /// Класс для генерации случайных вещественных чисел в заданном диапазоне.
    /// </summary>
    public static class RandomGenerator
    {
        private static readonly Random _random = new Random();

        /// <summary>
        /// Генерирует два случайных числа, гарантируя, что первое всегда будет меньше второго.
        /// </summary>
        public static (double x, double y) GenerateDouble(double minValue, double maxValue)
        {
            // Генерация двух случайных чисел
            double x = _random.NextDouble() * (maxValue - minValue) + minValue;
            double y = _random.NextDouble() * (maxValue - minValue) + minValue;

            // Убедимся, что x всегда меньше y
            if (x > y)
            {
                // Если x больше, меняем их местами
                double temp = x;
                x = y;
                y = temp;
            }

            return (x, y);  // Возвращаем кортеж с двумя значениями
        }
    }
}
