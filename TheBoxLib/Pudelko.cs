using System;
using System.Collections;
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
        
        
        //public static pudelko (pudelko p1, pudelko p2) ktore returnuje p3 -
        // najmniejsze mozliwe pudelko, do ktorego zmieszcza sie razem p1 i p2
        
        /*7. konwersja
        jawna - rzutowanie - przeciązenie operatora ()
        ma przeerzucic pudelko na (double[] p)
        
        public static implicit/explicit operator....(...) 
        
        niejawna
        jesli w kontekscie pudelka pojawi sie p = (2, 3, 55) ma automatycznie zostac traktowane jak pudelko
        p - podane dane są w mm jako ValueTuple, np pudelko p3 = (2,3,5)+(40,8,10) ma działać
        
        */
        
        /*8. indexer - przeciazenie operatora [] public static override operator this[i]()
        pozwala odczytac wartosc obiektu tak jakbysmy czytali tablice
        pudelko p ma wymiary a,b,c - chcemy moc sie do nich postac p[0] = a, p[1] = b, p[2] =c         
        */
        
        /* iterator - mechanizm przeglądania obiektu za pomocą foreacha
         foreach (double wymiar in pudelko p)
         elementy zdefiniowane tym iteratorem bedziemy mogli przegladac
         np. do obliczenia objetosci
         
         :ienumerable
         */
        public IEnumerator GetEnumerator()
        {
           //yield return
            throw new NotImplementedException();
        }
        //parsowanie z inputu jako string
        //metoda rozszerzajaca
        //metoda sortujaca - definiujac lambdę
    }
}