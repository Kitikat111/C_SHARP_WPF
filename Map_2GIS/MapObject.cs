using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Map_2GIS
{
    public abstract class MapObject
    {
        public string title;
        public DateTime CreationDate;

        public MapObject(string title)
        {
            this.title = title;
            CreationDate = DateTime.Now;
        }

        public string GetTitle()
        {
            return title;
        }

        public DateTime GetCreationDate()
        {
            return CreationDate;
        }

        public abstract PointLatLng GetFocus();

        public abstract GMapMarker GetMarker();
        

        //public double GetDistance(PointLatLng point)
        //{
        //   
        //}
    }
}
