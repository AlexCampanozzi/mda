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

        public override bool isColliderTouching(Collider otherCollider)
        {
            if(otherCollider is SquareCollider)
            {
                return otherCollider.isColliderTouching(this);
            }
            else if(otherCollider is CircleCollider)
            {
                Point pos = parent.GetPosition();
                Point theirPos = otherCollider.parent.GetPosition();

                double dX = Math.Abs(pos.X - theirPos.X);
                double dY = Math.Abs(pos.Y - theirPos.Y);

                double dist = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));

                if (dist < radius + ((CircleCollider)otherCollider).radius)
                    return true;

                return false;
            }
            return false;
        }
    }
}
