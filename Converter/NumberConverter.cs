using System;

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
        private static long ConvertToBase10(string number, int fromBase)
        {
            long result = 0;
            int currValue = 0;

            for (int idx = 0; idx < number.Length; idx++)
            {
                currValue = CharToValue(number[idx]); // get the integer value of the current character
                try
                {
                    result += Convert.ToInt64(currValue * Math.Pow(fromBase, idx)); // Add the current subresult to the result
                }
                catch(OverflowException ex)
                {
                    return -1;
                }
            }

            return result;
        }

        // Converts the number between the given bases
        // Returns the converted value formatted
        public static string ConvertNumber(string number, int fromBase, int toBase)
        {
            string result = "";
            long base10Num = ConvertToBase10(number, fromBase);

            do
            {
                try
                {
                    result += hexLookup[(int)base10Num % toBase];
                }
                catch(IndexOutOfRangeException ex)
                {
                    return ex.Message;
                }

                base10Num /= toBase;
            }
            while (base10Num > 0);

            return FormatString(result);
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
            string validChars = hexLookup.Substring(0, baseOf); // list of valid characters in the given base

            foreach (char ch in number)
            {
                if (!validChars.Contains(ch))
                {
                    return false;
                }
            }

            return true;
        }
        
        // Converts the given number to Roman number
        public static string ConvertToRomanNumber(string value, int fromBase)
        {
            // Characters needed to represent numbers in the given range
            // > 1000   M         [6]
            // > 100    CDM [4][5][6]
            // > 10     XLC [2][3][4]
            // > 1      IVX [0][1][2]

            // eg.: 1223 = [6][4][4][2][2][0][0][0] (indexes in the roman numbers lookup table)
            /*
             * 1223 / 1000 = 1 -> M, rem = 223
             * 223 / 100 = 2 -> CC, rem 23
             * 23 / 10 = 2 -> XX, rem 3
             * 3 / 1 = 3 -> III
             * result = MCCXXIII
            */

            string result = "";
            int charIdx = 6; // index to track position in roman lookup table

            long number = ConvertToBase10(value, fromBase); // Convert the input to base 10 number

            if (number > 0 && number < 4000) // Roman numbers can only be between 0 and 4000
            {
                for (int divider = 1000; divider >= 1; divider /= 10) // Dividers: 1000, 100, 10, 1
                {
                    if ((int)number >= divider)
                    {
                        int current = (int)number / divider;
                        if (current < 4) // 1, 2, 3
                        {
                            for (int times = current; times > 0; times--)
                            {
                                result += romanLookup[charIdx]; // e.g. II
                            }
                        }
                        else if (current > 4 && current < 9) // 5, 6, 7, 8
                        {
                            result += romanLookup[1 + charIdx]; // e.g. V
                            for (int times = 0; times < current - 5; times++)
                            {
                                result += romanLookup[charIdx]; // e.g. VII
                            }
                        }
                        else if (current == 4) // 4
                        {
                            result = result + romanLookup[charIdx] + romanLookup[1 + charIdx]; // e.g. IV
                        }
                        else if (current == 9) // 9
                        {
                            result = result + romanLookup[charIdx] + romanLookup[2 + charIdx]; // e.g. IX
                        }

                    }

                    charIdx -= 2; // step in the romanLookup table
                    number %= divider; // we use the remainder next time
                }
            }
            else
            {
                result = "Invalid number!";
            }

            return result;
        }
    }
}
