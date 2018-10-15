using SteeringCS.entity;
using SteeringCS.util;
using SteeringCS.util.sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SteeringCS.behaviour
{
    public class ExploreBehaviour : SteeringBehaviour
    {
        public ObstacleType CurrentType { get; set; }

        // exploring the game world to find a target entity
        public BaseGameEntity TargetEntity { get; set; }

        public ExploreBehaviour(MovingEntity me, BaseGameEntity entity = null) : base(me)
        {
            me.MaxSpeed = 5;
            me.ExploreRadius = 175;

            TargetEntity = entity;

            if (ME.SpriteStrategy.GetType() != typeof(RedFishSprite))
            {
                ME.SpriteStrategy = new RedFishSprite();
            }
        }

        /// <summary>
        /// Check the 'Radar', which is a circle around the entity with a given Explore radius.
        /// </summary>
        private void CheckRadar()
        {
            UtilCircle circle = new UtilCircle(ME.Pos, ME.ExploreRadius);

            if (TargetEntity == null)
                return;

            // calculate the distance between the current position and obstacle
            var distance = EntityHelper.Distance(ME.Pos, TargetEntity.Pos);

            // check if there is collision 
            if (distance <= ME.ExploreRadius + TargetEntity.Scale)
            {
                ME.TargetFound = true;
            }
            else
            {
                ME.TargetFound = false;
            }

        }

        public override Vector2D Calculate()
        {
            CheckRadar();

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
