using System;
using System.Globalization;

namespace WellFormedClass_TheBox
{
    public static class UnitMeasureConverter
    {
        public static double ConvertToMillimeters(double value, UnitOfMeasure unit = UnitOfMeasure.Meter)
        {
            return unit switch {
            UnitOfMeasure.Millimeter => Math.Round(value, 0),
            UnitOfMeasure.Centimeter => Math.Round(value*10, 1),
            UnitOfMeasure.Meter => Math.Round(value*1000, 3),
            _ => throw new InvalidOperationException()
            };
        }
        
        public static double ConvertToCentimeters(double value, UnitOfMeasure unit = UnitOfMeasure.Meter)
        {
            return unit switch {
            UnitOfMeasure.Millimeter => Math.Round(value/10, 0),
            UnitOfMeasure.Centimeter => Math.Round(value, 1),
            UnitOfMeasure.Meter => Math.Round(value*100, 3),
            _ => throw new InvalidOperationException()
            };
        }
                
        public static double ConvertToMeters(double value, UnitOfMeasure unit = UnitOfMeasure.Meter)
        {
            return unit switch {
            UnitOfMeasure.Millimeter => Math.Round(value/1000, 0),
            UnitOfMeasure.Centimeter => Math.Round(value/100, 1),
            UnitOfMeasure.Meter => Math.Round(value, 3),
            _ => throw new InvalidOperationException()
            };
        }
    }
}