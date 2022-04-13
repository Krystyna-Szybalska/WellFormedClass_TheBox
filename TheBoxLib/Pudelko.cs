using System;
using System.Collections;

namespace WellFormedClass_TheBox
{
    //TheBox class
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable
    {
        //co dokladnie robi sealed
        private double a, b, c;
        public double A => a;
        public double B => b;
        public double C => c;
        public double Objetosc => Math.Round(a * b * c, 9);

        public Pudelko(double a, double b, double c, UnitOfMeasure unit)
        {
            //czy wlasciwosci maja bycprzekazywane w konstruktorze czy siedziec w klasie?
        }
        
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            throw new NotImplementedException();
            //w dokumentacji jest przyklad na stopniach dla K, C, F
        }

        //override Wquals(objcet) i GetHashCode
        public bool Equals(Pudelko? other)
        {
            //przeciazyc == i !=
            throw new NotImplementedException();
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