using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.sprites
{
    public class FoodSourceSprite : ObstacleSprite, ISpriteMode
    {
        private int value;

        public FoodSourceSprite()
        {
            Random r = new Random();
            value = r.Next(1, 100);
        }

        public new void RenderSprite(Graphics g, BaseGameEntity e)
        {
            int scale = (int)e.Scale;
            var obstacle = (Obstacle)e;

            if (value % 2 == 1)
            {
                obstacle.Sprite = SteeringCS.Properties.Resources.chicken_drumstick;
                RenderSpriteWithOffset(g, e, new Point(-scale - 8, -scale - 15));
            }
            else
            {
                obstacle.Sprite = SteeringCS.Properties.Resources.tomato;
                RenderSpriteWithOffset(g, e, new Point(-8, -8));
            }

        }
    }
}
