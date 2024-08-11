using GMap.NET.WindowsPresentation;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Windows;
using GMap.NET.MapProviders;

namespace Map_2GIS
{
    public class Car : MapObject
    {
        public PointLatLng location;
        GMapMarker marker;

        Route route;
        Human human; //должен быть список!!!!!!!
        public PointLatLng endLocation;

        public event EventHandler Arrived;
        
        

        public Car(string title, PointLatLng location, string img) : base(title)
        {
            this.location = location;

            marker = new GMapMarker(location)
            {
                Shape = new Image
                {
                    Width = 50, // ширина маркера
                    Height = 50, // высота маркера
                    ToolTip = title, // всплывающая подсказка
                    Source = new BitmapImage(new Uri("pack://application:,,,/imgs/" + img)) // картинка
                }
            };
        }

        public override PointLatLng GetFocus()
        {
            return location;
        }

        public override GMapMarker GetMarker()
        {
            return marker;
        }

        public void MoveByRoute()
        {
            foreach (var point in route.getLocations())
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    marker.Position = point;
                });
                Thread.Sleep(500);
            }
            // отправка события о прибытии после достижения последней точки маршрута
            Arrived?.Invoke(this, null);
        }

        public void MoveTo(PointLatLng endLocation)
        {
            RoutingProvider routingProvider = GMapProviders.OpenStreetMap;

            // определение маршрута
            MapRoute route = routingProvider.GetRoute(
             location, // начальная точка маршрута
             endLocation, // конечная точка маршрута
             false, // поиск по шоссе
             false, // режим пешехода
             (int)15);

            List<PointLatLng> routePoints = route.Points;
            this.route = new Route("", routePoints);
            this.route.GetMarker();

            Thread newThread = new Thread(new ThreadStart(MoveByRoute));
            newThread.Start();
        }





        //public override double GetDistance(PointLatLng point)
        //{
        //    
        //}


    }

}
