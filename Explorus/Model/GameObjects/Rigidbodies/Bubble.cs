using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model
{
    public class Bubble : RigidBody
    {
        public Bubble(Point pos, ImageLoader loader, int ID) : base(pos, loader.BubbleImage, ID)
        {
            collider = new CircleCollider(this, 24);
        }
    }
}
