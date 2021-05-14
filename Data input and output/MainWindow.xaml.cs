using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Text.RegularExpressions;

namespace WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private DispatcherTimer timer = new DispatcherTimer();
        private double time;


        

        public MainWindow()
        {
            InitializeComponent();

            timer.Tick += new EventHandler(OnTimer);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);

        }

        public void OnTimer(object sender, EventArgs args)
        {
            Random rnd = new Random();
            
            time += 1;

            double aW = canvas.ActualWidth / 2;
            double aH = canvas.ActualHeight / 2;
            double maxWidth = 2 * Math.Sin(100) * (100 + Math.Sin(100 * 100 / 2));
            double maxHeight = 2 * Math.Cos(100) * (100 + Math.Sin(100 * 100 / 2));
            double coffWidth = aW / maxWidth;
            double coffHeight = aH / maxHeight;

            string message;
            
            double ang = Convert.ToDouble(angle.Text);
            double see = Convert.ToDouble(speed.Text);
            /**
            
            const double G = 9.80665;
            
            double x = Math.Round((see * t * Math.Cos(ang * Math.PI / 180)), 3);
            double y = Math.Round((see * t * Math.Sin(ang * Math.PI / 180) - G * t * t / 2), 3);
            pline.Points.Add(new Point(aW + coffWidth * x, -(aH + coffHeight * y)));
            **/

            double coorX = aW + coffWidth * ang * (Math.Sin(time) * (time + Math.Sin(time * time / 2)));
            double coorY = aH + coffHeight * see * (Math.Cos(time) * (time + Math.Sin(time * time / 2)));
            //pline.Points.Add(new Point(time * 5, 200+rnd.Next(-100,100)));
            pline.Points.Add(new Point(coorX, coorY));
            message = "[x]: " + coorX + "[y]: " + coorY;
            if (time > 100)
                timer.Stop();
            MessageBox.Show(message, "Координаты");

        }

        //private void Start()
        //{
        //       pline.Points.Add(new Point(0,0));
            
        //}



        private static readonly Regex _regex = new Regex("[^0-9.]"); 
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pline.Points.Clear();
            time = 0;
            timer.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            speed.Text = "";
            angle.Text = "";
        }
    }
}
