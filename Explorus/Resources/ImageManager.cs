using System.Drawing;
namespace Explorus
{
    public class ImageManager
    {
        public Image Wall;
        public Image Player;
        public Image Key;

        public ImageManager()
        {
            Bitmap myBitmap = new Bitmap("./Resources/TilesSheet.png");
            Rectangle cloneRect = new Rectangle(0, 0, 96, 96);
            Wall = myBitmap.Clone(cloneRect, myBitmap.PixelFormat);
            cloneRect = new Rectangle(0, 96, 96, 96);
            Player = myBitmap.Clone(cloneRect, myBitmap.PixelFormat);
            cloneRect = new Rectangle(528, 0, 48, 48);
            Key = myBitmap.Clone(cloneRect, myBitmap.PixelFormat);
        }

        public Image GetImage(objectTypes gameObject)
        {
            switch (gameObject)
            {
                case objectTypes.Player:
                    return Player;
                case objectTypes.Wall:
                    return Wall;
                case objectTypes.Key:
                    return Key;
                default:
                    return Wall;
            }
        }
    }
}