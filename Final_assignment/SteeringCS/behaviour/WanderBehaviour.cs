using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class WanderBehaviour : SteeringBehaviour
    {
        public WanderBehaviour(MovingEntity me) : base(me)
        {
            me.MaxSpeed = 4f;
        }

        public override Vector2D Calculate()
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
    }
}
