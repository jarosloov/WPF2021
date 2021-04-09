using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;

namespace BallisticFlight
{
    class Program
    {
        static void Main(string[] args)
        {

            

        }
    }

    class BallisticFlight
    {
        List<double> coordinates_X = 
            new List<double>();                             // cлаварь сохранения координат по Х
        List<double> coordinates_Y =
            new List<double>();                             // cлаварь сохранения координат по y
        List<double> velocityProjections_X =
           new List<double>();                              // словарь сохранения проекций скоростей по y
        List<double> velocityProjections_Y =
           new List<double>();                              // словарь сохранения проекций скоростей по Х

        private const double AccelerationOfFreeFall = 9.81; // ускорение свободного падения

        private double startSpeed;                          // начальная скорость
        private double angleOfInclination;                  // угол наклона
        private double startHeight;                         // начальная высота

        private double flightTime;                          // время плёта
        private double deltaFlightTime;                     // разбиение времени 

        private double coefficientResistance;               // коэффицент сопростивления 
        private double materialDensity;                     // плотность тела
        private double airDensity;                          // плотность воздуха 
        private double bodyRadius;                          // радиус 

        private double resistanceForce;                     // сила попротивления

        private double maxFlightLength;                     // максимальная длина полёта
        private double maxflightAltitude;                   // максимальная высота полёта 

        private bool statusDataEntry = false;               // статус наличия данных
        private bool statusEntriesInTextDocument = false;   // статус записи в текстовый докусент
        private bool statusEntriesInExcel = false;          // статус записи в эксель

        public BallisticFlight()
        {
            Menu();
        }

        private void Input()
        {
            Console.Clear();
            Console.WriteLine("Введите скорость тела: ");
            startSpeed = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Введите угол наклона: ");
            angleOfInclination = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Введите начальную высоту: ");
            startHeight = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Введите радиус тела ");
            bodyRadius = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Введите коэффицент сопротивления: ");
            coefficientResistance = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Введите плотность материала: ");
            materialDensity = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Введите плотность воздуха: ");
            airDensity = Convert.ToDouble(Console.ReadLine());

            resistanceForce = coefficientResistance 
                * (Math.PI * bodyRadius * bodyRadius) * airDensity / (2 * materialDensity 
                * (4 / 3) * Math.PI * Math.Pow(bodyRadius, 3));

            flightTime = 2 * startSpeed * Math.Sin(angleOfInclination * Math.PI / 180) / AccelerationOfFreeFall;

            statusDataEntry = true;
            Menu();
        }
        
        private void Calculate()
        {
            coordinates_X.Add(0);
            coordinates_Y.Add(startHeight);
        }

        private void Menu()
        {

        }

        private void Menu(int test)
        {

        }

        private void GoBack(string SpecifyingLocation)
        {
            Console.WriteLine(" ");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("[Esc] - вернуться назад");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
            {
                if (SpecifyingLocation == "Menu")
                {
                    //Menu();
                }
                    Menu();
            }
            else if (key.Key != ConsoleKey.Escape)
            {
                Console.WriteLine("Нет такой команды! Повторите.");
                key = Console.ReadKey();
            }
        }
    }
}
