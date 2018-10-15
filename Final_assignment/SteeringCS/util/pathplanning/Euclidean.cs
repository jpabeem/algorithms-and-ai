using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.pathplanning
{
    /// <summary>
    /// Euclidean distance heuristic.
    /// </summary>
    public class Euclidean : IHeuristic
    {
        public double Calculate(Vertex a, Vertex b)
        {
            var splitA = a.name.Split(';');
            var splitB = b.name.Split(';');
            var pointA = new Point(Int32.Parse(splitA[0]), Int32.Parse(splitA[1]));
            var pointB = new Point(Int32.Parse(splitB[0]), Int32.Parse(splitB[1]));

            return EntityHelper.Distance(new Vector2D(pointA), new Vector2D(pointB));
        }
    }
}
