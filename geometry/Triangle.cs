using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static geometry.MainWindow;

namespace geometry
{
    public class Triangle
    {
        Point2D A, B, C;
       

        public Triangle()
        {
            A = new Point2D();
            B = new Point2D();
            C = new Point2D();
        }
        public Triangle(Point2D A, Point2D B, Point2D C)
        {
            this.A = new Point2D(A);
            this.B = new Point2D(B);
            this.C = new Point2D(C);
        }

        public Point2D getA()
        {
            return A;
        }
        public Point2D getB() 
        {
            return B;
        }
        public Point2D getC()
        {
            return C;
        }
        public void setA(Point2D A)
        {
            this.A.setX(A.getX());
            this.A.setY(A.getY());
        }
        public void setB(Point2D B)
        {
            this.B.setX(B.getX());
            this.B.setY(B.getY());
        }
        public void setC(Point2D C)
        {
            this.C.setX(C.getX());
            this.C.setY(C.getY());
        }
        public void shiftX(double value)
        {
            A.shiftX(value);
            B.shiftX(value);
            C.shiftX(value);
        }
        public void shiftY(double value)
        {
            A.shiftY(value);
            B.shiftY(value);
            C.shiftY(value);
        }

        public double getRerimetr()
        {
            double result = 0;

            result += A.getDist(B);
            result += B.getDist(C);
            result += C.getDist(A); 
            result = Math.Round(result);

            return result;
        }
        public double getArea()
        {
            double result = 0;

            result =Math.Round( Math.Abs(( ((A.getX() * (B.getY() - C.getY()))   + (B.getX() * (C.getY() - A.getY()))   + (C.getX() * (A.getY() - B.getY()))) / 2 )));

            return result;
        }

        
    }
}
