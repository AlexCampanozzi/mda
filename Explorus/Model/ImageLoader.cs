using System.Drawing;

namespace Explorus.Model
{
    public class ImageLoader
    {
        Image wallImage = new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 0, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat);
        Image gemImage = new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(480, 0, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat);
        Image slimeImage = new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(528, 48, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat);
        Image slimusImage = new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 192, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat);


        private static ImageLoader instance = null;
        private static readonly object padlock = new object();

        public static ImageLoader GetInstance()
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ImageLoader();
                    }
                }
            }
            return instance;
        }

        public Image WallImage { get => wallImage;}
        public Image GemImage { get => gemImage;}
        public Image SlimeImage { get => slimeImage;}

        public Image SlimusImage { get => slimusImage;}
    }
}
