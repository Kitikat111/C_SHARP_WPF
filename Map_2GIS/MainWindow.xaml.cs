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
using System.Device.Location;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Security.Cryptography;

namespace Map_2GIS
{
    //поменять название чтобы их вводил пользователь
    public partial class MainWindow : Window
    {
        List <PointLatLng> point = new List<PointLatLng>();

        List<MapObject> objects = new List<MapObject>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MapLoaded(object sender, RoutedEventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache; // настройка доступа к данным
            Map.MapProvider = BingMapProvider.Instance;

            Map.MinZoom = 2;
            Map.MaxZoom = 17;
            Map.Zoom = 15;

            Map.Position = new PointLatLng(55.012823, 82.950359);
            Map.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            Map.CanDragMap = true;
            Map.DragButton = MouseButton.Left;
        }

        
        private void Map_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            switch (type.SelectedIndex)
            {
                case 0:
                    objects.Add(new Human("human", Map.FromLocalToLatLng((int)e.GetPosition(Map).X, (int)e.GetPosition(Map).Y), "cat.png"));
                    Map.Markers.Add(objects[objects.Count - 1].GetMarker());
                    break;

                case 1:
                    objects.Add(new Car("car", Map.FromLocalToLatLng((int)e.GetPosition(Map).X, (int)e.GetPosition(Map).Y), "car.png"));
                    Map.Markers.Add(objects[objects.Count - 1].GetMarker());
                    ((Car)objects[1]).Arrived += ((Human)objects[0]).CarArrived;
                    break;

                case 2:
                    objects.Add(new Location("location", Map.FromLocalToLatLng((int)e.GetPosition(Map).X, (int)e.GetPosition(Map).Y), "located.png"));
                    Map.Markers.Add(objects[objects.Count - 1].GetMarker());
                    break;

                case 3:
                    point.Add(Map.FromLocalToLatLng((int)e.GetPosition(Map).X, (int)e.GetPosition(Map).Y));
                    break;

                case 4:
                    point.Add(Map.FromLocalToLatLng((int)e.GetPosition(Map).X, (int)e.GetPosition(Map).Y));
                    break;

                default:
                    MessageBox.Show("unknown type");
                    break;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e) // очиcтка
        {
            point.Clear();
            Map.Markers.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //добавление маршрута и области ручками
        {
            if (type.SelectedIndex == 4)
            {
                objects.Add(new Route("", point));
                Map.Markers.Add(objects[objects.Count - 1].GetMarker());
            }
            if (type.SelectedIndex == 3)
            {
                objects.Add(new Area("", point));
                Map.Markers.Add(objects[objects.Count - 1].GetMarker());
            }
            
        }

        private void type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            point.Clear();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) //такси
        {
            ((Car)objects[1]).MoveTo(objects[0].GetFocus());

            //// определение маршрута
            //MapRoute route = GMap.NET.MapProviders.BingMapProvider.Instance.GetRoute(
            // new PointLatLng(55.016215, 82.948772), // начальная точка маршрута
            // new PointLatLng(55.016667, 82.949546), // конечная точка маршрута
            // false, // поиск по шоссе (false - включен)
            // false, // режим пешехода (false - выключен)
            // (int)15);
            //// получение точек маршрута
            //List<PointLatLng> routePoints = route.Points;


            //GMapMarker marker = new GMapRoute(routePoints)
            //{
            //    Shape = new Path()
            //    {
            //        Stroke = Brushes.DarkBlue, // цвет обводки
            //        Fill = Brushes.DarkBlue, // цвет заливки
            //        StrokeThickness = 4 // толщина обводки
            //    }
            //};
            //Map.Markers.Add(marker);

        }
    }
}
