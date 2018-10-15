using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.util.sprites
{
    public class OutlineCircle : ISpriteMode
    {
        public void RenderSprite(Graphics g, BaseGameEntity e)
        {
            double leftCorner = e.Pos.X - e.Scale;
            double rightCorner = e.Pos.Y - e.Scale;
            float size = e.Scale * 2;

            bool isNonCollisionObstacle = false;

            Pen pen;

            if (e is Vehicle entity)
                pen = new Pen(entity.VColor, 2);
            else if (e is Obstacle obstacle)
            {
                pen = new Pen(obstacle.OColor, 2);
                if (obstacle.Type != ObstacleType.DEFAULT)
                    isNonCollisionObstacle = true;
            }
            else
                pen = new Pen(Color.Black, 2); // fallback for entities other than vehicle

            if (isNonCollisionObstacle)
            {
                if (e.MyWorld.Settings.Get("ToggleObstacleBoundingBox"))
                    g.DrawEllipse(pen, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
            }else
                g.DrawEllipse(pen, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
        }
    }
}
