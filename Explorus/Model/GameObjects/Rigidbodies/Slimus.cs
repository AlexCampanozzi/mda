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
using Explorus.Threads;

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

        private PhysicsThread physics = PhysicsThread.GetInstance();

        public Slimus(Point pos, ImageLoader loader, int ID) : base(pos, loader.SlimusImage, ID)
        {
            states = loader.SlimusImages;
            image = loader.SlimusImage;
            goalPosition = pos;
            slimus_movement = new Movement(slimeVelocity, pos);
            collider = new CircleCollider(this, 39);
        }

        public int SlimeDirX { get => slimeDirX; set => slimeDirX = value; } 
        public int SlimeDirY { get => slimeDirY; set => slimeDirY = value; }

        public override void processInput()
        {
            Direction direction = new Direction(0,0);
            switch (GetCurrentInput())
            {
                case Keys.Left:
                    direction = new Direction(-1, 0);
                    slimeDirX = -1;
                    slimeDirY = 0;
                    if(last_slimeDirX == 0 && last_slimeDirY == 0)
                        last_movement = 30;
                    break;

                case Keys.Right:
                    direction = new Direction(1, 0);
                    slimeDirX = 1;
                    slimeDirY = 0;
                    if (last_slimeDirX == 0 && last_slimeDirY == 0)
                        last_movement = 10;
                    break;

                case Keys.Up:
                    direction = new Direction(0, -1);
                    slimeDirX = 0;
                    slimeDirY = -1;
                    if (last_slimeDirX == 0 && last_slimeDirY == 0)
                        last_movement = 20;
                    break;

                case Keys.Down:
                    direction = new Direction(0, 1);
                    slimeDirX = 0;
                    slimeDirY = 1;
                    if (last_slimeDirX == 0 && last_slimeDirY == 0)
                        last_movement = 0;
                    break;

                default: // None or other
                    slimeDirX = 0;
                    slimeDirY = 0;
                    break;

            }
            if(direction != null)
            {
                physics.addMove(new PlayMovement() { obj = this, dir = direction, speed = slimeVelocity });
            }
        }

        public override Image GetImage()
        {
            return image;
        }

        private void SetImage()
        {
            int[] state_order = { 2, 3, 2, 1 };
            int temp = Math.Min((int)Math.Floor((position.X % 96.0 + position.Y % 96.0) / 24.0), 3);
            int current_state = state_order[temp];
            image = states[last_movement + current_state];
        }

        private bool checkCollision(Point pos2, int radius2)
        {
            double dist = Math.Sqrt((position.X - pos2.X)* (position.X - pos2.X) + (position.Y - pos2.Y)* (position.Y - pos2.Y));
            //if (dist <= (radius+radius2) )
            //{
            //    return true;
            //}
            //else { return false; }
            return false;
        }

        public override void update()
        {  
            objectTypes[,] gridMap = Map.Instance.GetTypeMap();

            objectTypes nextGrid = gridMap[gridPosition.X + slimeDirX, gridPosition.Y + slimeDirY];
            
            Point newPosition = GetPosition();

            GameMaster gameMaster = GameMaster.GetInstance();
            Map oMap = Map.Instance;
            List<GameObject> compoundGameObjectList = Map.Instance.GetCompoundGameObject().getComponentGameObjetList();

            if (nextGrid == objectTypes.Door && gameMaster.GetKeyStatus())
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
            }

            if (nextGrid != objectTypes.Wall && nextGrid != objectTypes.Door) //Collision
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
            }

            for (int i = 0; i < compoundGameObjectList.Count; i++)
            {
                /*if (compoundGameObjectList[i].GetType() == typeof(Gem) && checkCollision(compoundGameObjectList[i].GetPosition(), compoundGameObjectList[i].getRadius()))
                {
                    gameMaster.GemCollected();
                    compoundGameObjectList[i].removeItselfFromGame();
                }
                if (compoundGameObjectList[i].GetType() == typeof(Slime) && checkCollision(compoundGameObjectList[i].GetPosition(), compoundGameObjectList[i].getRadius()))
                {
                    gameMaster.rescueSlime();
                    compoundGameObjectList[i].removeItselfFromGame();
                    break;
                }
                */
            }
        }
        public override void OnCollisionEnter(Collider otherCollider)
        {
            otherCollider.parent.removeItselfFromGame();
        }
    }
}