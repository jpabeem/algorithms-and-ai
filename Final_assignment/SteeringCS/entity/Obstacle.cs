using SteeringCS.util.sprites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{
    public enum ObstacleShape { CIRCLE, RECTANGLE, SPRITE };
    public enum ObstacleType { DEFAULT, COLLISION_ALLOWED, GRAVE_STONE, FOOD_SOURCE };
    public class Obstacle : BaseGameEntity
    {
        public ObstacleShape Shape { get; set; }
        public ObstacleType Type { get; set; }
        public Color OColor { get; set; }
        public Image Sprite { get; set; }

        public Obstacle(Vector2D pos, World w, ObstacleShape shape, ISpriteMode strategy = null, ObstacleType type = ObstacleType.DEFAULT) : base(pos, w)
        {
            Shape = shape;
            Type = type;

            Scale = 45;
            OColor = Color.Black;

            // if a sprite is set: render it, if not: render an outline circle only
            if (strategy != null)
            {
                SpriteStrategy = strategy;
            }
            else
            {
                SpriteStrategy = new OutlineCircle();
            }
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;

            float size = Scale * 2;

            Pen pen = new Pen(OColor, 2);
            Brush brush = new SolidBrush(OColor);


            SpriteStrategy.RenderSprite(g, this);

            /// TODO: implement different shapes within ISpriteMode
            //switch (Shape)
            //{
            //    case ObstacleShape.CIRCLE:
            //        //g.FillEllipse(brush, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
            //        g.DrawEllipse(pen, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
            //        break;
            //    case ObstacleShape.RECTANGLE:
            //        //g.FillRectangle(brush, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
            //        g.DrawRectangle(pen, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
            //        break;
            //    default:
            //        g.FillRectangle(brush, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
            //        break;
            //}
        }

        public override void Update(float delta)
        {
            throw new NotImplementedException();
        }
    }
}
