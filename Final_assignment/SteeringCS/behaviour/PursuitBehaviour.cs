using SteeringCS.entity;
using SteeringCS.util;
using SteeringCS.util.sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class PursuitBehaviour : SteeringBehaviour
    {
        public PursuitBehaviour(MovingEntity me) : base(me)
        {
            ME.MaxSpeed = 1.5f;

            if (ME.SpriteStrategy.GetType() != typeof(PursuitFishSprite))
            {
                ME.SpriteStrategy = new PursuitFishSprite();
            }
        }
        public ISpriteMode DefaultStrategy { get; set; }
        public Vector2D Target { get; set; }

        private Vector2D Evade(MovingEntity entity)
        {
            var distance = entity.Pos.Sub(ME.Pos);

            var updatesNeeded = distance.Length() / ME.MaxSpeed;

            var targetVector = entity.Velocity.Clone();
            targetVector.Multiply(updatesNeeded);

            var targetFuturePosition = entity.Pos.Clone().Add(targetVector);

            return Flee(targetFuturePosition);
        }

        private Vector2D Flee(Vector2D targetVector)
        {
            var panicDistance = 50.0;
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
                return new Vector2D(0, 0);
            }

            return desiredVelocity - ME.Velocity;
        }

        private Vector2D Seek(Vector2D targetVector)
        {
            Vector2D force = new Vector2D();
            var position = ME.Pos.Clone();
            Target = ME.MyWorld.GetVehicleForType(typeof(WanderBehaviour)).Pos.Clone();

            Vector2D desired = Target.Sub(position);
            desired.Normalize();
            desired.Multiply(ME.MaxSpeed);

            force = desired.Sub(ME.Velocity);
            return force;
        }

        public override Vector2D Calculate()
        {
            var position = ME.Pos.Clone();
            var fleeVehicle = ME.MyWorld.GetVehicleForType(typeof(WanderBehaviour));
            var Target = fleeVehicle.Pos.Clone();

            // cloned
            Vector2D distance = Target.Sub(position);
            // not cloned
            //Vector2D distance = ME.MyWorld.Target.Pos.Sub(ME.Pos);

            double updatesNeeded = distance.Length() / ME.MaxSpeed;

            Vector2D targetVelocity = fleeVehicle.Velocity.Clone();
            targetVelocity.Multiply(updatesNeeded);

            Target = ME.MyWorld.GetVehicleForType(typeof(WanderBehaviour)).Pos.Clone();

            Vector2D targetFuturePosition = Target.Add(targetVelocity);

            //// how far (in game ticks) we're allowed to check position of a target in the future
            //int time = 3;
            //var position = ME.Pos.Clone();
            //var target = ME.MyWorld.Target.Pos.Clone();

            //var desired = target.Sub(position);
            //desired.Normalize();
            ////desired.Multiply(ME.MaxSpeed);

            //var distance = desired.Length();

            //if (distance <= ME.SlowingRadius)
            //    desired.Multiply(ME.MaxSpeed * (distance / ME.SlowingRadius));
            //else
            //    desired.Multiply(ME.MaxSpeed);

            //var force = desired.Sub(ME.Velocity);


            return Seek(targetFuturePosition);
        }
    }
}
