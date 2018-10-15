using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util
{
    public class Path : IComparable<Path>
    {
        public Vertex Dest;
        public double Cost;

        public Path(Vertex dest, double cost)
        {
            Dest = dest;
            Cost = cost;
        }

        public int CompareTo(Path other)
        {
            //Returns -1 if cost < other.cost, returns 1 if cost > other.cost, else (if equal) returns 0
            return Cost < other.Cost ? -1 : Cost > other.Cost ? 1 : 0;
        }
    }
}
