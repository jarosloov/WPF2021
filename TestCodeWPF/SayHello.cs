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

            win.Title = "Jarosloov";
            win.Show();
            app.Run();
        }
    }
}
