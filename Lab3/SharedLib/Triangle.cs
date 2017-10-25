using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    [SpecialClass(5)]
    class Triangle : Shape
    {
        public override List<Point> Vertices
        {
            get
            {
                return base.Vertices;
            }

            set
            {
                if (value.Count == 3)
                {
                    base.Vertices = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Triangle only accepts three points");
                }
            }
        }

        public Triangle(List<Point> vertices) : base(vertices)
        {

        }

        public Triangle(Point p1, Point p2, Point p3) : base(new List<Point>() { p1, p2, p3 })
        {

        }

        public override double Area()
        {
            double s = Perimeter() / 2;
            double a = 0;
            double b = 0;
            double c = 0;

            a = this.Vertices[0].Distance(Vertices[1]);
            b = this.Vertices[1].Distance(Vertices[2]);
            c = this.Vertices[2].Distance(Vertices[0]);

            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }

        public bool IsEquilateral()
        {
            double a = 0;
            double b = 0;
            double c = 0;

            a = this.Vertices[0].Distance(Vertices[1]);
            b = this.Vertices[1].Distance(Vertices[2]);
            c = this.Vertices[2].Distance(Vertices[0]);

            if (Utils.IsRelativelyEqual(a, b) && Utils.IsRelativelyEqual(b, c) && Utils.IsRelativelyEqual(c, a))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsScalene()
        {
            double a = 0;
            double b = 0;
            double c = 0;

            a = this.Vertices[0].Distance(Vertices[1]);
            b = this.Vertices[1].Distance(Vertices[2]);
            c = this.Vertices[2].Distance(Vertices[0]);

            if (this.IsIsosceles())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsIsosceles()
        {
            double a = 0;
            double b = 0;
            double c = 0;

            a = this.Vertices[0].Distance(Vertices[1]);
            b = this.Vertices[1].Distance(Vertices[2]);
            c = this.Vertices[2].Distance(Vertices[0]);

            if (Utils.IsRelativelyEqual(a, b) || Utils.IsRelativelyEqual(b, c) || Utils.IsRelativelyEqual(c, a))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return string.Format($"Triangle: {base.ToString()}");
        }
    }
}
