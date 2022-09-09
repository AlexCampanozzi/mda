using System;
using System.Drawing;

/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

namespace Explorus.Model
{
    public sealed class Header
    {
        private ResourceBar lifeBar;
        private ResourceBar slimeBar;
        private ResourceBar gemBar;

        private Image keyImage = new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(528, 0, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat);
        private bool key = false;

        private static Header instance = null;
        private static readonly object padlock = new object();

        private Image logo = new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(96, 0, 192, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat);

        public Header()
        {
            lifeBar = new ResourceBar(100, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(144, 48, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat), new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(288, 0, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat));
            slimeBar = new ResourceBar(100, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(240, 48, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat), new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(336, 0, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat));
            gemBar = new ResourceBar(0, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(336, 48, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat), new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(480, 0, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat));
        }

        public static Header GetInstance()
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Header();
                    }
                }
            }
            return instance;
        }

        public void setLife(int life)
        {
            lifeBar.SetValue(life);
        }

        public void setSlime(int slime)
        {
            slimeBar.SetValue(slime);
        }

        public void setGem(int gem)
        {
            gemBar.SetValue(gem);
        }

        public Image getHeaderImage()
        {
            Image bitmap = new Bitmap(1152, 96);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(logo, 0, 0);
                g.DrawImage(lifeBar.GetBarImage(), 240, 0);
                g.DrawImage(slimeBar.GetBarImage(), 528, 0);
                g.DrawImage(gemBar.GetBarImage(), 816, 0);
                if(key)
                {
                    g.DrawImage(keyImage, 1070, 0);
                }
            }

            return bitmap;
        }

        public void setKey(bool keyVisible)
        {
            key = keyVisible;
        }
    }

}