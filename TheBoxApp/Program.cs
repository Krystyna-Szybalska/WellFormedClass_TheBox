using System;
using System.Collections.Generic;
using System.Linq;

namespace WellFormedClass_TheBox
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pudelko> boxes = new List<Pudelko>()
            {
                new(),
                new(0.2),
                new(0.2, 0.3),                
                new(2,1,0.012),
                new(0.2, 0.3, 0.4),
                new(158, 233, 4, UnitOfMeasure.Centimeter),
                new(356, 4000, 666, UnitOfMeasure.Millimeter),
                new(1, 3, 2.66, UnitOfMeasure.Meter),
            };
            Console.WriteLine("Unsorted list:");
            foreach (var box in boxes)
            {
                Console.WriteLine(box);
            }

            Console.WriteLine("Sorted list");
            foreach (var box in boxes.OrderBy(box => box.Objetosc).ThenBy(box => box.Pole))
            {
                Console.WriteLine($"Pudelko: {box}, objętość: {box.Objetosc}, pole: {box.Pole}");
            }
        }
    }
}