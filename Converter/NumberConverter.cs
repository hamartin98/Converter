using System;
using System.Windows;

namespace Converter
{
    class NumberConverter
    {
        private const string hexLookup = "0123456789ABCDEF"; // Lookup table for number conversion
        private const string romanLookup = "IVXLCDM"; // Lookup table for roman characters, ordered by value

        // Converts a character to int value eg.: C -> 12
        // Returns the integer value of the character
        private static int CharToValue(char ch)
        {
            return (ch >= '0' && ch <= '9') ? Convert.ToInt32(ch - 48) : Convert.ToInt32(ch - 55);
        }

        // Convert and return the given number in base 10
        // The number must be in Big-endian format
        private static int ConvertToBase10(string number, int fromBase)
        {
            int result = 0;
            int currValue = 0;

            for (int idx = 0; idx < number.Length; idx++)
            {
                currValue = CharToValue(number[idx]); // get the integer value of the current character
                result += Convert.ToInt32(currValue * Math.Pow(fromBase, idx)); // Add the current subresult to the result
            }

            return result;
        }

        // Converts the number between the given bases
        // Returns the converted value formatted
        public static string ConvertNumber(string number, int fromBase, int toBase)
        {
            string result = "";
            int base10Num = ConvertToBase10(number, fromBase);

            do
            {
                result += hexLookup[base10Num % toBase];
                base10Num /= toBase;
            }
            while (base10Num > 0);

            return FormatString(result);
        }

        // Converts the given number to Roman number
        public static string ConvertToRomanNumber(string number, int fromBase)
        {
            // needs to be implemented
            return "";
        }

        // Removes unnecessary spaces from the string
        // Invert and convert the characters in the string to uppper case
        // Returns the formatted string
        public static string FormatString(string str)
        {
            string result = "";
            str = str.Trim();

            for (int idx = str.Length - 1; idx >= 0; idx--)
            {
                result += char.ToUpper(str[idx]);
            }

            return result;
        }

        // Checks if the given value is a valid number in the selected base
        // Returns true if its correct
        public static bool IsNumberValidInBase(string number, int baseOf)
        {
            string validChars = hexLookup.Substring(0, baseOf);

            foreach (char ch in number)
            {
                if (!validChars.Contains(ch))
                {
                    MessageBox.Show("Number is invalid in the given base");
                    return false;
                }
            }

            return true;
        }
    }
}
