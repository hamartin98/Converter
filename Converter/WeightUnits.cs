using System;
using System.Collections.Generic;
using System.Text;

namespace Converter
{
    class WeightUnits : UnitBase
    {
        // Base unit is Kilogram

        private const double K = 1000.0;

        public WeightUnits() : base("Weight units")
        {
            this.conversionRates.Add("Milligram", K * K);
            this.conversionRates.Add("Gram", K);
            this.conversionRates.Add("Kilogram", 1.0);
            this.conversionRates.Add("Ton", 1 / K);
        }
    }
}
