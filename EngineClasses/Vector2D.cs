using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAMLEngine
{
    public struct Vector2D
    {
        public double x, y;

        public Vector2D()
        {
            this.x = 0;
            this.y = 0;
        }

        public Vector2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2D(Vector2D input)
        {
            this.x = input.x;
            this.y = input.y;
        }


        // Operator Overloads
        public static Vector2D operator +(Vector2D a) => a;
        public static Vector2D operator -(Vector2D a) => new Vector2D(-a.x, -a.y);

        public static Vector2D operator +(Vector2D a, Vector2D b) => new Vector2D(a.x + b.x, a.y + b.y);
        public static Vector2D operator -(Vector2D a, Vector2D b) => new Vector2D(a.x - b.x, a.y - b.y);
        public static Vector2D operator *(Vector2D a, Vector2D b) => new Vector2D(a.x * b.x, a.y * b.y);
        public static Vector2D operator /(Vector2D a, Vector2D b) => new Vector2D(a.x / b.x, a.y / b.y);

        public static Vector2D operator +(Vector2D a, double b) => new Vector2D(a.x + b, a.y + b);
        public static Vector2D operator -(Vector2D a, double b) => new Vector2D(a.x - b, a.y - b);
        public static Vector2D operator *(Vector2D a, double b) => new Vector2D(a.x * b, a.y * b);
        public static Vector2D operator /(Vector2D a, double b) => new Vector2D(a.x / b, a.y / b);

        public static Vector2D operator +(Vector2D a, float b) => new Vector2D(a.x + b, a.y + b);
        public static Vector2D operator -(Vector2D a, float b) => new Vector2D(a.x - b, a.y - b);
        public static Vector2D operator *(Vector2D a, float b) => new Vector2D(a.x * b, a.y * b);
        public static Vector2D operator /(Vector2D a, float b) => new Vector2D(a.x / b, a.y / b);

        public static Vector2D operator +(Vector2D a, int b) => new Vector2D(a.x + b, a.y + b);
        public static Vector2D operator -(Vector2D a, int b) => new Vector2D(a.x - b, a.y - b);
        public static Vector2D operator *(Vector2D a, int b) => new Vector2D(a.x * b, a.y * b);
        public static Vector2D operator /(Vector2D a, int b) => new Vector2D(a.x / b, a.y / b);

        public static bool operator ==(Vector2D a, Vector2D b) => Vector2D.Equals(a, b);
        public static bool operator !=(Vector2D a, Vector2D b) => !Vector2D.Equals(a, b);

        public override string ToString()
        {
            return $@"{x}, {y}";
        }
    }
}
