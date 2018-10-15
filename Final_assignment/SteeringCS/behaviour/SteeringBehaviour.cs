using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS
{
    public abstract class SteeringBehaviour
    {
        public MovingEntity ME { get; set; }
        public abstract Vector2D Calculate();

        public SteeringBehaviour(MovingEntity me)
        {
            ME = me;
        }

        protected void SetAngle(Vector2D vector, double value)
        {
            var length = vector.Length();
            vector.X = Math.Cos(value) * length;
            vector.Y = Math.Sin(value) * length;
        }
    }
}
