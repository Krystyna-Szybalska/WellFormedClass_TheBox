using System;
using System.Collections;
using System.Globalization;
using System.Linq;

namespace WellFormedClass_TheBox
{
    //TheBox class
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable
    {
        
        private double a, b, c;
        private double[] box_parameters;
        public double A => Math.Round(a, 3);
        public double B => Math.Round(b, 3);
        public double C => Math.Round(c, 3);
        public double Objetosc => Math.Round(a * b * c, 9);
        public double Pole => Math.Round(a * b * 2 + a * c * 2 + b * c * 2, 6);

       //if unit is not given it should be meter
        public Pudelko(double a = 0.01, double b=0.01, double c=0.01, UnitOfMeasure unit = UnitOfMeasure.Meter)
        {
            //parameters should be between 0-10 meters
            if (A <= 0 || A > 10 || B <= 0 || B > 10 || C <= 0 || C > 10 ) throw new ArgumentOutOfRangeException();
           
        }
        
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if (formatProvider is null) {
                formatProvider = CultureInfo.CurrentCulture;  //dlaczego nie mogę tego zrobić w pierwszej linijce
            }
            return format switch {
                "m" => $"{A.ToString("#0.000",formatProvider)} m × " +
                       $"{B.ToString("#0.000",formatProvider)} m × " +
                       $"{C.ToString("#0.000",formatProvider)} m",
                /*"cm" => $"{MeasureConverter.ConvertToCentimeters(A).ToString("F1", formatProvider)} cm × " +
                        $"{MeasureConverter.ConvertToCentimeters(B).ToString("F1", formatProvider)} cm × " +
                        $"{MeasureConverter.ConvertToCentimeters(C).ToString("F1", formatProvider)} cm",
                "mm" => $"{MeasureConverter.ConvertToMilimeters(A).ToString("####0", formatProvider)} mm × " +
                        $"{MeasureConverter.ConvertToMilimeters(B).ToString("####0", formatProvider)} mm × " +
                        $"{MeasureConverter.ConvertToMilimeters(C).ToString("####0", formatProvider)} mm",*/
                _ => throw new FormatException()
            };
        }

        public string ToString(string format)
        {
            return ToString(format, null);

        }

        //override Wquals(objcet) i GetHashCode

        public override bool Equals(object? obj) {
            if (obj == null || !(obj is Pudelko)) return false;
            var box = (Pudelko)obj;
            var new_parameters = new[] { box.A, box.B, box.C };
            return box_parameters.All(new_parameters.Contains) && box_parameters.Length == new_parameters.Length;
        }

        public bool Equals(Pudelko? box) {
            //przeciazyc == i !=

            return Equals((object?)box);
        }
        
        //6. operator+
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