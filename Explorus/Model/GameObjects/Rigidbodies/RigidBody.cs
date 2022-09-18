using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model
{
    public class RigidBody : GameObject
    {
        Collider collider;
        public RigidBody(Point pos, Image img) : base(pos, img)
        {

        }
        public virtual bool TryMove(Direction dir)
        {
            return false;
        }

        public virtual void OnCollisionEnter(Collider otherCollider)
        {

        }
    }
}
