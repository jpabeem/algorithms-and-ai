using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.sprites
{
    public interface ISpriteMode
    {
        void RenderSprite(Graphics g, BaseGameEntity e);
    }
}
