using GMap.NET.WindowsPresentation;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace Map_2GIS
{
    public class Area : MapObject
    {

        List<PointLatLng> locations;
        GMapMarker marker;

        public Area(string title, List<PointLatLng> locations) : base(title)
        {
            this.locations = locations;

            if (locations.Count < 3)
            {

                return;
            }
            marker = new GMapPolygon(locations)
            {
                Shape = new Path
                {
                    Stroke = Brushes.Black, // стиль обводки
                    Fill = Brushes.DeepPink, // стиль заливки
                    Opacity = 0.5 // прозрачность
                }
            };


        }

        public override PointLatLng GetFocus()
        {
            return locations[locations.Count / 2];
        }

        public override GMapMarker GetMarker()
        {
            return marker;
        }

        //public double GetDistance(PointLatLng point)
        //{
        //    
        //}
    }
}
