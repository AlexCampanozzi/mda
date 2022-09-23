/* code fsp du physics thread le 2 est utilisé comme limite pour simplifier l'affichage des états sur ltsa
* MOVEMENTBUFFER(N=2) = MOVEMENTBUFFER[0],
*MOVEMENTBUFFER[i:0..N] = (when(i<N) addMove -> MOVEMENTBUFFER[i+1]
*| when(i>0) moveObject  -> checkCollision -> MOVEMENTBUFFER[i-1]
*| when(i>0) clearBuffer -> MOVEMENTBUFFER[i-1]
*).
*
*REMOVEBUFFER(N=2) = REMOVEBUFFER[0],
*REMOVEBUFFER[i:0..N] = (when(i<N) removeFromGame -> REMOVEBUFFER[i+1] 
*| when(i>0) removeItselfFromGame -> REMOVEBUFFER[i-1]
*).
*
*MAIN = (addMove -> MAIN | removeFromGame -> MAIN | clearBuffer -> MAIN).
*PHYSICSTHREAD = (moveObject -> PHYSICSTHREAD | removeItselfFromGame -> PHYSICSTHREAD).
*
*||BUFFERS = (MAIN || MOVEMENTBUFFER || PHYSICSTHREAD 
*|| MAIN || REMOVEBUFFER || PHYSICSTHREAD).  
*/

using Explorus.Controller;
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
     public sealed class PhysicsThread
     {
        private static PhysicsThread instance = null;
        private static readonly object padlock = new object();
        private List<(int,int)> validDirection = new List<(int, int)>()
        {
            (0,1), (0,-1), (1,0),(-1,0)
        };
        List<PlayMovement> movementBuffer = new List<PlayMovement>() { };
        List<GameObject> removeBuffer = new List<GameObject>() { };
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
            if (GameEngine.GetInstance().GetState().GetType() == typeof(PlayState))
            {
                if (movement.dir != null && validDirection.Contains((movement.dir.X, movement.dir.Y)) && movement.obj != null && movement.speed > 0)
                    lock (movementBuffer)
                    {
                        movementBuffer.Add(movement);
                    }
            }
        }
        
        
        public void Run()
        {
            while (true)
            {
                if (GameEngine.GetInstance().GetState().GetType() == typeof(PlayState))
                {
                    if (movementBuffer.Count > 0)
                    {
                        lock (movementBuffer)
                        {
                            try
                            {
                                MoveObject(movementBuffer.First());
                                movementBuffer.RemoveAt(0);
                            }
                            catch
                            {

                            }
                        }
                    }

                    if (removeBuffer.Count > 0)
                    {
                        lock (removeBuffer)
                        {
                            //GameObject obj = removeBuffer[0];
                            removeBuffer.First().removeItselfFromGame();
                            removeBuffer.RemoveAt(0);
                        }
                    }
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
                            if(collisionType == objectTypes.Wall || (collisionType == objectTypes.Door && !GameMaster.Instance.GetKeyStatus()))
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
            if (rmv_obj != null)
            {
                lock (movementBuffer)
                {
                    int count = movementBuffer.Count;
                    if (count > 0)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if (movementBuffer[count - 1 - i].obj == rmv_obj) movementBuffer.RemoveAt(count - 1 - i);
                        }
                    }
                }
            }
        }

        public void removeFromGame(GameObject obj)
        {
            if (GameEngine.GetInstance().GetState().GetType() == typeof(PlayState))
            {
                lock (removeBuffer)
                {
                    removeBuffer.Add(obj);
                }
            }
        }

        public List<PlayMovement> getBuffer()
        {
            return movementBuffer;
        }
    }
}
