using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.sprites
{
    class FleeFishSprite : ISpriteMode
    {
        private Image leftSprite, rightSprite, upSprite, downSprite;

        public FleeFishSprite() { InitSprites(); }

        private void InitSprites()
        {
            leftSprite = SteeringCS.Properties.Resources.fleeFishLeft;
            rightSprite = SteeringCS.Properties.Resources.fleeFishRight;
            upSprite = SteeringCS.Properties.Resources.fleeFishUp;
            downSprite = SteeringCS.Properties.Resources.fleeFishDown;
        }

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
                    g.DrawImage(rightSprite, (int)(e.Pos.X - (scale + scale)), (int)(e.Pos.Y - (scale+ scale)));
                    //g.DrawImage(rightSprite, (int)(e.Pos.X - (scale + scale)), (int)(e.Pos.Y - (scale + scale + scale - 5)));
                    break;
                case EntityDirection.LEFT:
                    g.DrawImage(leftSprite, (int)(e.Pos.X - (scale + scale/2)), (int)(e.Pos.Y - (scale + scale)));
                    //g.DrawImage(leftSprite, (int)(e.Pos.X - (scale + scale)), (int)(e.Pos.Y - (scale + scale + scale + -5)));
                    break;
                case EntityDirection.UP:
                    g.DrawImage(upSprite, (int)(e.Pos.X - (scale + scale - 5)), (int)(e.Pos.Y - (scale + scale - 5)));
                    //g.DrawImage(upSprite, (int)(e.Pos.X - (scale + scale + 10)), (int)(e.Pos.Y - (scale + scale + scale - scale)));
                    break;
                case EntityDirection.DOWN:
                    g.DrawImage(downSprite, (int)(e.Pos.X - (scale + scale - 5)), (int)(e.Pos.Y - (scale + scale)));
                    //g.DrawImage(downSprite, (int)(e.Pos.X - (scale + scale - 5)), (int)(e.Pos.Y - (scale + scale)));
                    break;
            }

            // draw bounding box (ellipse) around entity
            if (e.MyWorld.Settings.Get("ToggleObstacleBoundingBox"))
                g.FillEllipse(brush, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
        }
    }
}
