using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.util.sprites
{
    public class FillCircle : ISpriteMode
    {
        public void RenderSprite(Graphics g, BaseGameEntity e)
        {
            double leftCorner = e.Pos.X - e.Scale;
            double rightCorner = e.Pos.Y - e.Scale;
            float size = e.Scale * 2;

            Brush brush;

            if (e is Vehicle entity)
                brush = new SolidBrush(entity.VColor);
            else if (e is Obstacle obstacle)
                brush = new SolidBrush(obstacle.OColor);
            else
                brush = new SolidBrush(Color.Black); // fallback for entities other than vehicle

            g.FillEllipse(brush, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));

        }
    }
}
