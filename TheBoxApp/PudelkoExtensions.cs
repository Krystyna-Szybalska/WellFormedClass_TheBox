using System;

namespace WellFormedClass_TheBox
{
    public static class Extensions
    {
        public static Pudelko Kompresuj(this Pudelko originalBox)
        {
            double a = Math.Cbrt(originalBox.Objetosc);
            Pudelko compressedBox = new Pudelko(a, a, a);
            return compressedBox;
        }
    }
}