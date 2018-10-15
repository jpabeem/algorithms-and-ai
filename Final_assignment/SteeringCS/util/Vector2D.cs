using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS
{
   
    public class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2D() : this(0,0)
        {
        }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector2D(Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        public double Length()
        {
            return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
        }

        public double LengthSquared()
        {
            return Math.Pow(Length(), 2);
        }

        public static Vector2D operator+(Vector2D v1, Vector2D v2)
        {
            return v1.Add(v2);
        }

        public Vector2D Add(Vector2D v)
        {
            this.X += v.X;
            this.Y += v.Y;
            return this;
        }

        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            return v1.Sub(v2);
        }

        public Vector2D Sub(Vector2D v)
        {
            this.X -= v.X;
            this.Y -= v.Y;
            return this;
        }

        public static Vector2D operator *(Vector2D v1, double value)
        {
            return v1.Multiply(value);
        }

        public Vector2D Multiply(double value)
        {
            this.X *= value;
            this.Y *= value;
            return this;
        }

        public Vector2D Divide(double value)
        {
            this.X /= value;
            this.Y /= value;
            return this;
        }

        public Vector2D Normalize()
        {
            if (Length() > 1)
            {
                return Divide(Length());
            }
            return new Vector2D();
        }

        public Vector2D Normalize(double scale)
        {
            double normalizedVectorLength = Length();
            if (normalizedVectorLength != 0)
            { // as3 return 0,0 for a point of zero length
                this.Divide(normalizedVectorLength);
                this.Multiply(scale);
            }
            return this;
        }

        public Vector2D Truncate(double maX)
        {
            if (Length() > maX)
            {
                Normalize();
                Multiply(maX);
            }
            return this;
        }
        
        public Vector2D Clone()
        {
            return new Vector2D(this.X, this.Y);
        }
        
        public override string ToString()
        {
            return String.Format("(X: {0};Y: {1})", TruncatePositionValue(X), TruncatePositionValue(Y));
        }

        private double TruncatePositionValue(double val)
        {
            return Math.Truncate(val * 100) / 100;
        }
    }
}
