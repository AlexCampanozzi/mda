﻿using System.Drawing;

namespace Explorus
{
    public class Wall:GameObject
    {
        public Wall(Point pos) : base(pos, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 0, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat))
        {

        }

        public Wall(int x, int y) : base(x, y, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 0, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat))
        {

        }
    }
}