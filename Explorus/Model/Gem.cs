using System.Drawing;

namespace Explorus
{
    public class Gem : Collectable
    {
        public Gem(Point pos) : base(pos, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(480, 0, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat))
        {

        }

        public Gem(int x, int y) : base(x, y, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(480, 0, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat))
        {

        }
    }
}