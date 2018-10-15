using SteeringCS.entity;
using SteeringCS.util;
using SteeringCS.util.sprites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class SeekBehaviour : SteeringBehaviour
    {
        public SeekBehaviour(MovingEntity me) : base(me)
        {
            me.MaxSpeed = 10;
        }

        private Vector2D Seek()
        {
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

            var desired = target.Sub(position);

            desired.Normalize();

            var distance = desired.Length();

            if (distance <= ME.SlowingRadius)
            {
                desired.Multiply(ME.MaxSpeed * (distance / ME.SlowingRadius));
            }
            else
                desired.Multiply(ME.MaxSpeed);

            var force = desired.Sub(ME.Velocity);


            return force;
        }

        public override Vector2D Calculate()
        {
            return Seek();
        }
    }
}
