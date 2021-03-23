using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Converter
{
    /// <summary>
    /// Interaction logic for UnitConverterControl.xaml
    /// </summary>
    public partial class UnitConverterControl : UserControl
    {
        // Use meter as the base unit
        // Store rates for every unit's conversion rate to meter

        private const double miliMeter = 1000;
        private const double centiMeter = 100;
        private const double meter = 1;
        private const double kiloMeter = 1 / 1000;

        private Dictionary<string, double> metricRates;

        public UnitConverterControl()
        {
            InitializeComponent();
            InitMetricDictionary();
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
                result = ConvertUnit(value, fromUnit, toUnit).ToString();
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
            List<string> metricUnits = UnitNamesList();
            cbUnitFrom.ItemsSource = metricUnits;
            cbUnitTo.ItemsSource = metricUnits;
        }

        // Initialize the metric dictionary with conversion rates
        private void InitMetricDictionary()
        {
            metricRates = new Dictionary<string, double>();

            metricRates.Add("millimeter", 1000);
            metricRates.Add("centimeter", 100);
            metricRates.Add("meter", 1);
            metricRates.Add("kilometer", 1.0 / 1000);
        }

        // Convert the value from the given unit to meter
        // Returns the result in meter as a double value
        private double ConvertToMeter(double value, string fromUnit)
        {
            return value / metricRates[fromUnit];
        }

        // Convert the value from meter to the given unit
        // Returns the result in the given unit as a double value
        private double ConvertFromMeter(double value, string toUnit)
        {
            return value * metricRates[toUnit];
        }

        // Convert the value from the given unit to another unit
        public double ConvertUnit(double value, string fromUnit, string toUnit)
        {
            return ConvertFromMeter(ConvertToMeter(value, fromUnit), toUnit);
        }

        // Returns the keys from metricUnits as a list
        private List<string> UnitNamesList()
        {
            return metricRates.Keys.ToList();
        }
    }
}
