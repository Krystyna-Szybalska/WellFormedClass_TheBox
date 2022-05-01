using System;

namespace WellFormedClass_TheBox
{
    public static class UnitMeasureConverter
    {
        public static double ConvertToMillimeters(double value, UnitOfMeasure unit = UnitOfMeasure.Meter)
        {
            return unit switch {
            UnitOfMeasure.Millimeter => value,
            UnitOfMeasure.Centimeter => value/10,
            UnitOfMeasure.Meter => value/1000,
            _ => throw new InvalidOperationException()
            };
        }
        
        public static double ConvertToCentimeters(double value, UnitOfMeasure unit = UnitOfMeasure.Meter)
        {
            return unit switch {
            UnitOfMeasure.Millimeter => value/10,
            UnitOfMeasure.Centimeter => value,
            UnitOfMeasure.Meter => value*100,
            _ => throw new InvalidOperationException()
            };
        }
                
        public static double ConvertToMeters(double value, UnitOfMeasure unit = UnitOfMeasure.Meter)
        {
            return unit switch {
            UnitOfMeasure.Millimeter => value*1000,
            UnitOfMeasure.Centimeter => value*100,
            UnitOfMeasure.Meter => value,
            _ => throw new InvalidOperationException()
            };
        }
    }
}