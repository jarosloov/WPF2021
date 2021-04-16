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
            BallisticFlight ballisticFlight = new BallisticFlight(true);
        }
    }

    class BallisticFlight
    {
        Dictionary<double, double> coordinates = 
            new Dictionary<double, double>();               // координаты

        List<double> coordinates_X = 
            new List<double>();                             // cлаварь сохранения координат по Х
        List<double> coordinates_Y =
            new List<double>();                             // cлаварь сохранения координат по y

        private const double AccelerationOfFreeFall = 9.81; // ускорение свободного падения
        private const double deltaTime = 0.001;            // разбиение времени 

        private double startSpeed;                          // начальная скорость
        private double angleOfInclination;                  // угол наклона
        private double startHeight;                         // начальная высота

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

        // В разработке
        public BallisticFlight()
        {
            Input();
        }

        // В разработке
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

            statusDataEntry = true;
            Calculate();
        }

        private void Calculate()
        {
            double speed_x = startSpeed * Math.Cos(angleOfInclination * Math.PI / 180);
            double speed_y = startSpeed * Math.Sin(angleOfInclination * Math.PI / 180);

            double coordinates_X = 0;
            double coordinates_Y = 0;

            double modelSpeed = (Math.Sqrt(speed_x * speed_x + speed_y * speed_y));

            double square = (bodyRadius * bodyRadius * Math.PI);
            // зависит от времени 
            resistanceForce = coefficientResistance * airDensity
                * (Math.PI * bodyRadius * bodyRadius) / (2 * materialDensity
                * (4 / 3) * Math.PI * Math.Pow(bodyRadius, 3)) * modelSpeed;

            while (coordinates_Y >= 0)
            {
                coordinates_X = coordinates_X + deltaTime * speed_x;
                speed_x = speed_x - deltaTime * resistanceForce * speed_x;

                coordinates_Y = coordinates_Y + deltaTime * speed_y;
                speed_y = speed_y - deltaTime * (9.81 + resistanceForce * speed_y);
                if (coordinates_Y <= 0)
                    break;

                //Console.WriteLine('[' + coordinates_X + "  0]");
                coordinates.Add(coordinates_X, coordinates_Y);
            }
           

            //foreach (var i in coordinates)
            //    Console.WriteLine(i);
            //Console.ReadKey();
        }
        // В разработке  
        private void Menu()
        {

        }

        public BallisticFlight(bool test)
        {         
            startSpeed = 30;
            angleOfInclination = 30;
            startHeight = 0;
            bodyRadius = 1;
            coefficientResistance = 0.47;
            materialDensity = 7;
            airDensity =1.29;
            Calculate();
            OutputToFileExcel();
        }

        private void OutputToFileExcel()
        {
            try
            {
                // Загрузить Excel, затем создать новую пустую рабочую книгу
                Excel.Application excelApp = new Excel.Application();

                // Сделать приложение Excel видимым
                excelApp.Visible = true;
                string txtFile = @"C:\GitHub\WPF2021\BallisticFlight\test.xlsx";

                Excel.Workbook workbook = excelApp.Workbooks.Open(
                        txtFile,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing);

                Excel._Worksheet workSheet = excelApp.ActiveSheet;

                int i = 1;
                foreach (var numer in coordinates)
                {
                    workSheet.Cells[i, 1] = "[x]:\t";
                    workSheet.Cells[i, 3] = numer.Key;
                    workSheet.Cells[i, 2] = "[y]:\t";
                    workSheet.Cells[i, 4] = numer.Value;
                    i++;
                }
                
                //for (double i = 0; i <= coordinates.Count; ++i)
                //{
                //    workSheet.Cells[i, 1] = "[x]:\t";
                //    workSheet.Cells[i, 2] = coordinates.Ke
                //    workSheet.Cells[i, 3] = "[y]:\t";
                //    workSheet.Cells[i, 4] = coordinates.Values;
                //}
                workbook.Close(true, Type.Missing, Type.Missing);
                excelApp.Quit();
                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Ошибка");
            }
        }
        // В разработке
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
