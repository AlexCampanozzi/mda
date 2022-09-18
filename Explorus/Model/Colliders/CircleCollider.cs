using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model
{
    public class CircleCollider : Collider
    {
        int radius;

        public CircleCollider(int r)
        {
            radius = r;
        }

        public int getRadius()
        {
            return radius;
        }
    }
}
