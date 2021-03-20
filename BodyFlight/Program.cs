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

        bool statusTXT = false;
        bool statusExcel = false;

        //private double fullFlightTime;
        //private double maximumFlightAltitude;
        //private double flightLength;

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
            time = 2 * speed * Math.Sin(angle * Math.PI / 180) / G;
        }

        private void Menu()
        {
            Console.Clear();
            Console.WriteLine("############  Meню  ############");
            Console.WriteLine("[I] - информаця");
            if(statusExcel)
                Console.WriteLine("[E] - запись в .excel \t [записано]");
            else
                Console.WriteLine("[E] - запись в .excel \t [не записано]");
            if(statusTXT)
                Console.WriteLine("[T] - запись в .txt  \t [записано]");
            else
                Console.WriteLine("[T] - запись в .txt  \t [не записано]");
            Console.WriteLine("[Esc] - выход");
            ConsoleKeyInfo key = Console.ReadKey();
            while (true)
            {
                if (key.Key == ConsoleKey.I)
                    OutputInformation();
                else if (key.Key == ConsoleKey.E)
                    OutputToFileExcel();
                else if (key.Key == ConsoleKey.T)
                    OutputToFileTXT();
                else if (key.Key == ConsoleKey.Escape)
                    break;
                else if((key.Key != ConsoleKey.I) || (key.Key != ConsoleKey.E) || 
                    (key.Key != ConsoleKey.T) || (key.Key != ConsoleKey.Escape))
                     {
                        Console.WriteLine("Нет такой команды! Повторите.");
                        key = Console.ReadKey();
                     }
                    
            }
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
            Console.WriteLine("[3] - Максимальная высота полета тела:       " +
                Math.Round((speed * speed * Math.Sin(angle * Math.PI / 180)
                * Math.Sin(angle * Math.PI / 180)) / 2*G), 3);
            Console.WriteLine("[4] - Растояние которое пролетело тело:      " +
                Math.Round((speed * speed * Math.Sin(2 * angle * Math.PI / 180)) / G), 3);
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

            Console.WriteLine("Введите интрервал времени с которым хотите получать координаты полёта тела:");
            Console.WriteLine("!Вместо запятых нужно узаказывать точку!");
            double intervalTime =  Convert.ToDouble(Console.ReadLine());
            if(intervalTime <= 0)
            {
                Console.WriteLine("Вы ввели '0' или  число < 0 . Зачем? Введите число > 0");
                intervalTime = Convert.ToDouble(Console.ReadLine());
            }
            //for (double t = 0; t <= time; t += intervalTime)
            //    Console.WriteLine("[x]:\t"+ Math.Round((speed * t * Math.Cos(angle * Math.PI / 180)), 3) +'\t' 
            //        + "[y]:\t" + Math.Round((speed * t * Math.Sin(angle * Math.PI / 180) - G * t * t / 2), 3));
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
