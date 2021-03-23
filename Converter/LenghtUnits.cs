namespace Converter
{
    class LenghtUnits : UnitBase
    {
        // Base unit is meter
        public LenghtUnits() : base("Length units")
        {
            conversionRates.Add("millimeters", 1000.0);
            conversionRates.Add("centimeter", 100.0);
            conversionRates.Add("meter", 1.0);
            conversionRates.Add("kilometer", 1 / 1000.0);
        }
    }
}
