using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
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

namespace FirstCourseWork
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private double time;
        
        // Участок АВ
        private double _body_mass;
        private double _initial_speed;
        private double _driving_force;
        private double _resistance_force;
        private double _coefficient_μ;
        private double _angle;
        private double _lengthАB;
        
        
        const double G = 9.80665;

        public MainWindow()
        {
            
            InitializeComponent();
            timer.Tick += new EventHandler(OnTimer);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);


        }

        private void OnTimer(object sender, EventArgs e)
        {

            time += 0.1;
            /**
            double aW = canvas.ActualWidth ; // середина канваса 
            double aH = canvas.ActualHeight / 2;
            //pline.Points.Add(new Point(aW, aH));

            double maxWidth = 2 * Math.Cos(45 * Math.PI / 180) * (-162 * Math.Pow(Math.E, -1 / 9 * 3) + 162.078);
            double maxHeight = -(-162 * Math.Pow(Math.E, -1 / 9 * 3) + 162.078) * Math.Sin(45 * Math.PI / 180) * 2;

            // координаты полёта
            double coorX = aW + Math.Cos(45 * Math.PI / 180) * (-162 * Math.Pow(Math.E, -1 / 9 * time) + 162.078) * maxWidth;
            double coorY = aH -(-162 * Math.Pow(Math.E, -1 / 9 * time) + 162.078) * Math.Sin(45 * Math.PI / 180)  * maxHeight;
            
            //pline.Points.Add(new Point(time * 5, 200+rnd.Next(-100,100)));
            
            pline.Points.Add(new Point(coorX, coorY));
            if (time > 100)
                timer.Stop();
            **/
            double aW = canvas.ActualWidth;     // ширена канваса 
            double aH = canvas.ActualHeight /2;    // висота канваса
            double maxWidth = (-180 * Math.Pow(Math.E, -0.1 * 8) + 180) * Math.Cos(45 * Math.PI / 180) + (1.85 * 8 * 8 * 8 - 0.981 * 8 * 8 + 13.33474 * 8) + 721.81263 +  94.28674 * 10; //1024.08237
            double maxHeight = (-180 * Math.Pow(Math.E, -0.1 * 8) + 180) * Math.Sin(45 * Math.PI / 180) +100;
            double coffWidth = -aW / maxWidth;
            double coffHeight = -aH / maxHeight;
            double coorX = aW ;
            double coorY = aH ;
            double angl = 45 * Math.PI / 180;
            double _x = (-180 * Math.Pow(Math.E, -0.1 * time) + 180) * Math.Cos(angl);
            double _y = (-180 * Math.Pow(Math.E, -0.1 * time) + 180) * Math.Sin(angl);

            if (time < 3)
            {
                coorX = aW + coffWidth *  _x;
                coorY = aH + coffHeight *  _y;
                plineAB.Points.Add(new Point(coorX , coorY));
            }
            
            if(time > 2 && time < 7)
            {
                coorX = aW + coffWidth * ((1.85 * time * time * time -0.981 * time * time + 13.33474 * time));
                coorY = aH + coffHeight * 32.98845;
                plineBC.Points.Add(new Point(coorX , coorY));
            }

            if ( time < 2.5)
            {
                coorX =   aW - 220 + (coffWidth * 94.28674 * time);
                coorY = aH - coffHeight * (9.81 * (time * time) / 2 + 3) - 60;
                plineCE.Points.Add(new Point(coorX , coorY));
            }

           

                /**
                
                
                
                double x = Math.Round((see * t * Math.Cos(ang * Math.PI / 180)), 3);
                double y = Math.Round((see * t * Math.Sin(ang * Math.PI / 180) - G * t * t / 2), 3);
                pline.Points.Add(new Point(aW + coffWidth * x, -(aH + coffHeight * y)));
                **/

                //double coorX = aW + coffWidth * (Math.Sin( time) * time) /2;
                //double coorY = aH + coffHeight * (Math.Cos( time) * time) /2;
                //pline.Points.Add(new Point(time * 5, 200+rnd.Next(-100,100)));

                
            if (time > 10)
                timer.Stop();
        }

        private void WhiteTheme(object sender, RoutedEventArgs e)
        {
            window.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            regionAB.Background = new SolidColorBrush(Color.FromRgb(235, 235, 235));
        }

        private void DarkTheme(object sender, RoutedEventArgs e)
        {
            window.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
            regionAB.Background = new SolidColorBrush(Color.FromRgb(192, 192, 192));
        }

        private void LinkToProject(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/jarosloov/Kursach");
        }

        private void LinkToMyProfile(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/jarosloov");
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ButtonStart(object sender, RoutedEventArgs e)
        {
            /*
            _body_mass = Convert.ToDouble(body_mass);
            _angle = Convert.ToDouble(angle);
            _coefficient_μ = Convert.ToDouble(coefficient_μ);
            _driving_force = Convert.ToDouble(driving_force);
            _initial_speed = Convert.ToDouble(initial_speed);
            _resistance_force = Convert.ToDouble(resistance_force);
            _lengthАB = Convert.ToDouble(lengthАB);
            */
            timer.Start();
        }
    }
}
