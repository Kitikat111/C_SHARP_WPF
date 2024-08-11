using GMap.NET.WindowsPresentation;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows;

namespace Map_2GIS
{
    public class Human : MapObject
    {
        PointLatLng location;
        GMapMarker marker;

        // обработчик события прибытия такси
        public void CarArrived(object sender, EventArgs e)
        {
            MessageBox.Show("Машина прибыла");
        }


        public Human(string title, PointLatLng location, string img) : base(title)
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
        //    // Реализация метода для определения расстояния до произвольной точки для человека
        //    // ...
        //    return 0; // Замените на реальную реализацию
        //}

       
    }
}
