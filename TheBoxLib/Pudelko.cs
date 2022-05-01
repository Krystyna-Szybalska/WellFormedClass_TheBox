using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WellFormedClass_TheBox
{
    //TheBox class
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable
    {
        private double _a, _b, _c;
        public double A => Math.Round(_a, 3);
        public double B => Math.Round(_b, 3);
        public double C => Math.Round(_c, 3);
        public double[] boxParameters => new[] {_a, _b, _c};
        public double Objetosc => Math.Round(_a * _b * _c, 9);
        public double Pole => Math.Round(_a * _b * 2 + _a * _c * 2 + _b * _c * 2, 6);

        public Pudelko(double a = 0.01, double b = 0.01, double c = 0.01, UnitOfMeasure unit = UnitOfMeasure.Meter)
        {
            if (a is <= 0 or > 10 ) throw new ArgumentOutOfRangeException(nameof(a));
            if (b is <= 0 or > 10 ) throw new ArgumentOutOfRangeException(nameof(b));
            if (c is <= 0 or > 10 ) throw new ArgumentOutOfRangeException(nameof(c));

            _a = a;
            _b = b;
            _c = c;
        }
        
        public string ToString(string? format, IFormatProvider? formatProvider = null)
        {
            if (formatProvider is null) {
                formatProvider = CultureInfo.CurrentCulture; 
            }
            return format switch {
                "m" => $"{A.ToString("#0.000",formatProvider)} m × " +
                       $"{B.ToString("#0.000",formatProvider)} m × " +
                       $"{C.ToString("#0.000",formatProvider)} m",
                "cm" => $"{UnitMeasureConverter.ConvertToCentimeters(A).ToString("###0.0", formatProvider)} cm × " +
                        $"{UnitMeasureConverter.ConvertToCentimeters(B).ToString("###0.0", formatProvider)} cm × " +
                        $"{UnitMeasureConverter.ConvertToCentimeters(C).ToString("###0.0", formatProvider)} cm",
                "mm" => $"{UnitMeasureConverter.ConvertToMillimeters(A).ToString("####0", formatProvider)} mm × " +
                        $"{UnitMeasureConverter.ConvertToMillimeters(B).ToString("####0", formatProvider)} mm × " +
                        $"{UnitMeasureConverter.ConvertToMillimeters(C).ToString("####0", formatProvider)} mm",
                _ => throw new FormatException()
            };
        }
        
        public override bool Equals(object obj) {
            if ( !(obj is Pudelko) || obj == null) return false;
            var box = (Pudelko)obj;
            var newParameters = box.boxParameters; //To do: czy to dziala?
            return boxParameters.All(newParameters.Contains) && boxParameters.Length == newParameters.Length;
        }
        
        public bool Equals(Pudelko other)
        {
            return Equals((object)other);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(A, B, C);
        }



        public static bool operator ==(Pudelko box, Pudelko otherBox)
        {
            return (box, otherBox) switch
            {
                (null, null) => true,
                (null, not null) => false,
                (not null, null) => false,
                _ => box.Equals(otherBox)
            };
        }

        public static bool operator !=(Pudelko? box, Pudelko? otherBox) => !(box == otherBox);
        
        public static Pudelko operator +(Pudelko firstBox, Pudelko secondBox)
        {
            var firstBoxParameters = firstBox.boxParameters.OrderBy(a => a).ToArray();
            var secondBoxParameters = secondBox.boxParameters.OrderBy(a => a).ToArray();
            var a = firstBoxParameters[0]+secondBoxParameters[0];
            var b = Math.Max(firstBoxParameters[1], secondBoxParameters[1]);
            var c = Math.Max(firstBoxParameters[2], secondBoxParameters[2]);
            
            return new Pudelko(a, b, c);
        }

        public static explicit operator double[](Pudelko box) => new[] {box.A, box.B, box.C};

        public static implicit operator Pudelko(ValueTuple<int, int, int> values) =>
            new(values.Item1, values.Item2, values.Item3, UnitOfMeasure.Millimeter);
        public double this[int index] => boxParameters[index];
        
        /*
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        */
        //ktora wersja jest poprawniejsza
        public IEnumerator GetEnumerator()
        {
            foreach (var parameter in boxParameters)
            {
                yield return parameter;
            }
        }
        //parsowanie z inputu jako string
        public static Pudelko Parse(string text)
        {
            throw new NotImplementedException();
        }

        //metoda rozszerzajaca
        //metoda sortujaca - definiujac lambdę
    }
}