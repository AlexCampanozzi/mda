using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model
{
    public class Direction
    {
        private int dirX, dirY;
        public Direction(int x, int y)
        {
            dirX = x;
            dirY = y;
        }
        public int X { get => dirX; set => dirX = value; }
        public int Y { get => dirY; set => dirY = value; }
    }
}
