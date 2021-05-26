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
        
        // Положение граффика 
        private double aW;
        private double aH;
        private double maxWidth;
        private double maxHeight;
        private double coffWidth;
        private double coffHeight;
        
        // Участок АВ
        private double _body_mass;
        private double _initial_speed;
        private double _driving_force;          //Q
        private double _resistance_force;
        private double _coefficient_μ;
        private double _angle;
        private double _time_τ;

        private double xAB;
        private double yAB;
        private double flipXAB;
        private double flipYAB;
        private double _speedB;
        
        // Участок СВ
        private double _coefficient_f;
        private double _force_F;
        private double _travel_time;
        
        private double xСВ;
        private double yСВ;
        private double _speedС;
        // Участок СЕ
        private double _height;
        
        const double G = 9.80665;
        
        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(OnTimer);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        private void Update()
        {
            InputData();
            StartDataCanvas();
            AreaAB();
            AreaBC();
        }
        
        // Стартовые данные для рисования
        private void StartDataCanvas()
        {
            aW = canvas.ActualWidth;        // ширина канваса 
            aH = canvas.ActualHeight / 2;   // высота канваса
            maxWidth = (-162 / Math.Exp(_coefficient_μ / _body_mass * 3) - _driving_force / _body_mass + 164)
                      + ((_force_F / _body_mass) * 4 * 4 * 4) / 6 - _coefficient_f * G * 4 * 4 / 2 + 12.9;
            maxHeight = -162 / Math.Exp(_coefficient_μ / _body_mass * 3) - _driving_force / _body_mass + 164;
            coffWidth = -aW / maxWidth;
            coffHeight = -aH / maxHeight;
        }

        private void OnTimer(object sender, EventArgs e)
        {
            time += 0.1;
            /**
            if (time < 3)
            {

                coorX = aW + coffWidth * _x;
                coorY = aH + coffHeight * _y;
                plineAB.Points.Add(new Point(coorX, coorY));
            }

            if (time > 2 && time < 7)
            {
                //coorX = aW + coffWidth * ((1.85 * time * time * time -0.981 * time * time + 13.33474 * time));
                //coorY = aH + coffHeight * 32.98845;
                //plineBC.Points.Add(new Point(coorX , coorY));
            }

            if ( time < 2.5)
            {
                //coorX =   aW - 220 + (coffWidth * 94.28674 * time);
                //coorY = aH - coffHeight * (9.81 * (time * time) / 2 + 3) - 60;
                //plineCE.Points.Add(new Point(coorX , coorY));
            }
            **/
            if (time > 100)
                timer.Stop();
        }

        private void InputData()
        {
            _body_mass = Convert.ToDouble(body_mass.Text);
            _angle = Convert.ToDouble(angle.Text);
            _angle = _angle * Math.PI / 180;
            _coefficient_μ = Convert.ToDouble(coefficient_μ.Text);
            _driving_force = Convert.ToDouble(driving_force.Text);
            _initial_speed = Convert.ToDouble(initial_speed.Text);
            _time_τ = Convert.ToDouble(time_τ.Text);
            
            _coefficient_f = Convert.ToDouble(coefficient_f.Text);
            _force_F = Convert.ToDouble(force_F.Text);
            _travel_time = Convert.ToDouble((travel_time.Text));

            _height = Convert.ToDouble(height.Text);
        }
        
        private void AreaAB()
        {
            
            _speedB = 162 * _coefficient_μ / (Math.Exp(_coefficient_μ * _time_τ / _body_mass) * _body_mass);
            
            resistance_force.Text = "μ * V";
            speedB.Text = Math.Round(_speedB, 2)  + "м/с";
            speedA.Text = initial_speed.Text + "м/с";
            
            // Рисование графика
            for(double t = 0; t <= 3; t += 0.1)
            {
                xAB = -162 / Math.Exp(_coefficient_μ / _body_mass * t) - _driving_force / _body_mass + 164;
                flipXAB = aW + coffWidth * xAB * Math.Cos(_angle);
                flipYAB = aH + coffHeight - xAB * Math.Sin(_angle);
                plineAB.Points.Add(new Point(flipXAB, flipYAB));
            }
            
        }

        private void AreaBC()
        {
            plineBC.Points.Add(new Point(flipXAB, flipYAB));
            for (double t = 0; t <= _travel_time; t += 0.1)
            {
                xСВ = ((_force_F / _body_mass) * t * t * t) / 6 - _coefficient_f * G * t * t / 2 + 12.9;
                plineBC.Points.Add(new Point( flipXAB + coffWidth * xСВ,flipYAB));
            }
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
            plineAB.Points.Clear();
            plineBC.Points.Clear();
            //time = 0;
            //timer.Start();
            Update();
        }

        private void StokData(object sender, RoutedEventArgs e)
        {
            body_mass.Text = "4,5";
            angle.Text = "45";
            initial_speed.Text = "18";
            coefficient_μ.Text = "0,5";
            driving_force.Text = "9";
            time_τ.Text = "3";

            coefficient_f.Text = "0,2";
            force_F.Text = "50";
            travel_time.Text = "4";

            height.Text = "3";
        }
    }
}
