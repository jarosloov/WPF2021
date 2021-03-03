using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<double, double> coordinates = new Dictionary<double, double>();
            const double G = 9.80665;
            double speed = Convert.ToDouble(Console.ReadLine());
            double angle = Convert.ToDouble(Console.ReadLine());
            double time = (2 * speed * Math.Sin(angle * Math.PI / 180) / G);
            // Время и растояние полёта 
            Console.WriteLine("Time of fight: " + Math.Round((2 * speed *
                Math.Sin(angle * Math.PI / 180) / G)), 3);
            Console.WriteLine("Length of fight: " + Math.Round((speed * speed *
                Math.Sin(2*angle * Math.PI / 180)) / G), 3);
            // запись   
            for (double t = 0; t <= time ; t += 0.01)
                coordinates.Add(Math.Round((speed * t * Math.Cos(angle * Math.PI / 180)), 3),
                    Math.Round((speed * t * Math.Sin(angle * Math.PI / 180) - G * t * t / 2), 3)) ;   
            // вывод
            foreach (var i in coordinates)
                Console.WriteLine(i);
        }
    }
}
