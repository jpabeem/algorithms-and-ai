using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.util.sprites
{
    public class RedFishSprite : FishSprite, ISpriteMode
    {
        protected override void InitSprites()
        {
            leftSprite = SteeringCS.Properties.Resources.redFishLeft;
            rightSprite = SteeringCS.Properties.Resources.redFishRight;
            upSprite = SteeringCS.Properties.Resources.redFishUp;
            downSprite = SteeringCS.Properties.Resources.redFishDown;
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
