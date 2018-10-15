using SteeringCS.behaviour;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{
    public enum Behaviour
    {
        SEEK,
        ARRIVE,
        FLEE,
        WANDER,
        PURSUIT,
        PATH_FOLLOWING,
        EXPLORE,
        SLOWPOKE,
        DEFAULT
    }

    public class Vehicle : MovingEntity
    {
        public Color VColor { get; set; }
        public Behaviour Behaviour { get; set; }

        public Vehicle(Vector2D pos, World w, Behaviour b = Behaviour.DEFAULT) : base(pos, w)
        {
            Velocity = new Vector2D(0, 0);
            Scale = 15;

            SetBehaviour(b);

            VColor = Color.Black;
        }

        /// <summary>
        /// Set the steering behaviour of the vehicle.
        /// </summary>
        /// <param name="behaviour"></param>
        public void SetBehaviour(Behaviour behaviour, BaseGameEntity target = null)
        {
            switch (behaviour)
            {
                case Behaviour.SEEK:
                    this.SB = new SeekBehaviour(this);
                    break;
                case Behaviour.ARRIVE:
                    this.SB = new ArriveBehaviour(this);
                    break;
                case Behaviour.FLEE:
                    this.SB = new FleeBehaviour(this);
                    break;
                case Behaviour.WANDER:
                    this.SB = new WanderBehaviour(this);
                    break;
                case Behaviour.PURSUIT:
                    this.SB = new PursuitBehaviour(this);
                    break;
                case Behaviour.PATH_FOLLOWING:
                    this.SB = new PathFollowingBehaviour(this);
                    break;
                case Behaviour.EXPLORE:
                    this.SB = new ExploreBehaviour(this, target);
                    break;
                case Behaviour.SLOWPOKE:
                    this.SB = new SlowpokeBehaviour(this);
                    break;
                default:
                    this.SB = new DefaultBehaviour(this);
                    break;
            }
            this.Behaviour = behaviour;
        }

        /// <summary>
        /// Render the vehicle.
        /// </summary>
        /// <param name="g"></param>
        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;

            // image points
            PointF leftCornerPoint = new PointF((float)(Pos.X - Scale), (float)Pos.Y);
            PointF rightCornerPoint = new PointF((float)(Pos.X + Scale), (float)Pos.Y - Scale);
            PointF lowerLeftCornerPoint = new PointF((float)(Pos.X), (float)Pos.Y + Scale);
            PointF lowerRightCornerPoint = new PointF((float)(Pos.X + Scale), (float)(Pos.Y + Scale));
            //PointF lowerLeftCornerPoint = new PointF((float)(Pos.X - Scale));
            PointF[] destinationParams = { leftCornerPoint, rightCornerPoint, lowerLeftCornerPoint };

            float size = Scale * 2;

            Pen p = new Pen(VColor, 2);
            Brush brush = new SolidBrush(VColor);
            Pen vectorPen = new Pen(Color.Black, 2);
            Pen collisionPen = new Pen(Color.Red, 2);

            var castedEntity = (BaseGameEntity)this;

            SpriteStrategy.RenderSprite(g, this);          

            // draw the explore radius
            if (Behaviour == Behaviour.EXPLORE)
            {
                var exploreRadiusSize = ExploreRadius * 2;
                var explorePen = new Pen(Color.DarkGray, 2);
                explorePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                g.DrawEllipse(explorePen, new Rectangle((int)(Pos.X-ExploreRadius), (int)(Pos.Y-ExploreRadius), (int)exploreRadiusSize, (int)exploreRadiusSize));
            }

            Image sprite;

            if (MyWorld.Settings.Get("SpritesEnabled"))
            {
                if (SB == null)
                {
                    sprite = Image.FromFile("D:\\Google Drive\\M6a\\Algorithms & AI\\Final_Project_Jeroen_Beemsterboer_s1091463\\sprites\\crosshair_sprite.png");

                    g.DrawImage(sprite, destinationParams);
                }
                else
                {
                    switch (Behaviour)
                    {
                        case Behaviour.PURSUIT:
                            sprite = Image.FromFile("D:\\Google Drive\\M6a\\Algorithms & AI\\Final_Project_Jeroen_Beemsterboer_s1091463\\sprites\\rat_sprite_version_1.png");
                            break;
                        case Behaviour.SEEK:
                            sprite = Image.FromFile("D:\\Google Drive\\M6a\\Algorithms & AI\\Final_Project_Jeroen_Beemsterboer_s1091463\\sprites\\cat.png");
                            break;
                        default:
                            sprite = Image.FromFile("D:\\Google Drive\\M6a\\Algorithms & AI\\Final_Project_Jeroen_Beemsterboer_s1091463\\sprites\\rat_sprite_version_1.png");
                            break;
                    }

                    //PointF[] destinationParams = { leftCornerPoint, rightCornerPoint, lowerLeftCornerPoint };

                    g.DrawImage(sprite, destinationParams);
                }
            }
        }
    }
}
