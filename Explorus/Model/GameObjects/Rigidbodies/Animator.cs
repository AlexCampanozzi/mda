using System;
using System.Collections.Generic;
using System.Drawing;
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
    }
}
