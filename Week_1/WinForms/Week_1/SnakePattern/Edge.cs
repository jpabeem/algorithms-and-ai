using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakePattern
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
    }
}
