using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;


namespace FirstCourseWork
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private static readonly Regex _regex = new Regex("[^0-9.]");
        private double time;
        const double G = 9.80665;

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
        private double _coefficient_μ;
        private double _angle;
        private double _time_τ;

        private double xAB;
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
        
        // Участок СЕ
        private double _height;
        private double xCE;
        private double yCE;

        // Проверки (защита от дурака)

        private bool statusInput = true;


        // Для сокращения

        private double b;
        private double m;
        
        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(OnTimer);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
        }

        private void Start()
        {
            Clear();
            InputData();                            // Ввод данных
            if (!statusInput) return;
            ConstantIntegrationsAB();               // Расчет постоянных для участка АВ
            ellipse.Fill = new SolidColorBrush(Color.FromRgb(157, 129, 186));
            timer.Start();
            Speed();                                // Расчет скоростей 
            StartDataCanvas();                      // Расчет разрешение графика 
        }

        private static readonly Regex regex = new Regex("[^0-9,]");
        private static bool IsTextAllowed(string text)
        {
            return !regex.IsMatch(text);
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private void Speed()
        {
            _speedB = b / m - m * _cAB_1 * Math.Exp(-m * _time_τ);
            _speedС = _force_F * _travel_time * _travel_time / (2 * _body_mass) - _coefficient_f * G * _travel_time +
                      _speedB;
            if (_initial_speed <= 0 || _speedB <= 0 || _speedС <= 0)
            {
                timer.Stop();
                Clear();
                ellipse.Fill = new SolidColorBrush(Color.FromRgb(235, 235, 235));
                MessageBox.Show("Нулевая скорость", "🔥Ошибка🔥",
                MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                speedC.Text = Math.Round(_speedС, 1) + "м/с";
                speedB.Text = Math.Round(_speedB, 1) + "м/с";
                speedA.Text = Math.Round(_initial_speed, 1) + "м/с";
            }
        }
        private void StartDataCanvas()
        {
            aW = canvas.ActualWidth;
            aH = canvas.ActualHeight / 2;
            maxAB = _cAB_1 / Math.Exp(m * _time_τ) - b * _time_τ / m  + _cAB_2;
            maxBC = _force_F * _travel_time * _travel_time * _travel_time / (6 * _body_mass) -
                    _coefficient_f * G * _travel_time * _travel_time / 2 - _speedB  * _travel_time;
            maxCE = 1000;
            maxWidth = maxAB + maxBC + maxCE;
            maxHeight = _cAB_1 / Math.Exp(_coefficient_μ / _body_mass * _time_τ) - _driving_force / _body_mass + _cAB_2 + 100;
            coffWidth = -aW / maxWidth;
            coffHeight = aH / maxHeight;
        }
        private void ConstantIntegrationsAB()
        {
            _cAB_1 = (_initial_speed - b/m)/-m;
            _cAB_2 = -_cAB_1;
        }

        private void OnTimer(object sender, EventArgs e)
        {
            time +=  0.1;
            if (time <= _time_τ)
            {
                xAB = _cAB_1 / Math.Exp(m * time) - b * time / m + _cAB_2;
                flipXAB = aW + coffWidth * xAB * Math.Cos(_angle);
                flipYAB = aH + coffHeight * (- xAB) * Math.Sin(_angle);
                polyline.Points.Add(new Point(flipXAB, flipYAB));
            }
            else if(time - _time_τ <= _travel_time)
            {
                xСВ = _force_F * time * time * time / (6 * _body_mass) - _coefficient_f * G * time * time / 2 -
                      _speedB * time;
                polyline.Points.Add(new Point(flipXAB + coffWidth * xСВ, flipYAB));
            }
            else if(flipYAB + coffHeight * yCE <= aH)
            {
                xCE = _speedС * (time - _time_τ - _travel_time);
                yCE = G * (time - _time_τ - _travel_time) * (time - _time_τ - _travel_time) / 2 ;
                polyline.Points.Add(new Point(flipXAB + coffWidth * (xCE +xСВ) , flipYAB + coffHeight * yCE));
            }
            else 
                timer.Stop();
            Canvas.SetLeft(ellipse, polyline.Points.Last().X - ellipse.Width / 2.0);
            Canvas.SetTop(ellipse, polyline.Points.Last().Y - ellipse.Height / 2.0); 
        }

        private void InputData()
        {
            if (body_mass.Text == "" &&  angle.Text == "" && 
                coefficient_μ.Text == "" &&  driving_force.Text == "" &&  
                initial_speed.Text == "" && 
                time_τ.Text == "" &&  height.Text == "")
            {
                MessageBox.Show("Не все поля были заполнены ", "🔥Ошибка🔥",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                statusInput = false;
            }
            else if (body_mass.Text == "0" && angle.Text == "0" &&
                coefficient_μ.Text == "0" && driving_force.Text == "0" &&
                initial_speed.Text == "0" &&
                time_τ.Text == "0" && height.Text == "0")
            {
                MessageBox.Show("Нулевые данные", "🔥Ошибка🔥", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                statusInput = false;
            }
            else if (Convert.ToDouble(angle.Text) > 90 || Convert.ToDouble(angle.Text) == 0)
            {
                MessageBox.Show("Невозможные углы", "🔥Ошибка🔥",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                statusInput = false;
            }
            else if(statusInput)
            {
                _body_mass = Convert.ToDouble(body_mass.Text);
                _angle = Convert.ToDouble(angle.Text) * Math.PI / 180;
                _coefficient_μ = Convert.ToDouble(coefficient_μ.Text);
                _driving_force = Convert.ToDouble(driving_force.Text);
                _initial_speed = Convert.ToDouble(initial_speed.Text);
                _time_τ = Convert.ToDouble(time_τ.Text);
                _coefficient_f = Convert.ToDouble(coefficient_f.Text);
                _force_F = Convert.ToDouble(force_F.Text);
                _travel_time = Convert.ToDouble((travel_time.Text));
                _height = Convert.ToDouble(height.Text);
                // для краткости
                b = _driving_force / _body_mass - G * Math.Sin(_angle);
                m = _coefficient_μ / _body_mass;
            }
            
        }
        private void LinkToProject(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/jarosloov/WPF2021");
        }

        private void LinkToMyProfile(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/jarosloov");
        }
        //  Выход из программы
        private void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        
        private void ButtonStart(object sender, RoutedEventArgs e)
        {
            Start();
        }

        private void Clear()
        {
            polyline.Points.Clear();
            time = xAB = xCE = xСВ = yCE = yСВ = 0;
            speedC.Text = "........";
            speedB.Text = "........";
            speedA.Text = "........";
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

