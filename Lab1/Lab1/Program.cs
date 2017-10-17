using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static PointList points = new PointList();
        static void Main(string[] args)
        {
            points.Changed += delegate (object sender, PointListChangedEventArgs e)
            {
                Console.WriteLine($"Points Changed: {e.Operation}");
            };

            points.Add(new Point(-4, -7));
            points.Add(new Point(0, 0));
            points.Add(new Point(1, 2));
            points.Add(new Point(-4, 5));
            points.Insert(2, new Point(3, 1));
            points.Add(new Point(7, -2));
            points[0] = new Point(2, 1);
            points.RemoveAt(2);

            //points on origin
            Func<Point, bool> originPointDel = (p) => p.X == 0 && p.Y == 0;

            Console.WriteLine($"Any points on origin: {points.Any(originPointDel)}");

            //points in 1st quadrant
            Func<Point, bool> firstQuadDel = (p) => p.X < 0 && p.Y > 0;

            var firstQuadPoints = points.Where(firstQuadDel);

            foreach (var point in firstQuadPoints)
            {
                Console.WriteLine($"First Quadrant Point: {point}");
            }

            //all points positive
            Func<Point, bool> allPositiveDel = (p) => p.X > 0 && p.Y > 0;

            Console.WriteLine($"All points positive: {points.All(allPositiveDel)}");

            Console.WriteLine("Press <ENTER> to quit...");
            Console.ReadLine();
        }
    }
}
