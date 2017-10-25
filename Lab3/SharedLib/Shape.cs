using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    [SpecialClass(4)]
    abstract class Shape
    {
        protected List<Point> _Vertices;
        public virtual List<Point> Vertices
        {
            get { return _Vertices; }
            set { _Vertices = value; }
        }

        public Shape()
        {

        }

        public Shape(List<Point> vertices)
        {
            Vertices = vertices;
        }

        public abstract double Area();

        public virtual double Perimeter()
        {
            double perimeter = 0;

            int length = Vertices.Count - 1;

            for (int i = 0; i <= length; i++)
            {
                if (i < length)
                {
                    var distance = Vertices[i].Distance(Vertices[i + 1]);
                    perimeter += distance;
                }
                else
                {
                    var distance = Vertices[i].Distance(Vertices[0]);
                    perimeter += distance;
                }
            }
            return perimeter;
        }

        public override string ToString()
        {
            string output = string.Empty;
            StringBuilder sb = new StringBuilder();

            int length = Vertices.Count - 1;

            for (int i = 0; i <= length; i++)
            {
                if (i < length)
                {
                    sb.Append($"({Vertices[i].X},{Vertices[i].Y}),");
                }
                else
                {
                    sb.Append($"({Vertices[i].X},{Vertices[i].Y})");
                }
            }
            return sb.ToString();
        }
    }
}
