using Explorus.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace Explorus.Threads
{
    public struct PlayMovement
    {
        public RigidBody obj;
        public Direction dir;
        public int speed;
    }
    internal class PhysicsThread
    {
        private static PhysicsThread instance = null;
        private static readonly object padlock = new object();

        List<PlayMovement> movementBuffer = new List<PlayMovement>();
        private PhysicsThread()
        {
        }

        public static PhysicsThread GetInstance()
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new PhysicsThread();
                    }
                }
            }
            return instance;
        }

        public void addMove(PlayMovement movement)
        {
            movementBuffer.Add(movement);
        }
        
        
        public void Run()
        {
            while (true)
            {
                if(movementBuffer.Count > 0)
                {
                    MoveObject(movementBuffer.First());
                    movementBuffer.RemoveAt(0);
                }

            }
        }

        private void MoveObject(PlayMovement movement)
        {
            if(movement.obj != null)
            {
                movement.obj.Move(movement.dir, movement.speed);
                checkCollision(movement);
            }
        }

        private void checkCollision(PlayMovement movement)
        {
            List<Collider> colliders = new List<Collider>();
            Collider col = movement.obj.GetCollider();
            Map map = Map.Instance;
            List<Collider> collisions = new List<Collider>();

            foreach (GameObject obj in map.GetObjectList())
            {
                if(obj.GetID() != movement.obj.GetID())
                {
                    Collider objCollider = obj.GetCollider();
                    if (objCollider != null)
                    {
                        if (objCollider.isColliderTouching(col))
                        {
                            collisions.Add(objCollider);
                        }
                    }
                }
            }
            foreach (Collider collision in collisions)
            {
                movement.obj.OnCollisionEnter(collision);
            }

        }
    }
}
