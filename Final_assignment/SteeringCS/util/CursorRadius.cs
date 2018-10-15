using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util
{
    public class CursorRadius : UtilCircle
    {
        World MyWorld { get; set; }

        public CursorRadius(Point p, World world) : base(p.X, p.Y, 15)
        {
            MyWorld = world;
        }

        public void UpdatePosition(int x, int y)
        {
            Pos.X = x;
            Pos.Y = y;
        }

        public void Render(Graphics g)
        {
            double leftCorner = Pos.X - Radius;
            double rightCorner = Pos.Y - Radius;

            double size = Radius * 2;

            SolidBrush brush = new SolidBrush(Color.Crimson);

            // only draw the ellipse when there is no entity selected
            //if (MyWorld.DebugEntity == null)
            //    g.FillEllipse(brush, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
        }
    }
}