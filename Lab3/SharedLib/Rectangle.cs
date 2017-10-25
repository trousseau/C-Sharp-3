using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    [SpecialClass(3)]
    class Rectangle : Shape
    {
        public override List<Point> Vertices
        {
            get
            {
                return base.Vertices;
            }

            set
            {
                var length = value.Count;

                if (length == 2 || length == 4)
                {
                    var normalizedList = Normalize(value);
                    base.Vertices = normalizedList;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Rectangle only accepts two or four points");
                }

            }
        }

        public double Width
        {
            get
            {
                var bounds = Utils.GetBoundingBox(Vertices);
                var width = bounds.Item3 - bounds.Item1;
                return width;
            }
        }
        public double Height
        {
            get
            {
                var bounds = Utils.GetBoundingBox(Vertices);
                var height = bounds.Item4 - bounds.Item2;
                return height;
            }
        }

        public Rectangle(List<Point> vertices)
        {
            Vertices = vertices;
        }

        public Rectangle(Point p1, Point p2)
        {
            List<Point> pointList = new List<Point>() { p1, p2 };
            Vertices = pointList;
        }

        public Rectangle(Point p1, Point p2, Point p3, Point p4)
        {
            List<Point> pointList = new List<Point>() { p1, p2, p3, p4 };
            Vertices = pointList;
        }

        public override double Area()
        {
            return this.Width * this.Height;
        }

        public bool IsSquare()
        {
            if (this.Height == this.Width)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<Point> Normalize(List<Point> input)
        {
            var bounds = Utils.GetBoundingBox(input);

            Point p1 = new Point(bounds.Item1, bounds.Item2);
            Point p2 = new Point(bounds.Item1, bounds.Item4);
            Point p3 = new Point(bounds.Item3, bounds.Item4);
            Point p4 = new Point(bounds.Item3, bounds.Item2);

            List<Point> normalizedList = new List<Point>() { p1, p2, p3, p4 };

            return normalizedList;
        }

        public List<Triangle> ToTriangles()
        {
            Triangle t1 = new Triangle(this.Vertices[0], this.Vertices[1], this.Vertices[2]);
            Triangle t2 = new Triangle(this.Vertices[3], this.Vertices[2], this.Vertices[1]);
            List<Triangle> triangles = new List<Triangle>() { t1, t2 };
            return triangles;
        }

        public override string ToString()
        {
            return string.Format($"Rectangle: {base.ToString()}");
        }
    }
}
