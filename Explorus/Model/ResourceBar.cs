/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System;
using System.Drawing;

namespace Explorus
{
    public class ResourceBar
    {
        private int currentVal;
        private Image icon;
        private Image bar;
        private Image barStart = new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(96, 48, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat);
        private Image barEnd = new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(480, 48, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat);
        private Image barEmpty = new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(432, 48, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat);

        public ResourceBar(int initialVal, Image barImage, Image barIcon)
        {
            icon = barIcon;
            bar = barImage;

            currentVal = initialVal;
        }

        public void SetValue(int value)
        {
            currentVal = value;
        }

        public Image GetBarImage()
        {
            Image bitmap = new Bitmap(288, 96);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(icon, 0, 0);
                g.DrawImage(barStart, 16, 0);
                g.DrawImage(barEnd, 238, 0);
                g.DrawImage(barEmpty, new Rectangle(new Point(64, 0), new Size(176, 48)));
                g.DrawImage(bar, new Rectangle(new Point(64, 0), new Size((int)Math.Round(currentVal * 176.0 / 100.0), 48)));
            }

            return bitmap;
        }
    }

}