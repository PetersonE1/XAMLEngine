using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAMLEngine.EngineClasses
{
    public static class MathD
    {        
        public static double Repeat(double min, double max, double value)
        {
            while (value < min)
            {
                value += (max - min) + 1;
            }
            while (value > max)
            {
                value -= (max - min) + 1;
            }
            return value;
        }

        public static double Lerp(double x, double y, double percent)
        {
            percent = Repeat(0, 1, percent);
            return ((y - x) * percent) + x;
        }

        public static double Distance(Vector2D point1, Vector2D point2)
        {
            return Math.Sqrt(Math.Pow(point2.x - point1.x, 2) + Math.Pow(point2.y - point1.y, 2));
        }

        public static Vector2D VLerp(Vector2D point1, Vector2D point2, double percent)
        {
            percent = Repeat(0, 1, percent);
            double x = Lerp(point1.x, point2.x, percent);
            double y = Lerp(point1.y, point2.y, percent);
            return new Vector2D(x, y);
        }

        public static double RLerp(double x, double y, double value)
        {
            return (value - x) / (y - x);
        }
    }
}
