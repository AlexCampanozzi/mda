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
        private Animator animator;

        private static readonly Dictionary<int, Image> states = new Dictionary<int, Image>()
        {
            {1, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {2, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(96, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {3, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(192, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {11, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(288, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {12, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(384, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {13, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(480, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {21, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 192, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {22, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(96, 192, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {23, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(192, 192, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {31, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(288, 192, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {32, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(384, 192, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {33, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(480, 192, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
        };
        public Slimus(Point pos, ImageLoader loader) : base(pos, loader.SlimusImage)
        {
            //image = states[1];
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
            switch (GetCurrentInput())
            {
                case Keys.Left:
                    slimeDirX = -1;
                    slimeDirY = 0;
                    if(last_slimeDirX == 0 && last_slimeDirY == 0)
                        last_movement = 30;
                    break;

                case Keys.Right:
                    slimeDirX = 1;
                    slimeDirY = 0;
                    if (last_slimeDirX == 0 && last_slimeDirY == 0)
                        last_movement = 10;
                    break;

                case Keys.Up:
                    slimeDirX = 0;
                    slimeDirY = -1;
                    if (last_slimeDirX == 0 && last_slimeDirY == 0)
                        last_movement = 20;
                    break;

                case Keys.Down:
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
        }

        public override Image GetImage()
        {
            return image;
        }

        private void SetImage()
        {
            int progress = (int)( position.X % 96.0 + position.Y % 96.0);
            image = animator.Animate(progress, last_slimeDirX, last_slimeDirY);
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
            objectTypes[,] gridMap = Map.GetInstance().GetTypeMap();

            objectTypes nextGrid = gridMap[gridPosition.X + slimeDirX, gridPosition.Y + slimeDirY];
            
            Point newPosition = GetPosition();

            GameMaster gameMaster = GameMaster.GetInstance();
            Map oMap = Map.GetInstance();
            List<GameObject> compoundGameObjectList = Map.GetInstance().GetCompoundGameObject().getComponentGameObjetList();

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
    }
}