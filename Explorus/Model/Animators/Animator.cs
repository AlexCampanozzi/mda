using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model.GameObjects.Rigidbodies
{
    internal class Animator
    {
        private Image image;
        public Animator()
        {
        }

        public virtual Image Animate(int progress, int last_slimeDirX, int last_slimeDirY)
        {
            return image;
        }
        public virtual Image Animate(int progress)
        {
            return image;
        }
        public Image halfOpacity(Image img)
        {
            Bitmap bitmap = new Bitmap(img.Width, img.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            ColorMatrix matrix = new ColorMatrix();
            matrix.Matrix33 = 0.5f;
            ImageAttributes imgAtt = new ImageAttributes();
            imgAtt.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAtt);
            graphics.Dispose();

            return (bitmap.Clone(new Rectangle(0, 0, img.Width, img.Height), new Bitmap("./Resources/TilesSheet.png").PixelFormat));
        }
    }
}
