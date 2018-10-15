using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.sprites
{
    public class GrassSprite : ObstacleSprite, ISpriteMode
    {
        public new void RenderSprite(Graphics g, BaseGameEntity e)
        {
            int scale = (int)e.Scale;
            var obstacle = (Obstacle)e;
            obstacle.Sprite = SteeringCS.Properties.Resources.grass;
            RenderSpriteWithOffset(g, e, new Point(-scale + 3, -scale + 3));
        }
    }
}
