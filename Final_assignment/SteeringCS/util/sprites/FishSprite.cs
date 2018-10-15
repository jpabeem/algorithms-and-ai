using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.sprites
{
    public abstract class FishSprite : ISpriteMode
    {
        protected Image leftSprite, rightSprite, upSprite, downSprite;
        protected Point leftOffset, rightOffset, upOffset, downOffset;

        protected BaseGameEntity entity { get; set; }

        /// <summary>
        /// Initialize the sprites in a concrete class.
        /// </summary>
        protected abstract void InitSprites();

        /// <summary>
        /// Initialize the offsets in a concrete class.
        /// </summary>
        protected abstract void InitOffsets();

        public void RenderSprite(Graphics g, BaseGameEntity e)
        {
            double scale = e.Scale;
            double leftCorner = e.Pos.X - scale;
            double rightCorner = e.Pos.Y - scale;
            float size = (float)scale * 2;
            var MyWorld = e.MyWorld;

            Brush brush;

            if (!(e is MovingEntity))
                throw new ArgumentException("Cannot render sprite for current entity type.");

            MovingEntity moving = (MovingEntity)e;

            if (e is Vehicle entity)
                brush = new SolidBrush(entity.VColor);
            else
                // fallback for moving entities other than vehicle
                brush = new SolidBrush(Color.Black);

           
            // draw the sprite for the current direction of the entity
            switch (moving.Direction)
            {
                case EntityDirection.RIGHT:
                    g.DrawImage(rightSprite, (int)(e.Pos.X - rightOffset.X), (int)(e.Pos.Y - rightOffset.Y));
                    //g.DrawImage(rightSprite, (int)(e.Pos.X - (scale + scale)), (int)(e.Pos.Y - (scale + scale + scale - 5)));
                    break;
                case EntityDirection.LEFT:
                    g.DrawImage(leftSprite, (int)(e.Pos.X - leftOffset.X), (int)(e.Pos.Y - leftOffset.Y));
                    //g.DrawImage(leftSprite, (int)(e.Pos.X - (scale + scale)), (int)(e.Pos.Y - (scale + scale + scale + -5)));
                    break;
                case EntityDirection.UP:
                    if (upSprite == null)
                        throw new ArgumentNullException("Trying to draw a non-existing sprite.");
                    else
                        g.DrawImage(upSprite, (int)(e.Pos.X - upOffset.X), (int)(e.Pos.Y - upOffset.Y));
                    break;
                case EntityDirection.DOWN:
                    if (downSprite == null)
                        throw new ArgumentNullException("Trying to draw a non-existing sprite.");
                    else
                        g.DrawImage(downSprite, (int)(e.Pos.X - downOffset.X), (int)(e.Pos.Y - downOffset.Y));
                    break;
            }

            // draw bounding box (ellipse) around entity
            if (e.MyWorld.Settings.Get("ToggleObstacleBoundingBox"))
                g.FillEllipse(brush, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));

        }
    }
}
