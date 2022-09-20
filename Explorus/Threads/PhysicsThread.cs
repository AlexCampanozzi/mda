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
        List<GameObject> removeBuffer = new List<GameObject>();
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

                if(removeBuffer.Count > 0)
                {
                    //GameObject obj = removeBuffer[0];
                    removeBuffer.First().removeItselfFromGame();
                    removeBuffer.RemoveAt(0);
                }

            }
        }

        private void MoveObject(PlayMovement movement)
        {
            if(movement.obj != null)
            {
                if(!checkCollision(movement)) movement.obj.Move(movement.dir, movement.speed);
                
            }
        }

        private bool checkCollision(PlayMovement movement)
        {
            List<Collider> colliders = new List<Collider>();
            Collider col = movement.obj.GetCollider();
            Map map = Map.Instance;
            List<Collider> collisions = new List<Collider>();
            objectTypes[,] gridMap = Map.Instance.GetTypeMap();
            bool wall = false;
            foreach (GameObject obj in map.GetObjectList())
            {
                if(obj.GetID() != movement.obj.GetID())
                {
                    Collider objCollider = obj.GetCollider();
                    if (objCollider != null)
                    {
                        if (objCollider.isColliderTouching(col))
                        {
                            objectTypes collisionType = gridMap[obj.GetGridPosition().X,obj.GetGridPosition().Y];
                            if(collisionType == objectTypes.Wall || collisionType == objectTypes.Door)
                            {
                                wall = true;
                            }
                            collisions.Add(objCollider);
                        }
                    }
                }
            }
            foreach (Collider collision in collisions)
            {
                movement.obj.OnCollisionEnter(collision);
            }
            return wall;
        }

        public void clearBuffer(RigidBody rmv_obj)
        {
            int count = movementBuffer.Count;
            for (int i=0; i<count;i++)
            {
                if (movementBuffer[count-i-1].obj == rmv_obj) movementBuffer.RemoveAt(count-i-1);
            }
        }

        public void removeFromGame(GameObject obj)
        {
            removeBuffer.Add(obj);
        }
    }
}
