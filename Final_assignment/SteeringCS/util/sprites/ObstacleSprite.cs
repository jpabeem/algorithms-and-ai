using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.sprites
{
    public abstract class ObstacleSprite : ISpriteMode
    {
        private Point Offset { get; set; }
        public void RenderSpriteWithOffset(Graphics g, BaseGameEntity e, Point offset)
        {
            Offset = offset;
            RenderSprite(g, e);
        }

        public void RenderSprite(Graphics g, BaseGameEntity e)
        {
            double scale = e.Scale;
            double leftCorner = e.Pos.X - scale;
            double rightCorner = e.Pos.Y - scale;
            float size = (float)scale * 2;
            var MyWorld = e.MyWorld;

            if (!(e is Obstacle))
                throw new ArgumentException("Cannot render sprite for current entity type.");

            Obstacle obstacle = (Obstacle)e;

            if (e.MyWorld.Settings.Get("ToggleObstacleBoundingBox"))
                g.DrawEllipse(new Pen(obstacle.OColor, 2), new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));

            g.DrawImage(obstacle.Sprite, (int)(e.Pos.X + Offset.X), (int)(e.Pos.Y + Offset.Y));
        }
    }
}
