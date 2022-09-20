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
using System.Diagnostics;
using System.Security;

namespace Explorus.Model
{
    public class Slimus : RigidBody
    {
        private int slimeVelocity = 1;
        private int slimeDirX = 0;
        private int slimeDirY = 0;
        private int last_slimeDirX = 0;
        private int last_slimeDirY = -1; //a mettre le defeault à 1 pour quil shoot en haut avant de bouger
        private int last_movement = 0;
        private Movement slimus_movement;
        private Point goalPosition;
        private Dictionary<int, Image> states;
        private Animator animator;
        private ImageLoader slimeloader;

        private Direction direction = new Direction(0, -1);

        private bool readyForInput = true;

        Stopwatch timer = new Stopwatch();
        Stopwatch alphaTimer = new Stopwatch();
        private bool invincible = false;
        private bool transparent = false;

        private PhysicsThread physics = PhysicsThread.GetInstance();

        private CompoundGameObject compoundGameObject;

        public Slimus(Point pos, ImageLoader loader, int ID) : base(pos, loader.SlimusImage, ID)
        {
            states = loader.SlimusImages;
            image = loader.SlimusImage;
            goalPosition = pos;
            slimus_movement = new Movement(slimeVelocity, pos);
            collider = new CircleCollider(this, 39);
            int[] order = { 2, 3, 2, 1 };
            animator = new directionAnimator(states, order);
            slimeloader = loader;
        }

        public int SlimeDirX { get => slimeDirX; set => slimeDirX = value; } 
        public int SlimeDirY { get => slimeDirY; set => slimeDirY = value; }

        public override void processInput()
        {
            Keys input = GetCurrentInput();
            if (readyForInput)
            {
                if(input == Keys.None)
                {
                    direction = new Direction(0, 0);
                }
                else
                {
                    switch (input)
                    {
                        case Keys.Left:
                            direction = new Direction(-1, 0);
                            last_slimeDirX = -1;
                            last_slimeDirY = 0;
                            if (last_slimeDirX == 0 && last_slimeDirY == 0)
                                last_movement = 30;
                            break;

                        case Keys.Right:
                            direction = new Direction(1, 0);
                            last_slimeDirX = 1;
                            last_slimeDirY = 0;
                            if (last_slimeDirX == 0 && last_slimeDirY == 0)
                                last_movement = 10;
                            break;

                        case Keys.Up:
                            direction = new Direction(0, -1);
                            last_slimeDirX = 0;
                            last_slimeDirY = -1;
                            if (last_slimeDirX == 0 && last_slimeDirY == 0)
                                last_movement = 20;
                            break;

                        case Keys.Down:
                            direction = new Direction(0, 1);
                            last_slimeDirX = 0;
                            last_slimeDirY = 1;
                            if (last_slimeDirX == 0 && last_slimeDirY == 0)
                                last_movement = 0;
                            break;

                        default:
                            direction = new Direction(0, 0);
                            break;
                    }
                }
                if (direction.X != 0 || direction.Y != 0)
                {
                    readyForInput = false;
                }

                if(input == Keys.Space)
                {
                    compoundGameObject = Map.Instance.GetCompoundGameObject();
                    GameMaster gameMaster = GameMaster.Instance;
                    Console.WriteLine(gameMaster.getBubbleStatus());
                    if (gameMaster.getBubbleStatus() >= 100)
                    {
                        gameMaster.useBubble();
                        Console.WriteLine("spawn bubble");
                        compoundGameObject.add(new Bubble(this.position, slimeloader, Map.Instance.getID(), this), gridPosition.X, gridPosition.Y);
                    }
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
            if (invincible)
            {
                if (!alphaTimer.IsRunning) alphaTimer.Start();
                else if (alphaTimer.ElapsedMilliseconds >= 300)
                {
                    if (!transparent) transparent = true;
                    else transparent = false;
                    alphaTimer.Restart();
                }
            }
            else if (alphaTimer.IsRunning)
            {
                alphaTimer.Stop();
                alphaTimer.Reset();
                transparent = false;
            }

            if (transparent) image = animator.halfOpacity(image);
        }

        public override void update()
        {  
            objectTypes[,] gridMap = Map.Instance.GetTypeMap();

            objectTypes nextGrid = gridMap[gridPosition.X + direction.X, gridPosition.Y + direction.Y];
            
            //Point newPosition = GetPosition();

            GameMaster gameMaster = GameMaster.Instance;
            //Map oMap = Map.Instance;
            //List<GameObject> compoundGameObjectList = Map.Instance.GetCompoundGameObject().getComponentGameObjetList();

            if (direction.X != 0 || direction.Y != 0)
            {
                if (nextGrid != objectTypes.Wall && (nextGrid != objectTypes.Door || gameMaster.GetKeyStatus())) //Collision
                {
                    if (direction.X + direction.Y > 0 && position.X >= (gridPosition.X + direction.X) * 96 && position.Y >= (gridPosition.Y + direction.Y) * 96)
                    {
                        gridPosition.X += direction.X;
                        gridPosition.Y += direction.Y;

                        direction = new Direction(0, 0);
                        readyForInput = true;
                    }
                    else if (direction.X + direction.Y < 0 && position.X <= (gridPosition.X + direction.X) * 96 && position.Y <= (gridPosition.Y + direction.Y) * 96)
                    {
                        gridPosition.X += direction.X;
                        gridPosition.Y += direction.Y;

                        direction = new Direction(0, 0);
                        readyForInput = true;
                    }
                    else
                    {
                        physics.clearBuffer(this);
                        physics.addMove(new PlayMovement() { obj = this, dir = direction, speed = slimeVelocity });
                        //readyForInput = false;
                    }
                }
                else
                {
                    physics.clearBuffer(this);
                    direction = new Direction(0, 0);
                    readyForInput = true;
                }
                
            }
            else readyForInput = true;

            SetImage();
            checklife();
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
            GameMaster gameMaster = GameMaster.Instance;
            if(otherCollider.parent.GetType() == typeof(ToxicSlime))
            {
                loseLife();
            }
            else if (otherCollider.parent.GetType() == typeof(Gem))
            {
                gameMaster.GemCollected();
                physics.removeFromGame(otherCollider.parent);
            }
            else if (otherCollider.parent.GetType() == typeof(Door))
            {
                physics.removeFromGame(otherCollider.parent);
            }
            else if (otherCollider.parent.GetType() == typeof(Slime))
            {
                physics.removeFromGame(otherCollider.parent);
                gameMaster.rescueSlime();
            }
        }

        public int getSlimeVelocity() { return slimeVelocity; }
        public int getLastSlimeDirX() { return last_slimeDirX; }
        public int getLastSlimeDirY() { return last_slimeDirY; }

        public void loseLife()
        {
            GameMaster gameMaster = GameMaster.Instance;
            if (!timer.IsRunning && !invincible)
            {
                gameMaster.lostLife();
                timer.Start();
                invincible = true;
            }
            else
            {
                if (timer.ElapsedMilliseconds >= 3000)
                {
                    timer.Stop();
                    timer.Reset();
                    invincible = false;
                }
            }
        }

        private void checklife()
        {
            if (timer.IsRunning && invincible)
            {
                if (timer.ElapsedMilliseconds >= 3000)
                {
                    timer.Stop();
                    timer.Reset();
                    invincible = false;
                }
            }
            
        }

    }
}