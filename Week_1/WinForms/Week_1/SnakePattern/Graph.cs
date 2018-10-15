using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakePattern
{
    public class Graph
    {
        public static readonly double INFINITY = Double.MaxValue;
        public Dictionary<String, Vertex> vertexMap = new Dictionary<String, Vertex>();


        public void AddEdge(string source, string dest, double cost)
        {
            Vertex v = GetVertex(source);
            Vertex w = GetVertex(dest);
            v.adj.AddLast(new Edge(w, cost));

        }

        public void AddVertex(string name)
        {
            GetVertex(name);
        }

        private void ClearAll()
        {
            foreach (var item in vertexMap)
            {
                item.Value.Reset();
            }
        }

        public void Unweighted(string name)
        {
            ClearAll();
            Console.WriteLine("Starting unweighted search for {0}", name);

            Vertex start = vertexMap[name];
            if (start == null)
            {
                throw new NotSupportedException("Start vertex not found");
            }


            Queue<Vertex> q = new Queue<Vertex>();
            // enqueue het startelement
            q.Enqueue(start);

            // zet de distance van het startelement op 0
            start.dist = 0;

            // zolang de queue count > 0
            while (q.Count > 0)
            {

                // dequeue element dat vooraan staat (want FIFO bij queue)
                Vertex vrtx = q.Dequeue();

                // foreach edge in de lijst van adjacents van de ge-dequeuede vertex
                foreach (Edge e in vrtx.adj)
                {
                    // bestemming van de edge binnnen de foreach van de lijst van adjacents
                    Vertex edgeDestination = e.Dest;

                    // als de distance van de edge gelijkstaat aan oneindig
                    if (edgeDestination.dist == Graph.INFINITY)
                    {
                        //Console.WriteLine("Afstand van Edge destination {0} is infinity", edgeDestination.name);

                        // de edge distance gelijkstellen aan de distance van de parent (vrtx) + 1
                        edgeDestination.dist = vrtx.dist + 1;

                        // het vorige element instellen (parent vertex) binnen de huidige edge
                        edgeDestination.prev = vrtx;
                        //Console.WriteLine("Enqueuing Edge destination: {0}", edgeDestination.name);

                        // enqueue de edge destination
                        q.Enqueue(edgeDestination);
                    }
                    else
                    {
                        //Console.WriteLine("Afstand van {0} tot {1}: {2}", name, edgeDestination.name, edgeDestination.dist);
                    }
                }
            }
        }



        public bool IsConnected()
        {
            Dictionary<String, Vertex> vertexMapCopy = new Dictionary<String, Vertex>(vertexMap);


            foreach (KeyValuePair<String, Vertex> vertex in vertexMap)
            {
                foreach (Edge adjEdge in vertex.Value.adj)
                {
                    Vertex destination = adjEdge.Dest;
                    if (vertexMapCopy.ContainsValue(destination))
                    {
                        vertexMapCopy.Remove(destination.name);
                    }
                }
            }

            return vertexMapCopy.Count == 0;
        }

        public void PrintPath(Vertex dest)
        {
            if (dest.prev != null)
            {
                PrintPath(dest.prev);
                Console.WriteLine(" to ");
            }
            Console.WriteLine(dest.name);
        }

        public void PrintPath(string destName)
        {
            Vertex w = GetVertex(destName);

            if (w == null)
            {
                throw new NotSupportedException("Start vertex not found");
            }
            else if (w.dist.Equals(Graph.INFINITY))
            {
                Console.WriteLine(destName + " is unreachable");
            }
            else
            {
                Console.WriteLine("(Cost is: " + w.dist + ") ");
                PrintPath(w);
                Console.WriteLine(" ");
            }
        }

        public Vertex GetVertex(string name)
        {
            Vertex v;
            if (!vertexMap.ContainsKey(name))
            {
                v = new Vertex(name);
                vertexMap[name] = v;
            }
            else
            {
                v = vertexMap[name];
            }
            return v;
        }

        public override string ToString()
        {
            string output = "";
            foreach (var item in vertexMap)
            {
                //if(item.Value.adj.Count > 0){
                output += item.ToString() + "\n";
                //}
            }
            return output;
        }
    }
}
