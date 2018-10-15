﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.sprites
{
    public class BlueFishSprite : FishSprite, ISpriteMode
    {
        protected override void InitSprites()
        {
            leftSprite = SteeringCS.Properties.Resources.blueFishLeft;
            rightSprite = SteeringCS.Properties.Resources.blueFishRight;
            upSprite = null;
            downSprite = null;
        }

        protected override void InitOffsets()
        {
            leftOffset = new Point((int)(entity.Scale + entity.Scale), (int)(entity.Scale + entity.Scale));
            rightOffset = new Point((int)(entity.Scale + entity.Scale + 10), (int)(entity.Scale + entity.Scale));
            upOffset = new Point((int)entity.Scale, (int)entity.Scale);
            downOffset = new Point((int)entity.Scale, (int)entity.Scale);
        }

        public new void RenderSprite(Graphics g, BaseGameEntity e)
        {
            entity = e;
            InitSprites();
            InitOffsets();
            base.RenderSprite(g, e);
        }
    }
}
