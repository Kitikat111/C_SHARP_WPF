using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geometry
{
    public class Point2D
    {
        private double X;
        private double Y;

       
        public Point2D()
        {
            X = 0;
            Y = 0;
        }

        public Point2D(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public Point2D(Point2D p)
        {
            X = p.X;
            Y = p.Y;
        }

        public void setX(double X)
        {
            this.X = X;
        }
        public void setY(double Y)
        {
            this.Y = Y;
        }

        public double getX()
        {
            return X;
        }
        public double getY()
        {
            return Y;
        }
        public void shiftX(double value)
        {
            X += value;
        }
        public void shiftY(double value)
        {
            Y += value;
        }

        public double getDist(Point2D other)
        {
            return Math.Sqrt(Math.Pow(other.X, 2) + Math.Pow(other.Y, 2));
        }
    }
}
