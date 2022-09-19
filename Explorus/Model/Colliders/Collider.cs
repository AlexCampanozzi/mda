using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model
{
    public class Collider
    {
        protected GameObject parent;
        public Collider(GameObject parentObject)
        {
            parent = parentObject;
        }
        virtual public bool ColliderTouching(Collider otherCollider)
        {
            return false; 
        }
    }
}
