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

namespace geometry
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point2D p = new Point2D();
        Random random = new Random();
        Triangle t;
        Rectangle R;
        public MainWindow()
        {
            InitializeComponent();
        }

        void drawPoint (Point2D point)
        {
            R = null;
            t = null;
            Ellipse myEllipse = new Ellipse();
            myEllipse.Stroke = System.Windows.Media.Brushes.Black;
            myEllipse.Fill = System.Windows.Media.Brushes.DarkBlue;
            myEllipse.HorizontalAlignment = HorizontalAlignment.Left;
            myEllipse.VerticalAlignment = VerticalAlignment.Center;

            myEllipse.Width = 5;
            myEllipse.Height = 5;

            myEllipse.RenderTransform = new TranslateTransform(point.getX(), point.getY());
            S.Content = 0;
            P.Content = 0;
            Canvas.Children.Add(myEllipse);
        }

        void drawLine (Point2D sp, Point2D ep) 
        {
            Line line = new Line();
            line.Stroke = Brushes.HotPink;
            line.StrokeThickness = 3;
            line.X1 = sp.getX();
            line.Y1 = sp.getY();

            line.X2 = ep.getX();
            line.Y2 = ep.getY();

            Canvas.Children.Add(line);
        }

        void drawTriangle(Triangle tri)
        {
            drawLine(tri.getA(), tri.getB());
            drawLine(tri.getB(), tri.getC());
            drawLine(tri.getA(), tri.getC());
        }

        void drawRectangle(Rectangle rect)
        {
            drawLine(rect.getA(), rect.getB());
            drawLine(rect.getB(), rect.getC());
            drawLine(rect.getC(), rect.getD());
            drawLine(rect.getA(), rect.getD());
        }

        private void Point_Click(object sender, RoutedEventArgs e)
        {
            p.setX(double.Parse(pX.Text));
            p.setY(double.Parse(pY.Text));

            Canvas.Children.Clear();
            drawPoint(p);
        }

        private void ScrollBarX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double val = e.OldValue - e.NewValue;

            if (R != null)
            {
                R.shiftX(val);
            }
            if (t != null)
            {
                t.shiftX(val);
            }
            else
            {
                p.shiftX(val);
            }
            

            Canvas.Children.Clear();
            if (R != null)
            {
                drawRectangle(R);
            }
            else if (t != null)
            {
                drawTriangle(t);
            }
            else
            {
                drawPoint(p);
            }
            
        }

        private void ScrollBarY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double val = e.OldValue - e.NewValue;

            if (R != null)
            {
                R.shiftY(val);
            }
           if (t != null)
            {
                t.shiftY(val);
            }
            else
            {
                p.shiftY(val);
            }
           

            Canvas.Children.Clear();

            if (R != null)
            {
                drawRectangle(R);
            }
            else if (t != null)
            {
                drawTriangle(t);
            }
            else
            {
                drawPoint(p);
            }
            
        }


        private void Triangle_Click(object sender, RoutedEventArgs e)
        {
            R = null; 
            Point2D A = new Point2D((random.NextDouble() * 500), (random.NextDouble() * 500));
            Point2D B = new Point2D((random.NextDouble() * 500), (random.NextDouble() * 500));
            Point2D C = new Point2D((random.NextDouble() * 500), (random.NextDouble() * 500));

            t = new Triangle(A, B, C);
            Canvas.Children.Clear();
            drawTriangle(t);
            P.Content = t.getRerimetr();
            S.Content = t.getArea();


        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            t = null;
            Point2D A = new Point2D((random.NextDouble() * 250), (random.NextDouble() * 250));
            Point2D B = new Point2D(250 + (random.NextDouble() * 250), (random.NextDouble() * 250));
            Point2D C = new Point2D(250 + random.NextDouble() * 250, 250 + random.NextDouble() * 250);
            Point2D D = new Point2D((random.NextDouble() * 250), 250 + (random.NextDouble() * 250));
            


            R = new Rectangle(A, B, C, D);
            Canvas.Children.Clear();
            drawRectangle(R);
            P.Content = R.getRerimetr();
            S.Content = R.getArea();
        }
    }
}
