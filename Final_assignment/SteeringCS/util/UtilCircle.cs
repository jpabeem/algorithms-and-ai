using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util
{
    public class UtilCircle
    {
        public double Radius { get; set; }
        public Vector2D Pos { get; set; }

        public UtilCircle(int x, int y, double radius)
        {
            Pos = new Vector2D(x, y);
            Radius = radius;
        }

        public UtilCircle(Vector2D vector, double radius)
        {
            Pos = vector;
            Radius = radius;
        }

    }
}
