using GMap.NET.WindowsPresentation;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace Map_2GIS
{
    public class Route : MapObject
    {
        List <PointLatLng> locations;
        GMapMarker marker;

        public Route(string title,List <PointLatLng> locations) : base(title)
        {
            this.locations = new List<PointLatLng>();
            
            if (locations.Count < 2)
            {
                return;
            }

            marker = new GMapRoute(locations)
            {
                Shape = new Path()
                {
                    Stroke = Brushes.DarkBlue, // цвет обводки
                    Fill = Brushes.DarkBlue, // цвет заливки
                    StrokeThickness = 4 // толщина обводки
                }
            };
        }


        public List<PointLatLng> getLocations()
        {
            return locations;
        }

        public override PointLatLng GetFocus()
        {
            return locations[0];
        }

        public override GMapMarker GetMarker()
        {
            return marker;
        }

        //public override double GetDistance(PointLatLng locations)
        //{
        //  
        //}

        
    }
}
