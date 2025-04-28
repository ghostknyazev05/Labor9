using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Section
{
    /// <summary>
    /// Класс для проверки корректности ввода вещественных чисел.
    /// </summary>
    public static class InputValidator
    {
        public static bool IsValidDouble(string input, out double value)
        {
            return double.TryParse(input, out value);
        }
    }
}
