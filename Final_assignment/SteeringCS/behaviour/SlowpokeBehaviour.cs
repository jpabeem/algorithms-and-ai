using SteeringCS.entity;
using SteeringCS.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    /// <summary>
    /// Modified FleeBehaviour: if am enemy is closeby it tries to flee away, if not 
    /// it will slowly wander around.
    /// </summary>
    public class SlowpokeBehaviour : SteeringBehaviour
    {
        public SlowpokeBehaviour(MovingEntity me) : base(me)
        {
            me.MaxSpeed = 5f;
        }

        private Vector2D Wander()
        {
            var circleCenter = ME.Velocity.Clone();
            circleCenter.Normalize();

            circleCenter.Multiply(MovingEntity.CIRCLE_DISTANCE);

            var displacement = new Vector2D(0, -1);
            displacement.Multiply(MovingEntity.CIRCLE_DISTANCE);

            SetAngle(displacement, ME.WanderAngle);

            Random random = new Random();

            ME.WanderAngle += (float)(random.NextDouble() * MovingEntity.ANGLE_CHANGE - MovingEntity.ANGLE_CHANGE + 0.5f);

            var wanderForce = circleCenter.Add(displacement);

            return wanderForce;
        }

        public override Vector2D Calculate()
        {
            ME.MaxSpeed = 5f;
            var panicDistance = 350.0;
            var position = ME.Pos.Clone();
            var target = ME.MyWorld.Target.Pos.Clone();

            // check if we're in panic distance
            var targetDistance = EntityHelper.Distance(position, target);

            Vector2D desiredVelocity = position.Sub(target);
            desiredVelocity.Normalize();
            desiredVelocity.Multiply(ME.MaxSpeed);

            // not in panic distance, wander around slowly
            if (targetDistance > panicDistance)
            {
                ME.MaxSpeed = 1;
                //ME.Velocity.Multiply((ME.SlowingRadius / panicDistance));
                //ME.Velocity = new Vector2D(0, 0);
                return Wander();
            }

            return desiredVelocity - ME.Velocity;
        }
    }
}
