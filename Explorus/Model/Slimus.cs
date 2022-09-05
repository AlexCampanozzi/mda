using System.Drawing;

namespace Explorus
{
    public class Slimus : GameObject
    {
        private int SlimePositionX = 0;
        private int SlimePositionY = 0;
        private int slimeVelocity = 5;
        public Slimus(Point pos) : base(pos, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat))
        {

        }

        public Slimus(int x, int y) : base(x, y, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat))
        {

        }

        public void update()
        {

        }
    }
}