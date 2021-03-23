namespace Converter
{
    class LenghtUnit : UnitBase
    {
        // Base unit is meter
        public LenghtUnit() : base("Length units")
        {
            conversionRates.Add("millimeters", 1000);
            conversionRates.Add("centimeter", 100);
            conversionRates.Add("meter", 1);
            conversionRates.Add("kilometer", 1.0 / 1000);
        }
    }
}
