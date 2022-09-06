using System.Drawing;
using System.Drawing.Imaging;

namespace Explorus
{
    public class Door : Wall
    {
        public Door(Point pos) : base(pos)
        {
            SetOpacity();
        }
        public Door(int x, int y) : base(x, y)
        {
            SetOpacity();
        }

        
        private void SetOpacity()
        {
            Image img = new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 0, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat);
            Bitmap bitmap = new Bitmap(img.Width, img.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            ColorMatrix matrix = new ColorMatrix();
            matrix.Matrix33 = 0.5f;
            ImageAttributes imgAtt = new ImageAttributes();
            imgAtt.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAtt);
            graphics.Dispose();

            SetImage(bitmap.Clone(new Rectangle(0, 0, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat));
        }
    }

}