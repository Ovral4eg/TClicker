using Assets.Clicker.Scripts.Utils.Enums;
using System;
using System.Drawing;

namespace Assets.Clicker.Scripts.Utils
{
    public static class StringHelper
    {
        public static string GetColorById(ColorId id)
        {
            string result = null;
            switch(id)
            {
                case ColorId.purple: result = "#750075"; break;
                case ColorId.red: result = "#750000"; break;

                case ColorId.black:
                default: result = "#000000" ; break;

            }
            return result;
        }

        private static string[] letters = new string[3 * 26 + 7];
        public static void CalculateLetters()
        {
            letters[0] = "";
            letters[1] = "";
            letters[2] = "k";
            letters[3] = "m";
            letters[4] = "b";
            letters[5] = "t";
            letters[6] = "q";
            var charCount = 7;
            for (char x = 'a'; x <= 'c'; x++)
            {
                for (char y = 'a'; y <= 'z'; y++)
                {
                    letters[charCount++] = $"{x}{y}";
                }
            }
        }

        public static string GetStringFromValue(double value)
        {
            int log = (int)Math.Log(value, 1000) + 1;
            double log2 = log - 1;

            if (log > 1)
            {
                var newValue = value / Math.Pow(1000, log2);

                if (letters.Length > log)
                {
                    return $"{Math.Round(newValue, 2)}{letters[log]}";
                }
            }

            return $"{Math.Round(value,2)}";
        }
    }
}
