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

        public static double GenerateDouble(double minValue, double maxValue)
        {
            return _random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}
