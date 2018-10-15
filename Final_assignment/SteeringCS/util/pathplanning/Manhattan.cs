using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.pathplanning
{
    /// <summary>
    /// Manhattan distance heuristic.
    /// </summary>
    public class Manhattan : IHeuristic
    {
        public double Calculate(Vertex a, Vertex b)
        {
            var splitA = a.name.Split(';');
            var splitB = b.name.Split(';');
            var pointA = new Point(Int32.Parse(splitA[0]), Int32.Parse(splitA[1]));
            var pointB = new Point(Int32.Parse(splitB[0]), Int32.Parse(splitB[1]));

            return Math.Abs(pointA.X - pointB.X) + Math.Abs(pointA.Y - pointB.Y);
        }
    }
}
