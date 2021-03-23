using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Chapter_07_141_PaintTheButton
{
    class PaintTheButton: Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PaintTheButton());
        }

        public PaintTheButton()
        {
            Title = "Paint the Button";

            //Создание объекта Button как содержимого окна
            Button btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Stretch;
            btn.VerticalAlignment = VerticalAlignment.Center;
            Content = btn;

            //Создание оьекта Canvas как содержимого кнопки
            Canvas canvas = new Canvas();
            canvas.Width = 144;
            canvas.Height = 144;
            btn.Content = canvas;

            //Создание Rectangle как дочернего обьекта Canvas 
            Rectangle rectangle = new Rectangle();
            rectangle.Width = canvas.Width;
            rectangle.Height = canvas.Height;
            rectangle.RadiusX = 24;
            rectangle.RadiusY = 24;
            rectangle.Fill = Brushes.Blue;
            canvas.Children.Add(rectangle);
            Canvas.SetLeft(rectangle, 0);
            Canvas.SetRight(rectangle, 0);

            // Создание Polegon как дочернего объекта Canvas
            Polygon polygon = new Polygon();
            polygon.Fill = Brushes.Yellow;
            polygon.Points = new PointCollection();
            for(int i = 0; i< 5; ++i)
            {
                double angle = i * 4 * Math.PI / 5;
                Point point = new Point(48 * Math.Sin(angle),
                    -48 * Math.Cos(angle));
                polygon.Points.Add(point);
            }
            canvas.Children.Add(polygon);
            Canvas.SetLeft(polygon, canvas.Width / 2);
            Canvas.SetRight(polygon, canvas.Height / 2);
        }

        
    }
}
