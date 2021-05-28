using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
        private DispatcherTimer timerAnimation = new DispatcherTimer();
        private double timeAnimation;
        
        // Положение графика 
        private double aW;
        private double aH;
        private double maxWidth;
        private double maxHeight;
        private double coffWidth;
        private double coffHeight;
        private double maxAB;
        private double maxBC;
        private double maxCE;
        
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

        private double _cAB_1;
        private double _cAB_2;
        
        // Участок СВ
        private double _coefficient_f;
        private double _force_F;
        private double _travel_time;
        
        private double xСВ;
        private double yСВ;
        private double _speedС;
        
        private double _cCB_1;
        private double _cCB_2;
        
        // Участок СЕ
        private double _height;
        private double xCE;
        private double yCE;

        private double _cCE_1;
        private double _cCE_2;
        private double _cCE_3;
        private double _cCE_4;
        

        
        const double G = 9.80665;
        
        public MainWindow()
        {
            InitializeComponent();
            timerAnimation.Tick += new EventHandler(OnTimer);
            timerAnimation.Interval = new TimeSpan(0, 0, 0, 0, 20);
        }

        private void Update(bool status)
        {
            InputData();                            // Ввод данных
            switch (status)
            {
                case true:
                    ConstantIntegrations(true);         // c анимацией 
                    StartDataCanvas();                          // Расчет разрешение графика 
                    break;
                case false:
                    ConstantIntegrations(false);    // без аниации
                    StartDataCanvas();                      // Расчет разрешение графика 
                    PrintGraph();                           // Рисование графика
                    break;
            }
           


        }

        private void PrintGraph()
        {
            AreaAB();
            //AreaBC();
            //AreaCE();
        }

        private void ConstantIntegrations(bool status)
        {
            if (status == false)
            {
                ConstantIntegrationsAB(0);         // Расчет постоянных для участка АВ
                Speed();                                // Расчет скоростей 
                ConstantIntegrationsBC(0);
                ConstantIntegrationsCE(0);
            }

            if(status)
            {
                MessageBox.Show("aнимация");
                ConstantIntegrationsAB(0);         // Расчет постоянных для участка АВ
                Speed();                                // Расчет скоростей 
                ConstantIntegrationsBC(_time_τ);
                ConstantIntegrationsCE(_travel_time);
                timeAnimation = 0;
                timerAnimation.Start();
            }
        }
        
        

        private void Speed()
        {
            speedA.Text = initial_speed.Text + "м/с";
            _speedB = -_cAB_1 * _coefficient_μ / (Math.Exp(_coefficient_μ * _time_τ / _body_mass) * _body_mass);
            speedB.Text = Math.Round(_speedB, 2) + "м/с";
            _speedС = ((_force_F / _body_mass) * _travel_time * _travel_time / 2) - _coefficient_f * G * _travel_time +
                      _speedB;
            speedC.Text = Math.Round(_speedС, 2) + "м/с";
        }
        
        // Стартовые данные для рисования
        private void StartDataCanvas()
        {
            aW = canvas.ActualWidth / 1.5;          // ширина канваса 
            aH = canvas.ActualHeight / 2;           // высота канваса
            maxAB =  -62.426 / Math.Exp(_coefficient_μ / _body_mass * _time_τ) +
                     (_driving_force / _body_mass - G * Math.Sin(_angle)) * _time_τ / _coefficient_μ / _body_mass + 64.426 + 100;
            maxBC = _force_F / _body_mass * _travel_time * _travel_time * _travel_time / 6 -
                _coefficient_f * G * _travel_time * _travel_time / 2 + _cCE_1  + _cCE_2;
            maxCE = _speedС  ;
            maxWidth = maxAB;
            maxHeight = -62.426 / Math.Exp(_coefficient_μ / _body_mass * _time_τ) +
                        (_driving_force / _body_mass - G * Math.Sin(_angle)) * _time_τ / _coefficient_μ / _body_mass + 64.426;;
            coffWidth = -aW / maxWidth;
            coffHeight = -aH / maxHeight;
        }

        private void ConstantIntegrationsAB(double time)
        {
            _cAB_1 = -_initial_speed * Math.Exp(_coefficient_μ / _body_mass * time) * _body_mass /_coefficient_μ;
            _cAB_2 = -_cAB_1 / Math.Exp(_coefficient_μ / _body_mass * time) + _driving_force / _body_mass;
        }
        // доделать
        private void ConstantIntegrationsBC(double time)
        {
            _cCB_1 = -time * (_force_F / _body_mass - _coefficient_f * G) + _speedB;
            _cCB_2 = _coefficient_f * G * time * time / 2 + _force_F * time * time / (2 * _body_mass) + _cCB_1 * time;
        }
        
        private void ConstantIntegrationsCE(double time)
        {
            // х
            _cCE_1 = _speedС;
            _cCE_2 = -_cCE_1 * time;
            //  y
            _cCE_3 = 0;
            _cCE_4 = 0;
        }
        
        private void OnTimer(object sender, EventArgs e)
        {
            timeAnimation += 0.1;


                xAB = _cAB_1 / Math.Exp(_coefficient_μ / _body_mass * timeAnimation) - _driving_force / _body_mass + _cAB_2;
                flipXAB = aW + coffWidth * xAB * Math.Cos(_angle);
                flipYAB = aH + coffHeight - xAB * Math.Sin(_angle);
                plineAB.Points.Add(new Point(flipXAB, flipYAB));
                
            
            if (timeAnimation >= _time_τ)
                timerAnimation.Stop();

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
            for(double t = 0; t <= _time_τ; t += 0.1)
            {
                xAB = -62.426 / Math.Exp(_coefficient_μ / _body_mass * t) +
                      (_driving_force / _body_mass - G * Math.Sin(_angle)) * t / _coefficient_μ / _body_mass + 64.426;
                
                //xAB = _cAB_1 / Math.Exp(_coefficient_μ / _body_mass * t) - _driving_force / _body_mass + _cAB_2;
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
                xСВ = ((_force_F / _body_mass) * t * t * t) / 6 - _coefficient_f * G * t * t / 2 + _speedB;
                plineBC.Points.Add(new Point( flipXAB + coffWidth * xСВ,flipYAB));
            }
        }

        
        private void AreaCE()
        {
            plineCE.Points.Add(new Point( flipXAB + coffWidth * xСВ,flipYAB));
            for (double t = 0;  Math.Abs(yCE) <= _height; t += 0.1)
            {
                xCE = _cCE_1 * t + _cCE_2;
                yCE = G * t * t / 2 ;
                plineCE.Points.Add(new Point( flipXAB + coffWidth * xСВ +coffWidth  * xCE, flipYAB + coffHeight * (-yCE)));
            }
            
        }
        private void WhiteTheme(object sender, RoutedEventArgs e)
        {
            window.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            regionAB.Background = new SolidColorBrush(Color.FromRgb(235, 235, 235));
            regionCB.Background = new SolidColorBrush(Color.FromRgb(235, 235, 235));
            regionCE.Background = new SolidColorBrush(Color.FromRgb(235, 235, 235));
            tabControl1.Background = new SolidColorBrush(Color.FromRgb(235, 235, 235));
        }

        private void DarkTheme(object sender, RoutedEventArgs e)
        {
            window.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
            regionAB.Background = new SolidColorBrush(Color.FromRgb(192, 192, 192));
            regionCB.Background = new SolidColorBrush(Color.FromRgb(192, 192, 192));
            regionCE.Background = new SolidColorBrush(Color.FromRgb(192, 192, 192));
            tabControl1.Background = new SolidColorBrush(Color.FromRgb(195, 195, 195));
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
            Clear();
            Update(false);
        }
        
        private void PlayAnimation(object sender, RoutedEventArgs e)
        {
            Clear();
            Update(true);
        }

        private void Clear()
        {
            plineAB.Points.Clear();
            plineBC.Points.Clear();
            plineCE.Points.Clear();
            xAB = xCE = xСВ = 0;
            yAB = yCE = yСВ = 0;
        }
        private void StokData(object sender, RoutedEventArgs e)
        {
            body_mass.Text = "4,5";
            angle.Text = "45";
            initial_speed.Text = "18";
            coefficient_μ.Text = "0,5";
            driving_force.Text = "9";
            time_τ.Text = "3";
            
            resistance_force.Text = "μ * V";
            
            coefficient_f.Text = "0,2";
            force_F.Text = "50";
            travel_time.Text = "4";

            height.Text = "3";
        }
    }
}
