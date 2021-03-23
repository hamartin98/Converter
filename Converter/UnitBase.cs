using System.Collections.Generic;
using System.Linq;

namespace Converter
{
    abstract class UnitBase
    {
        public string Name { get; }
        protected Dictionary<string, double> conversionRates; // Store the conversion rates to the base unit

        public UnitBase(string name)
        {
            this.Name = name;
            conversionRates = new Dictionary<string, double>();
        }

        // Convert the value from the given unit to the base unit
        // Returns the result in the base unit as a double value
        private double ConvertToBaseUnit(double value, string fromUnit)
        {
            return value / conversionRates[fromUnit];
        }

        // Convert the value from the base unit to the given unit
        // Returns the result in the given unit as a double value
        private double ConvertFromBaseUnit(double value, string toUnit)
        {
            return value * conversionRates[toUnit];
        }

        // Convert the value from the given unit to another unit
        public double ConvertUnit(double value, string fromUnit, string toUnit)
        {
            return ConvertFromBaseUnit(ConvertToBaseUnit(value, fromUnit), toUnit);
        }

        // Returns the keys from conversionRates dictionary as a list
        // Used to get the name of all unit
        public List<string> UnitNames()
        {
            return conversionRates.Keys.ToList();
        }
    }
}
