using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.behaviour
{
    public class PathFollowingBehaviour : SteeringBehaviour
    {
        private Vector2D FinalPosition { get; set; }

        public PathFollowingBehaviour(MovingEntity me) : base(me)
        {
            ME.MaxSpeed = 3;
            FinalPosition = null;
        }

        private Vector2D Seek(Vector2D target)
        {
            var position = ME.Pos.Clone();

            Vector2D force = new Vector2D();

            Vector2D worldTarget = ME.MyWorld.Target.Pos.Clone();

            Vector2D desired = worldTarget.Sub(position);

            desired.Normalize();
            desired.Multiply(ME.MaxSpeed);

            force = desired.Sub(ME.Velocity);

            return force;
        }

        private Vector2D PathFollowing()
        {
            var MyWorld = ME.MyWorld;
            Vector2D Pos = ME.Pos;

            if (MyWorld.SteeringPath == null)
                return new Vector2D();

            FinalPosition = MyWorld.SteeringPath.FinalWaypoint();

            if (!MyWorld.SteeringPath.Finished(Pos))
            {
                Vector2D currentWaypoint = MyWorld.SteeringPath.GetCurrentWaypoint(Pos);
                ME.MyWorld.CurrentTargetPathFollowing = currentWaypoint.Clone();

                ME.MyWorld.Target.Pos = currentWaypoint.Clone();

                return Seek(currentWaypoint);
            }
            else
            {
                // arrived at destination
                FinalPosition = null;
                return Seek(MyWorld.SteeringPath.GetCurrentWaypoint(Pos));
            }
        }


        public override Vector2D Calculate()
        {
            return PathFollowing();
        }
    }
}
