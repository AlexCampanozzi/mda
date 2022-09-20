using System;
using System.Drawing;
using System.Windows.Forms;
/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System.Collections.Generic;
using Explorus.Controller;
using Explorus.Model.GameObjects.Rigidbodies;
using Explorus.Threads;
using System.Threading;

namespace Explorus.Model
{
    public class Slimus : RigidBody
    {
        private int slimeVelocity = 2;
        private int slimeDirX = 0;
        private int slimeDirY = 0;
        private int last_slimeDirX = 0;
        private int last_slimeDirY = 0;
        private int last_movement = 0;
        private Movement slimus_movement;
        private Image image;
        private Point goalPosition;
        private Dictionary<int, Image> states;
        private Animator animator;

        private Direction direction = new Direction(0, 0);

        private bool readyForInput = true;

        private PhysicsThread physics = PhysicsThread.GetInstance();

        public Slimus(Point pos, ImageLoader loader, int ID) : base(pos, loader.SlimusImage, ID)
        {
            states = loader.SlimusImages;
            image = loader.SlimusImage;
            goalPosition = pos;
            slimus_movement = new Movement(slimeVelocity, pos);
            collider = new CircleCollider(this, 39);
            int[] order = { 2, 3, 2, 1 };
            animator = new directionAnimator(states, order);
        }

        public int SlimeDirX { get => slimeDirX; set => slimeDirX = value; } 
        public int SlimeDirY { get => slimeDirY; set => slimeDirY = value; }

        public override void processInput()
        {
            if (readyForInput)
            {
                switch (GetCurrentInput())
                {
                    case Keys.Left:
                        direction = new Direction(-1, 0);
                        if (last_slimeDirX == 0 && last_slimeDirY == 0)
                            last_movement = 30;
                        break;

                    case Keys.Right:
                        direction = new Direction(1, 0);
                        if (last_slimeDirX == 0 && last_slimeDirY == 0)
                            last_movement = 10;
                        break;

                    case Keys.Up:
                        direction = new Direction(0, -1);
                        if (last_slimeDirX == 0 && last_slimeDirY == 0)
                            last_movement = 20;
                        break;

                    case Keys.Down:
                        direction = new Direction(0, 1);
                        if (last_slimeDirX == 0 && last_slimeDirY == 0)
                            last_movement = 0;
                        break;

                    case Keys.Space:
                        GameMaster gameMaster = GameMaster.GetInstance();
                        gameMaster.useBubble();
                        break;

                    default:
                        direction = new Direction(0, 0);
                        break;

                }
                if (direction.X != 0 || direction.Y != 0)
                {
                    readyForInput = false;
                }
            }
        }

        public override Image GetImage()
        {
            return image;
        }

        private void SetImage()
        {
            int progress = (int)( position.X % 96.0 + position.Y % 96.0);
            image = animator.Animate(progress, direction.X, direction.Y);
        }

        public override void update()
        {  
            objectTypes[,] gridMap = Map.Instance.GetTypeMap();

            objectTypes nextGrid = gridMap[gridPosition.X + direction.X, gridPosition.Y + direction.Y];
            
            Point newPosition = GetPosition();

            GameMaster gameMaster = GameMaster.GetInstance();
            Map oMap = Map.Instance;
            List<GameObject> compoundGameObjectList = Map.Instance.GetCompoundGameObject().getComponentGameObjetList();

            if(direction.X != 0 || direction.Y != 0)
            {
                if (nextGrid != objectTypes.Wall && (nextGrid != objectTypes.Door || gameMaster.GetKeyStatus())) //Collision
                {
                    if(direction.X + direction.Y > 0 && position.X >= (gridPosition.X + direction.X) * 96 && position.Y >= (gridPosition.Y + direction.Y) * 96)
                    {
                        readyForInput = true;
                        gridPosition.X += direction.X;
                        gridPosition.Y += direction.Y;

                        direction = new Direction(0, 0);
                    }
                    else if(direction.X + direction.Y < 0 && position.X <= (gridPosition.X + direction.X) * 96 && position.Y <= (gridPosition.Y + direction.Y) * 96)
                    {
                        readyForInput = true;
                        gridPosition.X += direction.X;
                        gridPosition.Y += direction.Y;

                        direction = new Direction(0, 0);
                    }
                    else
                        physics.addMove(new PlayMovement() { obj = this, dir = direction, speed = slimeVelocity });
                }
                else
                {
                    direction = new Direction(0, 0);
                    readyForInput = true;
                }
                SetImage();
            }
            /*if (nextGrid == objectTypes.Door && gameMaster.GetKeyStatus())
            {
                for (int i = 0; i < compoundGameObjectList.Count; i++)
                {
                    if (compoundGameObjectList[i].GetType() == typeof(Door))
                    {
                        compoundGameObjectList[i].removeItselfFromGame();
                        oMap.removeObjectFromMap(gridPosition.X + slimeDirX, gridPosition.Y + slimeDirY);
                        gameMaster.useKey();
                        break;
                    }
                }
            }*/

            /*if (nextGrid != objectTypes.Wall && nextGrid != objectTypes.Door) //Collision
            {
                if (goalPosition == position)
                {
                    last_slimeDirX = slimeDirX;
                    last_slimeDirY = slimeDirY;
                    goalPosition.X += SlimeDirX * 96;
                    goalPosition.Y += SlimeDirY * 96;
                }

                (position, gridPosition) = slimus_movement.update(position, gridPosition, last_slimeDirX, last_slimeDirY);
                //SetPosition(position);
                //gridPosition = gridPos;
                SetImage();
            }*/

        
        }
        public override void OnCollisionEnter(Collider otherCollider)
        {
            //otherCollider.parent.removeItselfFromGame();
        }
    }
}