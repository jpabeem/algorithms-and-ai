using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Week_1
{
    public class Graph
    {
        public static readonly double INFINITY = Double.MaxValue;
        private Dictionary<String, Vertex> vertexMap = new Dictionary<String, Vertex>();

        public List<Vertex> verticesPath { get; private set; }

        public Graph() { }

        public Graph(List<Vertex> vertices, IEnumerable<Tuple<int, int>> edges, bool unweighted = false)
        {
            verticesPath = new List<Vertex>();
            foreach (var vertex in vertices)
            {
                AddVertex(vertex.name);
            }

            foreach (var edge in edges)
            {
                AddEdge(edge.Item1.ToString(), edge.Item2.ToString(), 0);
                if (unweighted)
                    AddEdge(edge.Item2.ToString(), edge.Item1.ToString(), 0);
            }
        }

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

        public HashSet<Vertex> SolveBreadthFirst(string startVertex, Action<Vertex> preVisit = null)
        {
            var visited = new HashSet<Vertex>();

            ClearAll();
            Console.WriteLine("Starting depth first search for {0}", startVertex);

            Vertex start = vertexMap[startVertex];
            if (start == null)
            {
                throw new NotSupportedException("Start vertex not found");
            }

            var queue = new Queue<Vertex>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();

                if (visited.Contains(vertex))
                    continue;

                if (preVisit != null)
                    preVisit(vertex);

                visited.Add(vertex);

                foreach(var edge in vertex.adj)
                {
                    Vertex edgeDestination = edge.Dest;

                    if (!visited.Contains(edgeDestination))
                        queue.Enqueue(edgeDestination);
                }

            }

            return visited;
        }

        public HashSet<Vertex> SolveDepthFirst(string startVertex)
        {
            var visited = new HashSet<Vertex>();

            ClearAll();
            Console.WriteLine("Starting depth first search for {0}", startVertex);

            Vertex start = vertexMap[startVertex];
            if (start == null)
            {
                throw new NotSupportedException("Start vertex not found");
            }

            var stack = new Stack<Vertex>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                var vertex = stack.Pop();

                if (visited.Contains(vertex))
                    continue;

                visited.Add(vertex);

                // foreach edge in de lijst van adjacents van de ge-dequeuede vertex
                foreach (Edge e in vertex.adj)
                {
                    Vertex edgeDestination = e.Dest;

                    if (!visited.Contains(edgeDestination))
                        stack.Push(edgeDestination);
                }

            }

            return visited;
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

        public void GetPath(string destName)
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
                GetPath(w);
                Console.WriteLine(" ");
            }
        }

        public void GetPath(Vertex dest)
        {
            if (dest.prev != null)
            {
                GetPath(dest.prev);
            }

            verticesPath.Add(dest);
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
