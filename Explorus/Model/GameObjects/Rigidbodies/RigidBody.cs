﻿using Explorus.Threads;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model
{

    [Serializable]
    public class RigidBody : GameObject
    {
        [NonSerialized]
        private PhysicsThread physics = PhysicsThread.GetInstance();

        private static readonly object padlock = new object();
        protected Direction direction;
        protected Direction last_direction;
        [NonSerialized]
        private Map map = Map.Instance;
        public RigidBody(Point pos, Image img, int ID) : base(pos, img, ID)
        {

        }
        public virtual void Move(Direction dir, int speed)
        {
            if(dir != null)
            {
                lock (padlock)
                {
                    position.X += dir.X * speed; //Ajouter mutex sur la position
                    position.Y += dir.Y * speed;
                }
            }   
        }

        public Map getMap()
        {
            return map;
        }

        public Direction getDirection()
        {
            return direction;
        }
        public Direction getLastDirection()
        {
            return last_direction;
        }
        public virtual void OnCollisionEnter(Collider otherCollider)
        {

        }
    }
}
