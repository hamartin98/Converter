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
        private UnitBase lengthUnit = new LenghtUnits();
        private List<UnitBase> units;
        private UnitBase selected;

        public UnitConverterControl()
        {
            InitializeComponent();
            AddUnits();
            InitItemSources();
            InitRadioButtons();
        }

        // Add different unit conversion items
        private void AddUnits()
        {
            units = new List<UnitBase>();

            units.Add(new WeightUnits());
            units.Add(new LenghtUnits());
            units.Add(new DataUnits());

            selected = units[0];
        }

        // Initialize radio buttons based on available unit conversions
        private void InitRadioButtons()
        {
            RadioButton radioButton;
            int idx = 0;

            foreach(var unit in units)
            {
                radioButton = new RadioButton();
                radioButton.Content = unit.Name;
                radioButton.Checked += RadioButton_Checked;
                radioButton.Margin = new Thickness(0, 10, 0, 0);
                radioButton.GroupName = "units";
                radioButton.Tag = idx++;
                radioPanel.Children.Add(radioButton);
            }

            RadioButton rb = radioPanel.Children[0] as RadioButton;
            rb.IsChecked = true;
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

                result = selected.ConvertUnit(value, fromUnit, toUnit).ToString();
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
            List<string> units = selected.UnitNames();
            cbUnitFrom.ItemsSource = units;
            cbUnitTo.ItemsSource = units;
            cbUnitFrom.SelectedIndex = 0;
            cbUnitTo.SelectedIndex = 0;
        }

        // Selection changes between the radio buttons
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            int idx = Convert.ToInt32(rb.Tag);
            selected = units[idx];
            InitItemSources();
        }
    }
}
