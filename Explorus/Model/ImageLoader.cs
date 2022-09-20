using System.Collections.Generic;
using System.Drawing;

namespace Explorus.Model
{
    public class ImageLoader
    {
        Image wallImage = new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 0, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat);
        Image gemImage = new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(480, 0, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat);
        Image slimeImage = new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(528, 48, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat);
        Image slimusImage = new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 192, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat);
        Image toxicSlimeImage = new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 384, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat);

        private static readonly Dictionary<int, Image> bubbleImages = new Dictionary<int, Image>()
        {
            {0, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(336, 0, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {1, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(384, 0, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {2, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(432, 0, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat)}
        };

        private static readonly Dictionary<int, Image> slimusImages = new Dictionary<int, Image>()
        {
            {1, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {2, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(96, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {3, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(192, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {11, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(288, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {12, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(384, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {13, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(480, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {21, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 192, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {22, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(96, 192, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {23, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(192, 192, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {31, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(288, 192, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {32, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(384, 192, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {33, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(480, 192, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
        };

        private static readonly Dictionary<int, Image> toxicSlimeImages = new Dictionary<int, Image>()
        {
            {1, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 288, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {2, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(96, 288, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {3, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(192, 288, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {11, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(288, 288, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {12, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(384, 288, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {13, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(480, 288, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {21, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 384, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {22, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(96, 384, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {23, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(192, 384, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {31, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(288, 384, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {32, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(384, 384, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {33, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(480, 384, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
        };

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

        public Image WallImage { get => wallImage; }
        public Image GemImage { get => gemImage; }
        public Image SlimeImage { get => slimeImage; }
        public Image SlimusImage { get => slimusImage; }
        public Image ToxicSlimeImage { get => toxicSlimeImage; }
        public Dictionary<int, Image> BubbleImages { get => bubbleImages; }
        public Dictionary<int, Image> SlimusImages { get => slimusImages;}
        public Dictionary<int, Image> ToxicSlimeImages { get => toxicSlimeImages; }

    }
}
