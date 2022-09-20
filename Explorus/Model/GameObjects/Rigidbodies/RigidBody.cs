using Explorus.Threads;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model
{
    public class RigidBody : GameObject
    {
        private PhysicsThread physics = PhysicsThread.GetInstance();

        private Map map = Map.Instance;
        public RigidBody(Point pos, Image img, int ID) : base(pos, img, ID)
        {

        }
        public virtual void Move(Direction dir, int speed)
        {
            if(dir != null)
            {
                position.X += dir.X * speed;
                position.Y += dir.Y * speed;
            }
        }

        public Map getMap()
        {
            return map;
        }

        public virtual void OnCollisionEnter(Collider otherCollider)
        {

        }
    }
}
