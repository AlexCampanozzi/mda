using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model
{
    public class SquareCollider : Collider
    {
        int sizeX;
        int sizeY;

        public SquareCollider(int X, int Y)
        {
            sizeX = X;
            sizeY = Y;
        }

        public int getX()
        {
            return sizeX;
        }

        public int getY()
        {
            return sizeY;
        }
    }
}
