using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    [SpecialClass(6)]
    static class Utils
    {
        public static bool IsRelativelyEqual(double d1, double d2)
        {
            var marginOfError = Math.Abs(((d1 + d2) / 2) * .0001);
            var diff = Math.Abs(d1 - d2);

            if (diff < marginOfError)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static Tuple<double, double, double, double> GetBoundingBox(List<Point> points)
        {
            double minX = 0, minY = 0, maxX = 0, maxY = 0;

            List<double> xValues = new List<double>();
            List<double> yValues = new List<double>();

            foreach (var point in points)
            {
                xValues.Add(point.X);
                yValues.Add(point.Y);
            }

            minX = xValues.Min();
            minY = yValues.Min();
            maxX = xValues.Max();
            maxY = yValues.Max();

            return new Tuple<double, double, double, double>(minX, minY, maxX, maxY);
        }
    }
}
