﻿using System;
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
//using System.Drawing;
using Explorus.Threads;
using System.Windows.Media;
using Explorus.Model.Behavior;
using System.Diagnostics;

namespace Explorus.Model
{
    public class ToxicSlime : RigidBody
    {
        private Animator animator;

        private int last_slimeDirX = 0;
        private int last_slimeDirY = 0;

        private Dictionary<int, Image> states;

        private PhysicsThread physics = PhysicsThread.GetInstance();

        protected new Direction direction;

        private int slimeVelocity = 1;

        private int life = 2;

        private ImageLoader iloader;

        CompoundGameObject compoundGameObject;

        private AudioThread audio = AudioThread.Instance;

        private BehaviorContext context;
        protected int lastPlayerPosX, lastPlayerPosY;
        protected Direction lastPlayerDir;
        public Stopwatch behaviorTimer = new Stopwatch();
        protected bool pursuit;

        //Map map = Map.GetInstance();
        public ToxicSlime(Point pos, ImageLoader loader, int ID) : base(pos, loader.ToxicSlimeImage, ID)
        {
            int[] order = { 2, 3, 2, 1 };
            collider = new CircleCollider(this, 39);
            states = loader.ToxicSlimeImages;
            animator = new directionAnimator(states, order);
            iloader = loader;
            context = new BehaviorContext(this, new DualStrategy());

        }
        private void SetImage()
        {
            int progress = (int)(position.X % 96.0 + position.Y % 96.0);
            lock (image)
            {
                image = animator.Animate(progress, direction.X, direction.Y);
            }
        }


        public (int, int, Direction) getLastPlayerInfo()
        {
            return (lastPlayerPosX, lastPlayerPosY, lastPlayerDir);
        }

        private void setLastPlayerInfo(int posX, int posY, Direction dir)
        {
            lastPlayerDir = dir;
            lastPlayerPosX = posX;
            lastPlayerPosY = posY;
        }

        public Direction GetDirection()
        {
            return direction;
        }
        public bool getPursuit()
        {
            return pursuit;
        }
        public void setPursuit(bool pursuitMode)
        {
            pursuit = pursuitMode;
        }
        public override void update()
        {
            if(direction == null || (direction.X == 0 && direction.Y ==0))
            {
                (direction, _, _, _) = context.choosePath();
                last_direction = direction;
            }
            objectTypes[,] gridMap = Map.Instance.GetTypeMap();

            objectTypes nextGrid = gridMap[gridPosition.X + direction.X, gridPosition.Y + direction.Y];

            Point newPosition = GetPosition();

            GameMaster gameMaster = GameMaster.Instance;
            Map oMap = Map.Instance;

            if (GameEngine.GetInstance().GetState().GetType() == typeof(PlayState))
            {
                // 3 cases
                // special, lost, random
                //pursuit
                // follow & follow around corner (special)
                // dual
                // follow & follow around and run away
                // ambush
                // parralelle
                if (direction.X != 0 || direction.Y != 0)
                {
                    if (direction.X + direction.Y > 0 && position.X >= (gridPosition.X + direction.X) * 96 && position.Y >= (gridPosition.Y + direction.Y) * 96)
                    {
                        gridPosition.X += direction.X;
                        gridPosition.Y += direction.Y;
                        (direction, lastPlayerPosX, lastPlayerPosY, lastPlayerDir) = context.choosePath();
                        last_direction = direction;
                    }
                    else if (direction.X + direction.Y < 0 && position.X <= (gridPosition.X + direction.X) * 96 && position.Y <= (gridPosition.Y + direction.Y) * 96)
                    {
                        gridPosition.X += direction.X;
                        gridPosition.Y += direction.Y;
                        (direction, lastPlayerPosX, lastPlayerPosY, lastPlayerDir) = context.choosePath();
                        last_direction = direction;
                    }

                    physics.clearBuffer(this);
                    physics.addMove(new PlayMovement() { obj = this, dir = direction, speed = slimeVelocity });

                    SetImage();
                }
                //if (direction.X != 0 || direction.Y != 0)
                //{
                //if (nextGrid != objectTypes.Wall && nextGrid != objectTypes.Door)) //Collision
                //{
                //    if (direction.X + direction.Y > 0 && position.X >= (gridPosition.X + direction.X) * 96 && position.Y >= (gridPosition.Y + direction.Y) * 96)
                //    {
                //        gridPosition.X += direction.X;
                //        gridPosition.Y += direction.Y;
                //    }
                //    else if (direction.X + direction.Y < 0 && position.X <= (gridPosition.X + direction.X) * 96 && position.Y <= (gridPosition.Y + direction.Y) * 96)
                //    {
                //        gridPosition.X += direction.X;
                //        gridPosition.Y += direction.Y;
                //    }
                //    else
                //    {
                //    }
                //}
                //else
                //{
                //    physics.clearBuffer(this);
                //    //newRndDir();
                //    direction = context.choosePath();
                //}

                //}
            }
        }

        public void newRndDir()
        {
            Random rnd = new Random();
            int newDirID = rnd.Next(0, 3);
            Direction newDir = new Direction(0, 1);
            switch (newDirID)
            {
                case 0:
                    newDir = new Direction(0, 1);
                    break;
                case 1:
                    newDir = new Direction(1, 0);
                    break;
                case 2:
                    newDir = new Direction(0, -1);
                    break;
                case 3:
                    newDir = new Direction(-1, 0);
                    break;
            }
            direction = newDir;
        }

        public void invertDir()
        {
            direction.X = direction.X * -1;
            direction.Y = direction.Y * -1;
            audio.addSound(Sound.soundDirectionChange);
        }

        public override void OnCollisionEnter(Collider otherCollider)
        {
            if(otherCollider.parent.GetType() == typeof(ToxicSlime))
            {
                ((ToxicSlime)otherCollider.parent).invertDir();
                this.invertDir();
                audio.addSound(Sound.soundHit);
            }
            else if (otherCollider.parent.GetType() == typeof(Door))
            {
                physics.removeFromGame(otherCollider.parent);
            }
            else if (otherCollider.parent.GetType() == typeof(Slimus))
            {
                ((Slimus)otherCollider.parent).loseLife();
            }
            /*else if(otherCollider.parent.GetType() == typeof(Bubble))
            {
                loseLife();
            }*/
        }

        public void loseLife()
        {
            life--;
            if (life <= 0)
            {
                physics.removeFromGame(this);
                physics.clearBuffer(this);
                compoundGameObject = Map.Instance.GetCompoundGameObject();
                compoundGameObject.add(new Gem(this.position, iloader, Map.Instance.getID()), gridPosition.X, gridPosition.Y);

                audio.addSound(Sound.soundDead);
            }
        }
    }
}
