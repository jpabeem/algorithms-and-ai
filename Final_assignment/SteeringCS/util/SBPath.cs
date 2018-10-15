using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util
{
    public class SBPath
    {
        public List<Vector2D> Nodes { get; private set; }
        public float Radius { get; private set; }
        public int ForwardModifier { get; private set; }

        public SBPath(float radius, List<Vector2D> nodes = null)
        {
            Radius = radius;
            ForwardModifier = 3;
            if (nodes != null)
                Nodes = nodes;
            else
                Nodes = new List<Vector2D>();
        }

        /// <summary>
        /// Return the next waypoint on the line;
        /// </summary>
        /// <returns></returns>
        public Vector2D GetCurrentWaypoint(Vector2D Pos)
        {
            double smallestDistance = Double.MaxValue;
            int smallestIndex = 0;

            for (int waypoint = 0; waypoint < Nodes.Count; waypoint++)
            {
                var distance = EntityHelper.Distance(GetWaypointAtIndex(waypoint), Pos);
                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    smallestIndex = waypoint;
                }
            }

            // return the next waypoint or return the final waypoint
            return smallestIndex < Nodes.Count - ForwardModifier ? GetWaypointAtIndex(smallestIndex + ForwardModifier) : FinalWaypoint();
        }

        /// <summary>
        /// Helper method which retrieves a waypoint (Vector2D) at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private Vector2D GetWaypointAtIndex(int index)
        {
            if (index > Nodes.Count)
                throw new ArgumentOutOfRangeException("Specified index is out of range");
            return Nodes[index];
        }

        public bool Finished(Vector2D position)
        {
            return EntityHelper.Distance(FinalWaypoint(), position) < 15;
        }

        public Vector2D FinalWaypoint()
        {
            return Nodes[Nodes.Count - 1];
        }

        public void AddNode(Vector2D node)
        {
            Nodes.Add(node);
        }
    }
}
