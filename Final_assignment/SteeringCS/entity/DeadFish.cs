using SteeringCS.util.sprites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{
    public class DeadFish : Vehicle
    {
        public bool PickedUp
        {
            get
            {
                return PickedUp;
            }

            set
            {
                if (value)
                {
                    // picked up = true, stop wandering
                    SetBehaviour(Behaviour.DEFAULT);
                }
                else
                {
                    SetBehaviour(Behaviour.WANDER);
                }
            }
        }

        public DeadFish(Vector2D pos, World w, Behaviour b = Behaviour.WANDER) : base(pos, w, b)
        {
            MaxSpeed = 0.75f;
            SpriteStrategy = new DeadFishSprite();
        }

        public override void Render(Graphics g)
        {
            SpriteStrategy.RenderSprite(g, this);

            Pen vectorPen = new Pen(Color.Black, 2);
            Pen collisionPen = new Pen(Color.Red, 2);

            if (MyWorld.Mode == CursorMode.DEBUG_INFO)
            {
                g.DrawLine(vectorPen, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Heading.X * MAX_SEE_AHEAD), (int)Pos.Y + (int)(Heading.Y * MAX_SEE_AHEAD));
                g.DrawLine(collisionPen, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));
            }
        }
    }
}
