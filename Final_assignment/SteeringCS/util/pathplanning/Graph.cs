using SteeringCS.util.pathplanning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util
{
    public class Graph
    {
        public static readonly double INFINITY = Double.MaxValue;
        public Dictionary<String, Vertex> vertexMap = new Dictionary<String, Vertex>();

        public List<Vertex> VisitedVertices { get; set; }

        public IHeuristic Heuristic { get; set; }

        public Graph()
        {
            Heuristic = new Euclidean();
            VisitedVertices = new List<Vertex>();
        }

        public void SetHeuristic(IHeuristic heuristic)
        {
            Heuristic = heuristic;
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

        public bool ContainsEdge(string source, string dest)
        {
            Vertex src = GetVertex(source);
            Vertex dst = GetVertex(dest);
            var sourceEdges = src.adj;

            bool containsEdge = false;

            // check if the destination exists within the list of edges from the source vertex
            if (sourceEdges.Any(e => e.Dest == dst))
                containsEdge = true;

            return containsEdge;
        }

        public void Reset()
        {
            vertexMap = new Dictionary<String, Vertex>();
            VisitedVertices = new List<Vertex>();
        }

        /// <summary>
        /// Reset the scratch, cost and visited properties of all vertices.
        /// </summary>
        private void ClearAll()
        {
            foreach (var item in vertexMap)
            {
                item.Value.Reset();
            }

            VisitedVertices.Clear();
        }

        /// <summary>
        /// Implementation of the A* search algorithm.
        /// </summary>
        /// <param name="startVertex"></param>
        /// <param name="goalVertex"></param>
        public void AStarSearch(string startVertex, string goalVertex)
        {
            var pq = new PriorityQueue<Path>();

            var start = vertexMap[startVertex];
            var goal = vertexMap[goalVertex];

            if (start == null || goal == null)
            {
                throw new GraphException("One or more vertex/vertices were notfound!");
            }

            // clear all existing dist/scratches
            ClearAll();

            pq.Enqueue(new Path(start, 0));
            start.dist = 0;

            var nodesSeen = 0;

            // while the priority queue has elements && seen nodes is less than total vertices count within the vertexMap
            while (pq.Count() > 0)
            {
                var vrec = pq.Dequeue();
                var v = vrec.Dest;

                // early exit if we find the goal => no need to keep on looking
                if (v == goal)
                    break;

                VisitedVertices.Add(v);

                if (v.scratch != 0)
                    continue;

                v.scratch = 1;
                nodesSeen++;

                foreach (var edge in v.adj)
                {
                    var w = edge.Dest;
                    double cvw = edge.Cost;

                    // calculate the heuristic value
                    var heuristicVal = Heuristic.Calculate(w, goal);

                    cvw += heuristicVal;


                    if (cvw < 0)
                    {
                        throw new GraphException("Graph has negative edges");
                    }

                    if (w.dist > v.dist + cvw)
                    {
                        w.dist = v.dist + cvw;
                        w.prev = v;
                        pq.Enqueue(new Path(w, w.dist));
                    }
                }
            }
        }

        /// <summary>
        /// Implementation of the Dijkstra algorithm.
        /// </summary>
        /// <param name="startName"></param>
        public void Dijkstra(string startName)
        {
            var pq = new PriorityQueue<Path>();

            var start = vertexMap[startName];
            if (start == null)
            {
                throw new Exception("Vertex not found!");
            }

            // clear all existing dist/scratches
            ClearAll();

            pq.Enqueue(new Path(start, 0));
            start.dist = 0;

            var nodesSeen = 0;

            // while the priority queue has elements && seen nodes is less than total vertices count within the vertexMap
            while (pq.Count() > 0 && nodesSeen < vertexMap.Count)
            {
                var vrec = pq.Dequeue();
                var v = vrec.Dest;

                VisitedVertices.Add(v);

                if (v.scratch != 0)
                    continue;

                v.scratch = 1;
                nodesSeen++;

                foreach (var edge in v.adj)
                {
                    var w = edge.Dest;
                    double cvw = edge.Cost;

                    if (cvw < 0)
                    {
                        throw new GraphException("Graph has negative edges");
                    }

                    if (w.dist > v.dist + cvw)
                    {
                        w.dist = v.dist + cvw;
                        w.prev = v;
                        pq.Enqueue(new Path(w, w.dist));
                    }
                }
            }
        }

        /// <summary>
        /// Implementation of the Breadth-first search algorithm.
        /// </summary>
        /// <param name="name"></param>
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

                        // de edge distance gelijkstellen aan de distance van de parent (vrtx) + 1
                        edgeDestination.dist = vrtx.dist + 1;

                        // het vorige element instellen (parent vertex) binnen de huidige edge
                        edgeDestination.prev = vrtx;

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

        /// <summary>
        /// Check if there are cycles within the graph.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Helper method which prints the path to a certain destination (vertex).
        /// </summary>
        /// <param name="dest">The destination as Vertex</param>
        private void PrintPath(Vertex dest)
        {
            if (dest.prev != null)
            {
                PrintPath(dest.prev);
                Console.WriteLine(" to ");
            }
            Console.WriteLine(dest.name);
        }

        /// <summary>
        /// Retrieve the path to a certain destination as a List of vectors.
        /// </summary>
        /// <param name="destName"></param>
        /// <returns></returns>
        public List<Vector2D> GetVectorPath(string destName)
        {
            Vertex dest = GetVertex(destName);
            List<Vector2D> path = new List<Vector2D>();
            while (dest.prev != null)
            {
                string[] split = dest.name.Split(';');
                var vector = new Vector2D(Int32.Parse(split[0]), Int32.Parse(split[1]));
                path.Add(vector);
                dest = dest.prev;
            }
            // reverse the path, because the original path is already reversed 
            // (back to front instead of front to back)
            path.Reverse();
            return path;
        }

        /// <summary>
        /// Retrieve the path to a certain destination as a List of vertices.
        /// </summary>
        /// <param name="destName"></param>
        /// <returns></returns>
        public List<Vertex> GetPath(string destName)
        {
            Vertex dest = GetVertex(destName);
            List<Vertex> path = new List<Vertex>();
            while (dest.prev != null)
            {
                path.Add(dest);
                dest = dest.prev;
            }
            return path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destName"></param>
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

    /// <summary>
    /// Small exception class.
    /// </summary>
    public class GraphException : SystemException
    {
        public GraphException(string name) : base(name)
        {

        }
    }
}
