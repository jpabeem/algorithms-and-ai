using SteeringCS.entity;
using SteeringCS.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class FleeBehaviour : SteeringBehaviour
    {
        public FleeBehaviour(MovingEntity me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            var panicDistance = 350.0;
            var position = ME.Pos.Clone();
            var target = ME.MyWorld.Target.Pos.Clone();

            // check if we're in panic distance
            var targetDistance = EntityHelper.Distance(position, target);

            Vector2D desiredVelocity = position.Sub(target);
            desiredVelocity.Normalize();
            desiredVelocity.Multiply(ME.MaxSpeed);

            if (targetDistance > panicDistance)
            {
                ME.Velocity.Multiply((ME.SlowingRadius / panicDistance));
                ME.Velocity = new Vector2D(0, 0);
                return new Vector2D(0, 0);
            }

            return desiredVelocity - ME.Velocity;
        }
    }
}
