using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    public class DefaultBehaviour : SteeringBehaviour
    {
        public DefaultBehaviour(MovingEntity me) : base(me) { }

        public override Vector2D Calculate()
        {
            ME.MaxSpeed = 0;
            return new Vector2D(0, 0);
        }
    }
}
