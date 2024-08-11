using GMap.NET.WindowsPresentation;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace Map_2GIS
{
    public class Location : MapObject
    {
        PointLatLng location;
        GMapMarker marker;

        public Location(string title, PointLatLng location, string img) : base(title)
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

        //public override double GetDistance(PointLatLng point)
        //{
        //    
        //}

        
    }
}
