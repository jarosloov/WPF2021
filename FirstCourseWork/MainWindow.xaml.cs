using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace FirstCourseWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
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
    }
}
