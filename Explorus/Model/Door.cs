﻿/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System.Drawing;
using System.Drawing.Imaging;

namespace Explorus.Model
{
    public class Door : Wall
    {
        ImageLoader imageLoader;
        public Door(Point pos, ImageLoader loader) : base(pos, loader)
        {
            imageLoader = loader;

            SetOpacity();
        }

        
        private void SetOpacity()
        {
            Image img = imageLoader.WallImage;
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