using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util
{
    public class EntityHelper
    {
        public static double DistanceTolerance = 1.5;

        /// <summary>
        /// Check if a rectangle and a circular obstacle have collision.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="obstacle"></param>
        /// <returns></returns>
        public static bool Intersects(Rectangle rect, BaseGameEntity obstacle)
        {
            double rectHalfWidth = rect.Width/ 2;
            double rectHalfHeight = rect.Height / 2;

            double cx = Math.Abs(obstacle.Pos.X - rect.X - rectHalfWidth);
            double xDist = rectHalfWidth + obstacle.Scale;

            if (cx > xDist)
                return false;

            double cy = Math.Abs(obstacle.Pos.Y - rect.Y - (rect.Width / 2));
            double yDist = rectHalfHeight + obstacle.Scale;

            if (cy > yDist)
                return false;

            if (cx <= rectHalfWidth || cy <= rectHalfHeight)
                return true;

            double xCornerDist = cx - rectHalfWidth;
            double yCornerDist = cy - rectHalfHeight;
            double xCornerDistSq = xCornerDist * xCornerDist;
            double yCornerDistSq = yCornerDist * yCornerDist;
            double maxCornerDistSq = Math.Pow(obstacle.Scale, 2);

            return xCornerDistSq + yCornerDistSq <= maxCornerDistSq;
        }

        /// <summary>
        /// Returns a boolean if a given moving entity is at a given target position.
        /// </summary>
        /// <param name="me"></param>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        public static bool IsAtPosition(MovingEntity me, Vector2D targetPosition, double tolerance = 0)
        {
            var calculation = Vector2DDistance(me.Pos, targetPosition);
            Double tolerancePOW;

            if (tolerance == 0)
                tolerancePOW = Math.Pow(DistanceTolerance, 2);
            else
                tolerancePOW = tolerance;

            return calculation < tolerancePOW;
        }

        /// <summary>
        /// Used in conjunction with CalculateTimeToReachPosition method.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private static double Vector2DDistance(Vector2D v1, Vector2D v2)
        {
            var ySeperation = v2.Y - v1.Y;
            var xSeperation = v2.X - v1.X;

            return Math.Sqrt(Math.Pow(ySeperation, 2) + Math.Pow(xSeperation, 2));
        }

        /// <summary>
        /// Calculate the amount of time it takes to reach a given position.
        /// </summary>
        /// <param name="me"></param>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        public static double CalculateTimeToReachPosition(MovingEntity me, Vector2D targetPosition)
        {
            // prevent division by zero
            var frameRate = me.MyWorld.FrameRate == 0 ? 30 : me.MyWorld.FrameRate;
            var vec2ddistance = Vector2DDistance(me.Pos, targetPosition);
            var calculation = (me.MaxSpeed * frameRate);
            return vec2ddistance / calculation;
        }

        /// <summary>
        /// Return the euclidean distance between vector 1 and vector 2.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static double Distance(Vector2D vector1, Vector2D vector2)
        {
            return Math.Sqrt(Math.Pow(vector1.X - vector2.X, 2) + Math.Pow(vector1.Y - vector2.Y, 2));
        }

        /// <summary>
        /// Return the closest vertex for a given point.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="vertexMap"></param>
        /// <returns></returns>
        public static Vertex GetClosestVertex(Point p, Dictionary<String, Vertex> vertexMap)
        {
            Vertex closestVertexToPoint = null;
            double distance = double.PositiveInfinity;
            foreach (var keyValuePair in vertexMap)
            {
                Vertex vertex = keyValuePair.Value;
                string[] split = vertex.name.Split(';');

                int x = Int32.Parse(split[0]);
                int y = Int32.Parse(split[1]);

                var dist = EntityHelper.Distance(new Vector2D(p), new Vector2D(x, y));

                if (dist < distance)
                {
                    distance = dist;
                    closestVertexToPoint = vertex;
                }
            }

            return closestVertexToPoint;
        }
    }
}
