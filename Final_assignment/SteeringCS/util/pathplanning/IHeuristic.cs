using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util
{
    public interface IHeuristic
    {
        /// <summary>
        /// Calculate the heuristic value between two vertices.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        double Calculate(Vertex a, Vertex b);
    }
}
