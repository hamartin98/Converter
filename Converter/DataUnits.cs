using System;
using System.Collections.Generic;
using System.Text;

namespace Converter
{
    class DataUnits : UnitBase
    {
        // Base unit is Megabyte
        private const double K = 1024.0;

        public DataUnits() : base("Data units")
        {
            this.conversionRates.Add("bit [b]", K * K * 8);
            this.conversionRates.Add("byte [B]", K * K);
            this.conversionRates.Add("kilobyte [kB]", K);
            this.conversionRates.Add("megabyte [MB]", 1);
            this.conversionRates.Add("gigabyte [GB]", 1 / K);
            this.conversionRates.Add("terabyte [TB]", 1 / K / K);
            this.conversionRates.Add("petabyte [PB]", 1 / K / K / K);
            this.conversionRates.Add("exabyte [EB]", 1 / K / K / K);
        }
    }
}
