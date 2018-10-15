using SteeringCS.util;
using SteeringCS.util.spatial_partitioning;
using SteeringCS.util.sprites;
using System;
using System.Drawing;

namespace SteeringCS
{
    public abstract class BaseGameEntity
    {
        public Vector2D Pos { get; set; }
        public float Scale { get; set; }
        public World MyWorld { get; set; }
        public Bucket Bucket { get; set; }

        public ISpriteMode SpriteStrategy { get; set; }

        public BaseGameEntity(Vector2D pos, World w)
        {
            Pos = pos;
            MyWorld = w;
            Bucket = null;
        }

        public double Left
        {
            get
            {
                return Pos.X - (Scale / 2);
            }
        }

        public double Right
        {
            get
            {
                return Pos.X + (Scale / 2);
            }
        }

        public double Top
        {
            get
            {
                return Pos.Y + (Scale / 2);
            }
        }

        public double Bottom
        {
            get
            {
                return Pos.Y - (Scale / 2);
            }
        }

        public abstract void Update(float delta);

        /// <summary>
        /// Calculates the distance from another Entity.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="useHitboxes"></param>
        /// <returns></returns>
        public virtual double DistanceFrom(BaseGameEntity e, bool useHitboxes = false)
        {
            return EntityHelper.Distance(Pos, e.Pos);
        }

        public virtual void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Blue, new Rectangle((int)Pos.X, (int)Pos.Y, 10, 10));
        }

    }
}
