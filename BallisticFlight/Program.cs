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
        public BallisticFlight()
        {
            Menu();
        }

        private void Menu()
        {
            Console.WriteLine("############  Meню  ############");
            if (statusInput)
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
                else if (key.Key == ConsoleKey.D)
                {
                    Input();
                    Console.Clear();
                }
                else if (key.Key == ConsoleKey.Escape)
                    Process.GetCurrentProcess().Kill();
                else if ((key.Key != ConsoleKey.I) || (key.Key != ConsoleKey.D) || (key.Key != ConsoleKey.E) ||
                    (key.Key != ConsoleKey.T) || (key.Key != ConsoleKey.Escape))
                {
                    Console.WriteLine("Нет такой команды! Повторите.");
                    key = Console.ReadKey();
                }
            }
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
                    Menu();
                }
                else
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
