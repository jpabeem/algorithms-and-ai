using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util
{
    public class Edge
    {
        public Vertex Dest { get; set; }
        public double Cost { get; set; }

        public Edge(Vertex destination, double cost)
        {
            this.Dest = destination;
            this.Cost = cost;
        }

        public Vector2D GetDestination()
        {
            var exploded = Dest.name.Split(';');
            return new Vector2D(double.Parse(exploded[0]), double.Parse(exploded[2]));
        }
    }
}
