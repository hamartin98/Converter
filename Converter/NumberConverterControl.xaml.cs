using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Converter
{
    /// <summary>
    /// Interaction logic for NumberConverterControl.xaml
    /// </summary>
    public partial class NumberConverterControl : UserControl
    {
        private List<string> fromBaseList; // Stores the list of bases selectable to convert from
        private List<string> toBaseList; // Stores the list of bases selectable to convert to
        private const string hexLookup = "0123456789ABCDEF"; // Lookup table for number conversion
        private const string romanLookup = "IVXLCDM"; // Lookup table for roman characters, ordered by value

        public NumberConverterControl()
        {
            InitializeComponent();
            InitList();
        }

        private void btnCopyResult_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(lblResult.Content.ToString());
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            lblResult.Content = ConvertInput();
        }

        // Converts the given number based on the input
        // Returns the converted value as string
        private string ConvertInput()
        {
            string result = "Error, wrong number format!";

            string number = tbNumber.Text.ToString();
            int fromBase = cbFromBase.SelectedIndex + 2;
            int toBase;
            string toBaseStr = cbToBase.SelectedValue.ToString();

            number = FormatString(number);

            if (number != "" && IsNumberValidInBase(number, fromBase))
            {
                if (toBaseStr == "Roman")
                {
                    //result = ConvertToRomanNumber(number, fromBase);
                }
                else
                {
                    toBase = cbToBase.SelectedIndex + 2;
                    result = FormatString(ConvertNumber(number, fromBase, toBase));
                }
            }

            return result;
        }

        // Checks if the given value is a valid number in the selected base
        // Returns true if its correct
        private bool IsNumberValidInBase(string number, int baseOf)
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

        // Converts the number between the given bases
        // Returns the converted value
        private string ConvertNumber(string number, int fromBase, int toBase)
        {
            string result = "";
            int base10Num = ConvertToBase10(number, fromBase);

            do
            {
                result += hexLookup[base10Num % toBase];
                base10Num /= toBase;
            }
            while (base10Num > 0);

            return result;
        }

        // Converts the given number to Roman number
        private string ConvertToRomanNumber(string number, int fromBase)
        {
            // needs to be implemented
            return "";
        }

        // Removes unnecessary spaces from the string
        // Invert and convert the characters in the string to uppper case
        // Returns the formatted string
        private string FormatString(string str)
        {
            string result = "";
            str = str.Trim();
            
            for(int idx = str.Length - 1; idx >= 0; idx--)
            {
                result += char.ToUpper(str[idx]);
            }

            return result;
        }

        // Initialize the base lists, then sets the itemsources of the base selector ComboBoxes
        private void InitList()
        {
            fromBaseList = new List<string>();
            toBaseList = new List<string>();

            for (int num = 2; num < 17; num++)
            {
                fromBaseList.Add(num.ToString());
                toBaseList.Add(num.ToString());
            }

            cbFromBase.ItemsSource = fromBaseList;
            toBaseList.Add("Roman number");
            cbToBase.ItemsSource = toBaseList;
        }

        // Converts a character to int value eg.: C -> 12
        // Returns the integer value of the character
        private int CharToValue(char ch)
        {
            return (ch >= '0' && ch <= '9') ? Convert.ToInt32(ch - 48) : Convert.ToInt32(ch - 55);
        }

        // Convert and return the given number in base 10
        // The number must be in Big-endian format
        private int ConvertToBase10(string number, int fromBase)
        {
            int result = 0;
            int currValue = 0;

            for(int idx = 0; idx < number.Length; idx++)
            {
                currValue = CharToValue(number[idx]); // get the integer value of the current character
                result += Convert.ToInt32(currValue * Math.Pow(fromBase, idx)); // Add the current subresult to the result
            }

            return result;
        }
    }
}
