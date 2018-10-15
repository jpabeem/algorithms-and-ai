using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util
{
    public class FlockingHelper
    {
        private List<MovingEntity> Entities { get; set; }
        public Settings SettingsForm { get; set; }
        public float AlignmentWeight { get; set; }
        public float SeparationWeight { get; set; }
        public float CohesionWeight { get; set; }
        public float DistanceFrom { get; set; }

        public FlockingHelper(List<MovingEntity> entities)
        {
            this.Entities = entities;
            AlignmentWeight = 0.1f;
            SeparationWeight = 0.1f;
            CohesionWeight = 0.1f;
            DistanceFrom = 50f;
        }

        /// <summary>
        /// Compute the alignment part of the flocking behaviour.
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public Vector2D ComputeAlignment(MovingEntity me)
        {
            Vector2D v = new Vector2D();
            int neighborCount = 0;

            lock (Entities)
            {
                for (int i = 0; i < Entities.Count; i++)
                {
                    var agent = Entities[i];
                    if (agent != me)
                    {
                        if (me.DistanceFrom(agent) < DistanceFrom)
                        {
                            v.X += agent.Velocity.X;
                            v.Y += agent.Velocity.Y;
                            neighborCount++;
                        }

                    }
                }
            }

            if (neighborCount == 0)
                return v;

            v.Divide(neighborCount);
            v.Normalize();

            return v;
        }

        /// <summary>
        /// Compute the cohesion part of the flocking behaviour.
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public Vector2D ComputeCohesion(MovingEntity me)
        {
            Vector2D v = new Vector2D();
            int neighborCount = 0;

            for (int i = 0; i < Entities.Count; i++)
            {
                var agent = Entities[i];
                if (agent != me)
                {
                    if (me.DistanceFrom(agent) < DistanceFrom)
                    {
                        v.X += agent.Velocity.X;
                        v.Y += agent.Velocity.Y;
                        neighborCount++;
                    }

                }
            }
            if (neighborCount == 0)
                return v;

            v.X /= neighborCount;
            v.Y /= neighborCount;

            v = new Vector2D(v.X - me.Pos.X, v.Y - me.Pos.Y);

            v.Normalize();

            return v;
        }

        /// <summary>
        /// Compute the seperation part of the flocking behaviour.
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public Vector2D ComputeSeparation(MovingEntity me)
        {
            Vector2D v = new Vector2D();
            int neighborCount = 0;


            for (int i = 0; i < Entities.Count; i++)
            {
                var agent = Entities[i];
                if (agent != me)
                {
                    if (me.DistanceFrom(agent) < DistanceFrom)
                    {
                        v.X += agent.Velocity.X;
                        v.Y += agent.Velocity.Y;
                        neighborCount++;
                    }

                }
            }

            if (neighborCount == 0)
                return v;

            v.X /= neighborCount;
            v.Y /= neighborCount;
            v.X *= -1;
            v.Y *= -1;
            v.Normalize();

            return v;
        }

        /// <summary>
        /// Compute the WallAvoidance.
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public Vector2D ComputeWallAvoidance(MovingEntity me)
        {
            Vector2D v = new Vector2D();
            double avoidance = me.Scale + 45;

            if (me.Left < avoidance)
            {
                v.X = 10;
            }
            if (me.Right > me.MyWorld.Width - avoidance)
            {
                v.X = -10;
            }
            if (me.Top < avoidance)
            {
                v.Y = 10;
            }
            if (me.Bottom > me.MyWorld.Height - avoidance)
            {
                v.Y = -10;
            }
            return v;
        }

        public void EnforceNonPenetrationConstraint(MovingEntity me, List<MovingEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.Equals(me))
                    continue;

                var toEntity = entity.Pos.Clone() - me.Pos.Clone();

                var distFromEachOther = toEntity.Length();

                var overlap = me.Scale + entity.Scale - distFromEachOther + 10;

                /*  
                 *  If the distance is smaller than the sum of their radii,
                 *  then this entity must be moved away in the direction parallel 
                 *  to the toEntity vector.
                 */
                if (overlap >= 0)
                {
                    var newPos = entity.Pos.Clone() + (toEntity.Divide(distFromEachOther)) * overlap;
                    entity.Pos = newPos;
                }
            }
        }
    }
}
