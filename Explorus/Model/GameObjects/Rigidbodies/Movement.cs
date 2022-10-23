using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model
{

    [Serializable]
    internal class Movement
    {
        private int speed;

        public Movement(int Speed, Point initial_pos)
        {
            speed = Speed;
        }

        public (Point,Point) update(Point currentPos, Point currentGrid, int DirX, int DirY)
        {
            Point position = new Point(currentPos.X + DirX * speed, currentPos.Y + DirY * speed);

            Point gridPos = new Point(currentGrid.X, currentGrid.Y);

            if (DirX == 1)
            {
                if (position.X >= ((gridPos.X + DirX) * 96.0))
                {
                    gridPos.X = gridPos.X + DirX;
                    position.X = gridPos.X * 96;
                }
            }
            else if (DirX == -1)
            {
                if (position.X <= ((gridPos.X + DirX) * 96.0))
                {
                    gridPos.X = gridPos.X + DirX;
                    position.X = gridPos.X * 96;
                }
            }
            else if (DirY == 1)
            {
                if (position.Y >= ((gridPos.Y + DirY) * 96.0))
                {
                    gridPos.Y = gridPos.Y + DirY;
                    position.Y = gridPos.Y * 96;
                }
            }
            else if (DirY == -1)
            {
                if (position.Y <= ((gridPos.Y + DirY) * 96.0))
                {
                    gridPos.Y = gridPos.Y + DirY;
                    position.Y = gridPos.Y * 96;
                }
            }

            return (position, gridPos);
        }
    }
}
