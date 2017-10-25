using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    [SpecialClass(2)]
    class MathStuff
    {
        public static double GetCircumference(double radius)
        {
            return 2 * Math.PI * radius;
        }

        public static double CircleArea(double radius)
        {
            return Math.PI * Math.Pow(radius, 2);
        }

        public static double RectangleArea(double length, double width)
        {
            return length * width;
        }

        public static double RectanglePerimiter(double length, double width)
        {
            return (length * 2) + (width * 2);
        }
    }
}
