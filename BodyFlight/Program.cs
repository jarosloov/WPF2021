using System;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Diagnostics;

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

        bool statusTXT = false;
        bool statusExcel = false;
        bool statusInput = true;

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
            Menu();
        }

        private void Input()
        {
            Console.Clear();
            Console.WriteLine("Введите скорость тела: ");
            speed = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите угол наклона: ");
            angle = Convert.ToDouble(Console.ReadLine());
            time = 2 * speed * Math.Sin(angle * Math.PI / 180) / G;
            statusInput = false;
            Menu();
        }

        private void Menu()
        {
            Console.WriteLine("############  Meню  ############");
            if(statusInput)
            {
                Console.WriteLine("[D] - Ввод данных \t [нет данных]");
                Console.WriteLine("[I] - информаця \t [нет данных]");
                if (statusExcel)
                    Console.WriteLine("[E] - запись в .excel \t [записано][нет данных]");
                else
                    Console.WriteLine("[E] - запись в .excel \t [не записано][нет данных]");
                if (statusTXT)
                    Console.WriteLine("[T] - запись в .txt  \t [записано][нет данных]");
                else
                    Console.WriteLine("[T] - запись в .txt  \t [не записано][нет данных]");
                Console.WriteLine("[Esc] - выход");
            }
            else
            {
                Console.WriteLine("[D] - Ввод новых данных \t [есть данные]");
                Console.WriteLine("[I] - информаця \t [есть данные]");
                
                if (statusExcel)
                    Console.WriteLine("[E] - запись в .excel \t [записано]");
                else
                    Console.WriteLine("[E] - запись в .excel \t [не записано]");
                if (statusTXT)
                    Console.WriteLine("[T] - запись в .txt  \t [записано]");
                else
                    Console.WriteLine("[T] - запись в .txt  \t [не записано]");
                Console.WriteLine("[Esc] - выход");
            }
                
            ConsoleKeyInfo key = Console.ReadKey();
            while (true)
            {
                if (key.Key == ConsoleKey.I)
                    OutputInformation();
                else if (key.Key == ConsoleKey.E)
                {
                    OutputToFileExcel();
                    Console.Clear();
                }
                else if (key.Key == ConsoleKey.T)
                {
                    OutputToFileTXT();
                    Console.Clear();
                }
                else if(key.Key == ConsoleKey.D)
                {
                    Input();
                    Console.Clear();
                }
                else if (key.Key == ConsoleKey.Escape)
                    Process.GetCurrentProcess().Kill();
                else if((key.Key != ConsoleKey.I) || (key.Key != ConsoleKey.D) || (key.Key != ConsoleKey.E) || 
                    (key.Key != ConsoleKey.T) || (key.Key != ConsoleKey.Escape))
                     {
                        Console.WriteLine("Нет такой команды! Повторите.");
                        key = Console.ReadKey();
                     }
            }
        }

        private void OutputToFileExcel()
        {
            Console.WriteLine("####  запись в .excel  ####");
            Console.WriteLine(" ");
            Console.WriteLine("##  Находится в разработке ##");



            Console.WriteLine(" ");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("[Esc] - вернуться назад");
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
                Menu();
            else if (key.Key != ConsoleKey.Escape)
            {
                Console.WriteLine("Нет такой команды! Повторите.");
                key = Console.ReadKey();
            }
        }

        private async void OutputToFileTXT()
        {
            
            Console.WriteLine("####  запись в .txt  ####");
            Console.WriteLine(" ");
            string writePath = @"C:\GitHub\WPF2021\BodyFlight\test.txt";
            //string text = "Привет мир!\nПока мир...";
            Console.WriteLine("Введите интрервал с которым хотите получать координаты полёта тела:");
            Console.WriteLine("!Запятая!");
            double diameter = Convert.ToDouble(Console.ReadLine());
            double flightLength = Math.Round(((speed * speed * Math.Sin(2 * angle * Math.PI / 180)) / G), 3);
            double newangle = angle * Math.PI / 180;
            double t;
            if (diameter <= 0)
            {
                Console.WriteLine("Вы ввели '0' или  число < 0 . Зачем? Введите число > 0");
                diameter = Convert.ToDouble(Console.ReadLine());
            }
            else if (diameter > flightLength)
            {
                Console.WriteLine("Ошибка число больше чем, максимальной длины полета. Введите число заново.");
                diameter = Convert.ToDouble(Console.ReadLine());
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    for (double l = 0; l <= flightLength; l += diameter)
                    {
                        t = l / (speed * Math.Cos(newangle));
                        await sw.WriteLineAsync("[x]:\t"
                            + Math.Round(l, 3) + '\t' 
                            + "[y]:\t" 
                            + Math.Round((speed * t * Math.Sin(newangle) - (G * t * t) / 2), 3));
                    }
                }
                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Ошибка");
                statusTXT = false;
            }

            statusTXT = true;

            Console.WriteLine(" ");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("[Esc] - вернуться назад");
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
                Menu();
            else if (key.Key != ConsoleKey.Escape)
            {
                Console.WriteLine("Нет такой команды! Повторите.");
                key = Console.ReadKey();
            }
        }

        private void OutputInformation()
        {
            Console.Clear();
            Console.WriteLine("############  Показ информации  ############");
            Console.WriteLine("[I] - Основная информаця");
            Console.WriteLine("[С] - Показ всех координат");
            Console.WriteLine("[T] - Координыты в текущий момент времени");
            Console.WriteLine("[Esc] - вернуться назад");
            ConsoleKeyInfo key = Console.ReadKey();
            while (true)
            {
                if (key.Key == ConsoleKey.I)
                    OutputIndication();
                else if (key.Key == ConsoleKey.C)
                    OutputFlightCoordinates();
                else if (key.Key == ConsoleKey.T)
                    OutputToFileTXT();
                else if (key.Key == ConsoleKey.Escape)
                    Menu();
                else if ((key.Key != ConsoleKey.I) || (key.Key != ConsoleKey.C) ||
                    (key.Key != ConsoleKey.T) || (key.Key != ConsoleKey.Escape))
                {
                    Console.WriteLine("Нет такой команды! Повторите.");
                    key = Console.ReadKey();
                }
            }
        }

        private void OutputIndication()
        {
            Console.Clear();
            Console.WriteLine("############  Показ информации  ############");
            Console.WriteLine("  #########  Основные показатели  ########");
            Console.WriteLine(" ");
            Console.WriteLine("[0] - Скорость тела в начальный момент времени:      " +
                speed);
            Console.WriteLine("[1] - Угол наглона тела при запуске:     " +
                angle);
            Console.WriteLine("[2] - Полное время полета тела:      " +
                Math.Round(time, 3));
            Console.WriteLine("[3] - Растояние которое пролетело тело:      " +
                Math.Round((speed * speed * Math.Sin(2 * angle * Math.PI / 180)) / G), 3);
            //Console.WriteLine("[4] - Максимальная высота полета тела:       " +
            //   Math.Round((speed * speed * Math.Sin(angle * Math.PI / 180)
            //   * Math.Sin(angle * Math.PI / 180)) / 2 * G), 3);
            Console.WriteLine(" ");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("[Esc] - вернуться назад");
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
                OutputInformation();
            else if (key.Key != ConsoleKey.Escape)
            {
                Console.WriteLine("Нет такой команды! Повторите.");
                key = Console.ReadKey();
            }
        }

        private void OutputFlightCoordinates()
        {
            Console.Clear();
            Console.WriteLine("############  Показ информации  ############");
            Console.WriteLine("  ##########  Координаты полета  #########");
            Console.WriteLine(" ");

            Console.WriteLine("Введите интрервал с которым хотите получать координаты полёта тела:");
            Console.WriteLine("!Запятая!");
            double diameter =  Convert.ToDouble(Console.ReadLine());
            double flightLength = Math.Round(((speed * speed * Math.Sin(2 * angle * Math.PI / 180)) / G), 3);
            double newangle = angle * Math.PI / 180;
            double t;
            if (diameter <= 0)
            {
                Console.WriteLine("Вы ввели '0' или  число < 0 . Зачем? Введите число > 0");
                diameter = Convert.ToDouble(Console.ReadLine());
            }
            else if(diameter > flightLength)
            {
                Console.WriteLine("Ошибка число больше чем, максимальной длины полета. Введите число заново.");
                diameter = Convert.ToDouble(Console.ReadLine());
            }

            for (double l = 0; l <= flightLength; l += diameter)
            {
                t = l / (speed * Math.Cos(newangle));
                Console.WriteLine("[x]:\t"
                    + Math.Round(l,3) + '\t' +"[y]:\t" + Math.Round((speed * t * Math.Sin(newangle) - (G * t * t) / 2), 3)); 
            }

            Console.WriteLine(" ");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("[Esc] - вернуться назад");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
                OutputInformation();
            else if (key.Key != ConsoleKey.Escape)
            {
                Console.WriteLine("Нет такой команды! Повторите.");
                key = Console.ReadKey();
            }
        }

        private void OutputCoordinatesAtGivenTime()
        {
            Console.Clear();
            Console.WriteLine("############  Показ информации  ############");
            Console.WriteLine(" ##  Координаты в данный момент времени  ##");
            Console.WriteLine(" ");

            Console.WriteLine("Введите время в момент которого вы хотетие узнать положение тела:");
            double timeMoment = Convert.ToDouble(Console.ReadLine());
            if(timeMoment > time)
            {
                Console.WriteLine("В данный момент времени тело уже приземлилось. Контакт с землей был на растоянии от старта:\t" 
                    + Math.Round((speed * speed * Math.Sin(2 * angle * Math.PI / 180)) / G), 3);
            }
            else if(timeMoment < time)
            {
                Console.WriteLine("[x]:\t"
                    + Math.Round((speed * timeMoment * Math.Cos(angle * Math.PI / 180)), 3));
                Console.WriteLine("[y]:\t"
                    + Math.Round((speed * timeMoment * Math.Sin(angle * Math.PI / 180) - G * timeMoment * timeMoment / 2), 3));
            }
            else
            {
                Console.WriteLine("[x]:\t 0");
                Console.WriteLine("[y]:\t 0");
            }    
            Console.WriteLine(" ");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("[Esc] - вернуться назад");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
                OutputInformation();
            else if (key.Key != ConsoleKey.Escape)
            {
                Console.WriteLine("Нет такой команды! Повторите.");
                key = Console.ReadKey();
            }
        }
    }
}
