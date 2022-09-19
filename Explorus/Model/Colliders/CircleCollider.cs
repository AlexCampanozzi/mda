using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model
{
    public class CircleCollider : Collider
    {
        int radius;

        public CircleCollider(GameObject parent, int r) : base(parent)
        {
            radius = r;
        }

        public int getRadius()
        {
            return radius;
        }

        public bool isColliderTouching(SquareCollider otherCollider)
        {
            return otherCollider.isColliderTouching(this);
        }

        public bool isColliderTouching(CircleCollider otherCollider)
        {
            Point pos = parent.GetPosition();
            Point theirPos = otherCollider.parent.GetPosition();

            double dX = Math.Abs(pos.X - theirPos.X);
            double dY = Math.Abs(pos.Y - theirPos.Y);

            double dist = Math.Sqrt(Math.Exp(dX) + Math.Exp(dY));

            if (dist < radius + otherCollider.radius)
                return true;

            return false;
        }
    }
}
