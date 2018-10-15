using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.sprites
{
    public class PursuitFishSprite : FishSprite, ISpriteMode
    {
        protected override void InitSprites()
        {
            leftSprite = SteeringCS.Properties.Resources.pursuitFishLeft;
            rightSprite = SteeringCS.Properties.Resources.pursuitFishRight;
            upSprite = SteeringCS.Properties.Resources.pursuitFishUp;
            downSprite = SteeringCS.Properties.Resources.pursuitFishDown;
        }

        protected override void InitOffsets()
        {
            leftOffset = new Point((int)(entity.Scale + entity.Scale), (int)(entity.Scale + entity.Scale));
            rightOffset = new Point((int)(entity.Scale + entity.Scale + 5), (int)(entity.Scale + entity.Scale));
            upOffset = new Point((int)entity.Scale, (int)entity.Scale);
            downOffset = new Point((int)entity.Scale, (int)entity.Scale);

            // case EntityDirection.RIGHT:
            //        g.DrawImage(rightSprite, (int)(e.Pos.X - (scale + scale)), (int)(e.Pos.Y - (scale + scale)));
            ////g.DrawImage(rightSprite, (int)(e.Pos.X - (scale + scale)), (int)(e.Pos.Y - (scale + scale + scale - 5)));
            //break;
            //    case EntityDirection.LEFT:
            //        g.DrawImage(leftSprite, (int)(e.Pos.X - (scale + scale / 2 + 5)), (int)(e.Pos.Y - (scale + scale)));
            ////g.DrawImage(leftSprite, (int)(e.Pos.X - (scale + scale)), (int)(e.Pos.Y - (scale + scale + scale + -5)));
            //break;
            //    case EntityDirection.UP:
            //        g.DrawImage(upSprite, (int)(e.Pos.X - (scale + scale - 5)), (int)(e.Pos.Y - (scale + scale - 5)));
            ////g.DrawImage(upSprite, (int)(e.Pos.X - (scale + scale + 10)), (int)(e.Pos.Y - (scale + scale + scale - scale)));
            //break;
            //    case EntityDirection.DOWN:
            //        g.DrawImage(downSprite, (int)(e.Pos.X - (scale + scale - 5)), (int)(e.Pos.Y - (scale + scale)));
            ////g.DrawImage(downSprite, (int)(e.Pos.X - (scale + scale - 5)), (int)(e.Pos.Y - (scale + scale)));
            //break;
        }

        public new void RenderSprite(Graphics g, BaseGameEntity e)
        {
            entity = e;
            InitSprites();
            InitOffsets();
            base.RenderSprite(g, e);
            //double scale = e.Scale;
            //double leftCorner = e.Pos.X - scale;
            //double rightCorner = e.Pos.Y - scale;
            //float size = (float)scale * 2;
            //var MyWorld = e.MyWorld;

            //Brush brush;

            //if (!(e is MovingEntity))
            //    throw new ArgumentException("Cannot render sprite for current entity type.");

            //MovingEntity moving = (MovingEntity)e;

            //if (e is Vehicle entity)
            //    brush = new SolidBrush(entity.VColor);
            //else
            //    // fallback for moving entities other than vehicle
            //    brush = new SolidBrush(Color.Black);

            //// draw elipse
            //g.FillEllipse(brush, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));

            // draw the sprite for the current direction of the entity
            //switch (moving.Direction)
            //{
            //    case EntityDirection.RIGHT:
            //        g.DrawImage(rightSprite, (int)(e.Pos.X - (scale + scale)), (int)(e.Pos.Y - (scale + scale)));
            //        //g.DrawImage(rightSprite, (int)(e.Pos.X - (scale + scale)), (int)(e.Pos.Y - (scale + scale + scale - 5)));
            //        break;
            //    case EntityDirection.LEFT:
            //        g.DrawImage(leftSprite, (int)(e.Pos.X - (scale + scale / 2 + 5)), (int)(e.Pos.Y - (scale + scale)));
            //        //g.DrawImage(leftSprite, (int)(e.Pos.X - (scale + scale)), (int)(e.Pos.Y - (scale + scale + scale + -5)));
            //        break;
            //    case EntityDirection.UP:
            //        g.DrawImage(upSprite, (int)(e.Pos.X - (scale + scale - 5)), (int)(e.Pos.Y - (scale + scale - 5)));
            //        //g.DrawImage(upSprite, (int)(e.Pos.X - (scale + scale + 10)), (int)(e.Pos.Y - (scale + scale + scale - scale)));
            //        break;
            //    case EntityDirection.DOWN:
            //        g.DrawImage(downSprite, (int)(e.Pos.X - (scale + scale - 5)), (int)(e.Pos.Y - (scale + scale)));
            //        //g.DrawImage(downSprite, (int)(e.Pos.X - (scale + scale - 5)), (int)(e.Pos.Y - (scale + scale)));
            //        break;
            //}
        }
    }
}
