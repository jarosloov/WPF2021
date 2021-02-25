using System;
using System.Windows;

namespace FirstCodeWPF
{
    class SayHello
    {
        [STAThread]
        public static void Main()
        {
            Window win = new Window();
            Application app = new Application();

            win.Title = "!1Jarosloov!";
            win.Show();
            app.Run();
        }
    }
}
