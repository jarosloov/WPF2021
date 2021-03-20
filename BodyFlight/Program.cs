using System;
using System.IO;
using System.Collections.Generic;

namespace BodyFlight
{
    class Program
    {
        static void Main(string[] args)
        {
            BodyFlight body = new BodyFlight();
        }
    }

    class BodyFlight
    {
        private double speed;
        private double time;
        private double angle;

        private double fullFlightTime;
        private double maximumFlightAltitude;
        private double flightLength;

        Dictionary<double, double> coordinates = new Dictionary<double, double>();

        private const double G = 9.80665;



        /// <summary>
        /// Скорость, время полета, угол наклона 
        /// </summary>
        /// <param Скорость="speed"></param>
        /// <param время полета="time"></param>
        /// <param угол наклона="angle"></param>
        public BodyFlight(double speed, double time, double angle)
        {
            this.speed = speed;
            this.time = time;
            this.angle = angle;
            Menu();
        }

        /// <summary>
        /// Скорость, угол наклона 
        /// </summary>
        /// <param Скорость="speed"></param>
        /// <param угол наклона="anfle"></param>
        public BodyFlight(double speed, double angle)
        {
            this.speed = speed;
            this.angle = angle;
            time = 2 * speed * Math.Sin(angle * Math.PI / 180) / G;
            Menu();
        }
        /// <summary>
        /// Конуструктор по умолчанию 
        /// </summary>
        public BodyFlight()
        {
            Input();
            Menu();
        }

        private void Input()
        {
            Console.WriteLine("Введите скорость тела: ");
            speed = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите угол наклона: ");
            angle = Convert.ToDouble(Console.ReadLine());
        }

        private void Menu()
        {
            Console.Clear();
            Console.WriteLine("Вызволось меню:");
            Console.WriteLine("Нажмите [I] чтобы вызвать информацию");
            ConsoleKeyInfo key = Console.ReadKey();
            //switch (key.Key)
            //{
            //    case ConsoleKey.I:
            //        OutputInformation();
            //        break;

            //    default:
            //        Console.WriteLine("Default case");
            //        break;
            //}
            if (key.Key == ConsoleKey.I)
                OutputInformation();
            Console.Clear();
        }

        private bool OutputToFileExcel()
        {
            return true;
        }

        private bool OutputToFileTXT()
        {
            return true;
        }

        private void OutputInformation()
        {
            Console.WriteLine("Вызволось инфо");
            Menu();
        }
    }
}
