using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;

namespace WellFormedClass_TheBox
{
    //TheBox class
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable
    {
        private double _a, _b, _c;
        public double A
        {
            get => Math.Round(_a, 3);
            init => _a = value;
        }
        public double B
        {
            get => Math.Round(_b, 3);
            init => _b = value;
        }        
        public double C
        {
            get => Math.Round(_c, 3);
            init => _c = value;
        }
        
        public UnitOfMeasure Unit { get; init; } //i needed that for constructor to work properly
        public double[] boxParameters => new[] {_a, _b, _c};
        public double Objetosc => Math.Round(_a * _b * _c, 9);
        public double Pole => Math.Round(_a * _b * 2 + _a * _c * 2 + _b * _c * 2, 6);

        public Pudelko(double a = 0.1, double b = 0.1, double c = 0.1, UnitOfMeasure unit = UnitOfMeasure.Meter)
        {
            Unit = unit;
            double _a = UnitMeasureConverter.ConvertToMeters(a, Unit);
            if (_a is <= 0 or > 10 ) throw new ArgumentOutOfRangeException(nameof(_a),$"_a is {_a} and a is {a}");

            _b = UnitMeasureConverter.ConvertToMeters(b, Unit);
            if (_b is <= 0 or > 10 ) throw new ArgumentOutOfRangeException(nameof(_b));
            
            _c = UnitMeasureConverter.ConvertToMeters(c, Unit);
            if (_c is <= 0 or > 10 ) throw new ArgumentOutOfRangeException(nameof(_c));
        }
        
        public override string ToString() {
            return $"{A.ToString("#0.000", null)} m × " +
                   $"{B.ToString("#0.000", null)} m × " +
                   $"{C.ToString("#0.000", null)} m";
        }
        public string ToString(string? format, IFormatProvider? formatProvider = null)
        {
            if (formatProvider is null) formatProvider = CultureInfo.CurrentCulture;
            if (format == null) format = "m";
            
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
            //new P(2.5, 9.321, 0.1) == P.Parse("2.500 m × 9.321 m × 0.100 m")
            
            string[] parametersString = text.Replace(" ", String.Empty).Split('×');
            List<double> parameters = new List<double>();

            foreach (var parameter in parametersString)
            {
                if (parametersString.Length != 3) throw new FormatException(nameof(text));
                if (parameter.Contains("mm")) {
                    if (!Double.TryParse(parameter.Replace("mm", String.Empty), out double a_mm)) throw new FormatException(nameof(text));
                    parameters.Add(a_mm / 1000); 
                }
                else if (parameter.Contains("cm")) {
                    if (!Double.TryParse(parameter.Replace("cm", String.Empty), out double b_cm)) throw new FormatException(nameof(text));
                    parameters.Add(b_cm / 100); 
                }
                else if (parameter.Contains("m")) {
                    if (!Double.TryParse(parameter.Replace("m", String.Empty), NumberStyles.Number, CultureInfo.InvariantCulture, out double c)) throw new FormatException(nameof(text));
                    parameters.Add(c);
                }
                else throw new FormatException(nameof(text));
            }
            
            return new Pudelko(parameters[0], parameters[1], parameters[2]);
        }

        //metoda rozszerzajaca
        //metoda sortujaca - definiujac lambdę
    }
}