using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        public NumberConverterControl()
        {
            InitializeComponent();
            InitItemSources();
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

            number = NumberConverter.FormatString(number);

            if (number != "" && NumberConverter.IsNumberValidInBase(number, fromBase))
            {
                if (toBaseStr == "Roman number")
                {
                    result = NumberConverter.ConvertToRomanNumber(number, fromBase);
                }
                else
                {
                    toBase = cbToBase.SelectedIndex + 2;
                    result = NumberConverter.ConvertNumber(number, fromBase, toBase);
                }
            }

            return result;
        }

        // Initialize the base lists, then sets the itemsources of the base selector ComboBoxes
        private void InitItemSources()
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

        private void tbNumber_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
