using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorus.Controller;
using Explorus.Threads;

namespace Explorus.Model
{
    public class Bubble : RigidBody
    {
        private Image image;
        private Movement bubble_movement;

        private Slimus slimus;
        private int velocity;
        private int dirX;
        private int dirY;
        private Direction direction;


        private PhysicsThread physics = PhysicsThread.GetInstance();


        public Bubble(Point pos, ImageLoader loader, int ID, Slimus _slimus) : base(pos, loader.BubbleImage, ID)
        {
            collider = new CircleCollider(this, 24);
            slimus = _slimus;
            velocity = slimus.getSlimeVelocity()*2;
            image = loader.BubbleImage;

            dirX = slimus.getLastSlimeDirX();
            dirY = slimus.getLastSlimeDirY();
            direction = new Direction(dirX, dirY);
            Console.WriteLine(dirX + " " + dirY);
        }

        public override void update()
        {

            objectTypes[,] gridMap = Map.Instance.GetTypeMap();
             
            objectTypes nextGrid = gridMap[gridPosition.X + direction.X, gridPosition.Y + direction.Y];

            Point newPosition = GetPosition();

            GameMaster gameMaster = GameMaster.GetInstance();
            Map oMap = Map.Instance;
            List<GameObject> compoundGameObjectList = Map.Instance.GetCompoundGameObject().getComponentGameObjetList();

            if (direction.X != 0 || direction.Y != 0)
            {
                if (nextGrid != objectTypes.Wall && nextGrid != objectTypes.Door) //Collision
                {
                    if (direction.X + direction.Y > 0 && position.X >= (gridPosition.X + direction.X) * 96 && position.Y >= (gridPosition.Y + direction.Y) * 96)
                    {
                        gridPosition.X += direction.X;
                        gridPosition.Y += direction.Y;
                    }
                    else if (direction.X + direction.Y < 0 && position.X <= (gridPosition.X + direction.X) * 96 && position.Y <= (gridPosition.Y + direction.Y) * 96)
                    {
                        gridPosition.X += direction.X;
                        gridPosition.Y += direction.Y;
                    }
                    else
                        physics.addMove(new PlayMovement() { obj = this, dir = direction, speed = velocity });
                }
                else
                {
                    //pop
                }
            }
        }

        public override void OnCollisionEnter(Collider otherCollider)
        {

            if (otherCollider.parent.GetType() == typeof(Wall))
            {
                Console.WriteLine("removed bubble");
                removeItselfFromGame();
            }


        }

    }
}
