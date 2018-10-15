using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1
{
    public class Vertex
    {
        public string name; // Vertex name
        public LinkedList<Edge> adj; //adjacent vertices
        public double dist; // cost
        public Vertex prev; // previous vertex on the shortest path
        public int scratch;

        public Vertex(string name)
        {
            this.name = name;
            this.adj = new LinkedList<Edge>();
            Reset();
        }

        public void Reset()
        {
            this.dist = Graph.INFINITY;
            this.prev = null;
            this.scratch = 0;
        }

        public override string ToString()
        {
            string adjacents = "";
            foreach (var edge in adj)
            {
                adjacents += edge.Dest.name + " (" + edge.Cost + ") ";
            }
            return name + " --> " + adjacents;
        }
    }
}
