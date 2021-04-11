using System;
using System.Collections.Generic;

namespace FlyTrajectory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HO");
            int a = 3;
            int b = 5;
            MyFlyTrajectory ii = new MyFlyTrajectory(a,b);
            ii.Lol();
        }
    }

    class MyFlyTrajectory
    {
        public Dictionary<double, double> coordinates = new Dictionary<double, double>();
        public const double G = 9.80665;
        //public double speed = Convert.ToDouble(Console.ReadLine());
        //public double angle = Convert.ToDouble(Console.ReadLine());
        //public double time = (2 * speed * Math.Sin(angle * Math.PI / 180) / G);

        public MyFlyTrajectory(int i, int j)
        {
            Console.WriteLine('2');
        }

        public MyFlyTrajectory(int i, int j, int k)
        {
            Console.WriteLine('3');
        }
        /// <summary>
        /// Функиця выаода показаний 
        /// </summary>
        public void Lol()
        {
            Console.WriteLine("lol");
        }
    }
}
