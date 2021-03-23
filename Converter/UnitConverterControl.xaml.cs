using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace Converter
{
    /// <summary>
    /// Interaction logic for UnitConverterControl.xaml
    /// </summary>
    public partial class UnitConverterControl : UserControl
    {
        private UnitBase lengthUnit = new LenghtUnit();
        private List<UnitBase> units = new List<UnitBase>();
        private UnitBase selected;

        public UnitConverterControl()
        {
            InitializeComponent();
            InitItemSources();
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            ConvertUnit();
        }

        // Process the input, then convert the value betweeen the given units
        private void ConvertUnit()
        {
            string result = "Error";

            try
            {
                double value = Convert.ToDouble(tbNumber.Text);
                string fromUnit = cbUnitFrom.SelectedValue.ToString();
                string toUnit = cbUnitTo.SelectedValue.ToString();

                //result = ConvertUnit(value, fromUnit, toUnit).ToString();
                result = lengthUnit.ConvertUnit(value, fromUnit, toUnit).ToString();
            }
            catch (FormatException ex)
            {
                result = ex.Message;
            }

            lblResult.Content = result;
        }

        // Copy result to the clipboard
        private void btnCopyResult_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(lblResult.Content.ToString());
        }

        // Only numeric input is allowed
        private void tbNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        // Initialize item sources to the combo boxes
        private void InitItemSources()
        {
            List<string> units = lengthUnit.UnitNames();
            cbUnitFrom.ItemsSource = units;
            cbUnitTo.ItemsSource = units;
        }
    }
}
