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

        private Dictionary<int, Image> states;

        //Map map = Map.GetInstance();
        public ToxicSlime(Point pos, ImageLoader loader, int ID) : base(pos, loader.ToxicSlimeImage, ID)
        {
            int[] order = { 2, 3, 2, 1 };
            collider = new CircleCollider(this, 39);
            animator = new directionAnimator(states, order);
            states = loader.ToxicSlimeImages;

        }
        private void SetImage()
        {
            int progress = (int)(position.X % 96.0 + position.Y % 96.0);
            image = animator.Animate(progress, last_slimeDirX, last_slimeDirY);
        }
    }
}
