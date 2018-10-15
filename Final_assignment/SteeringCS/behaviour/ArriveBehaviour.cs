using SteeringCS.entity;
using SteeringCS.util.sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    public enum Deceleration
    {
        SLOW = 3,
        NORMAL = 2,
        FAST = 1
    }

    // Seek + Arrive behaviours
    // TODO: refactor to Arrive behaviour?
    class ArriveBehaviour : SteeringBehaviour
    {
        const double DecelerationTweaker = 0.6;
        const int MAX_SPEED = 5;
        public ArriveBehaviour(MovingEntity me) : base(me)
        {
            if (ME.SpriteStrategy.GetType() != typeof(RedFishSprite))
            {
                ME.SpriteStrategy = new RedFishSprite();
            }
            me.MaxSpeed = MAX_SPEED;
        }

        private Vector2D Arrive(Deceleration deceleration)
        {
            ME.MaxSpeed = MAX_SPEED;
            var position = ME.Pos.Clone();
            Vector2D target;

            if (ME.Target == null)
            {
                target = ME.MyWorld.Target.Pos.Clone();
            }
            else
            {
                target = ME.Target.Clone();
            }

            //var target = ME.MyWorld.Target.Pos.Clone();

            var toTarget = target.Sub(ME.Pos);

            var distance = toTarget.Length();

            if (distance < 0.05)
                ME.MaxSpeed = 0;
            else if (distance > 1)
            {
                var speed = distance / ((double)deceleration * DecelerationTweaker);

                speed = Math.Min(speed, ME.MaxSpeed);

                var desiredVelocity = toTarget.Multiply(speed / distance);

                return (desiredVelocity.Sub(ME.Velocity));
            }
          
            return new Vector2D();
            //var desired = target.Sub(position);
            //desired.Normalize();
            ////desired.Multiply(ME.MaxSpeed);

            //var distance = desired.Length();

            //if (distance <= ME.SlowingRadius)
            //    desired.Multiply(ME.MaxSpeed * (distance / ME.SlowingRadius));
            //else
            //    desired.Multiply(ME.MaxSpeed);

            //var force = desired.Sub(ME.Velocity);


            //return force;
        }

        public override Vector2D Calculate()
        {
            //return new Vector2D();
            return Arrive(Deceleration.FAST);
        }
    }
}
