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

            win.Title = "JAROSLOOV";
            win.Show();
            app.Run();
        }
    }
}
