using System.Drawing;

namespace Explorus
{
    public class Slimus : GameObject
    {
        Mouvement mouvement = new Mouvement();

        public Slimus(Position pos, Image2D img) : base(pos, img)
        {
            /*Bitmap myBitmap = new Bitmap("./Resources/TilesSheet.png");
            Rectangle slimusRectangle = new Rectangle(0, 96, 96, 96);

            img = new Image2D(0, 0, myBitmap.Clone(slimusRectangle, myBitmap.PixelFormat));
            pos = new Position(5, 5);
            */
        }
    }
}
