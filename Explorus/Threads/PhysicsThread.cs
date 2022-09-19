using Explorus.Model;
using System;
using System.Collections.Generic;
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
        List<PlayMovement> movementBuffer;
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
                    moveObject(movementBuffer.First());
                    movementBuffer.RemoveAt(0);
                }

            }
        }

        private void moveObject(PlayMovement movement)
        {
            movement.obj.TryMove(dir, speed);
        }
    }
}
