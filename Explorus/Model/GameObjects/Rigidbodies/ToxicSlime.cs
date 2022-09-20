using System;
using System.Drawing;
using System.Windows.Forms;
/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System.Collections.Generic;
using Explorus.Controller;
using Explorus.Model.GameObjects.Rigidbodies;
using System.Drawing;

namespace Explorus.Model
{
    public class ToxicSlime : GameObject
    {
        private Animator animator;
        private Image image;

        private int last_slimeDirX = 0;
        private int last_slimeDirY = 0;

        private static readonly Dictionary<int, Image> states = new Dictionary<int, Image>()
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

        public ToxicSlime(Point pos, ImageLoader loader) : base(pos, loader.ToxicSlimeImage)
        {
            int[] order = { 2, 3, 2, 1 };
            animator = new directionAnimator(states, order);

        }

        private void SetImage()
        {
            int progress = (int)(position.X % 96.0 + position.Y % 96.0);
            image = animator.Animate(progress, last_slimeDirX, last_slimeDirY);
        }
    }
}
