using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Explorus.Controller;

namespace Explorus
{
    public class Slimus : GameObject
    {
        private int slimeVelocity = 6;
        private int slimeDirX = 0;
        private int slimeDirY = 0;
        private int last_slimeDirX = 0;
        private int last_slimeDirY = 0;
        private int last_state = 0;
        private int frame_count = 0;
        private int last_movement = 0;
        private Image image;
        private Point goalPosition;
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
        public int gemCollected = 0;
        public Slimus(Point pos) : base(pos, states[1])
        {
            image = states[1];
            goalPosition = pos;
        }

        public Slimus(int x, int y) : base(x, y, states[1])
        {
            image = states[1];
            goalPosition.X = x;
            goalPosition.Y = y;

        }

        public int SlimeDirX { get => slimeDirX; set => slimeDirX = value; } 
        public int SlimeDirY { get => slimeDirY; set => slimeDirY = value; }

        public override void processInput()
        {
            switch (GetCurrentInput())
            {
                case Keys.Left:
                    Console.WriteLine("left");
                    slimeDirX = -1;
                    slimeDirY = 0;
                    if(last_slimeDirX == 0 && last_slimeDirY == 0)
                        last_movement = 30;
                    break;

                case Keys.Right:
                    Console.WriteLine("right");
                    slimeDirX = 1;
                    slimeDirY = 0;
                    if (last_slimeDirX == 0 && last_slimeDirY == 0)
                        last_movement = 10;
                    break;

                case Keys.Up:
                    Console.WriteLine("up");
                    slimeDirX = 0;
                    slimeDirY = -1;
                    if (last_slimeDirX == 0 && last_slimeDirY == 0)
                        last_movement = 20;
                    break;

                case Keys.Down:
                    Console.WriteLine("down");
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
            int[] state_order = { 1, 2, 3, 2 };
            int temp = (int)Math.Floor((position.X % 96.0 + position.Y % 96.0) / 24.0);
            int current_state = state_order[temp];
            image = states[last_movement + current_state];
        }

        public override void update()
        {
            bool collision = false;
            // Process collisions            

            objectTypes[,] gridMap = Map.GetInstance().GetTypeMap();

            objectTypes nextGrid = gridMap[gridPosition.X + slimeDirX, gridPosition.Y + slimeDirY];
            
            Point newPosition = GetPosition();

            GameMaster gameMaster = GameMaster.GetInstance();
            Map oMap = Map.GetInstance();

            if (nextGrid == objectTypes.Door && gameMaster.GetKeyStatus())
            {
                for (int i = 0; i < oMap.GetObjectList().Count; i++)
                {
                    if (oMap.GetObjectList()[i].GetType() == typeof(Door))
                    {
                        oMap.GetObjectList()[i].removeItselfFromGame();
                        oMap.removeObjectFromMap(gridPosition.X + slimeDirX, gridPosition.Y + slimeDirY);
                        gameMaster.useKey();
                        //nextGrid = objectTypes.Empty;
                        break;
                    }
                }
            }

            if (nextGrid != objectTypes.Wall && nextGrid != objectTypes.Door) //Collision
            {
                
                // mouvements intermédiaire pour voir slimus glisser
                if (goalPosition == newPosition)
                {
                    goalPosition.X = (goalPosition.X + slimeDirX * 96);
                    goalPosition.Y = (goalPosition.Y + slimeDirY * 96);
                    last_slimeDirX = slimeDirX;
                    last_slimeDirY = slimeDirY;
                }

                if(last_slimeDirX != 0 || last_slimeDirY != 0)
                {
                    SetImage();
                }

                newPosition.X = (newPosition.X + last_slimeDirX * slimeVelocity);
                newPosition.Y = (newPosition.Y + last_slimeDirY * slimeVelocity);


                SetPosition(newPosition);
                position = newPosition;

                if(last_slimeDirX == 1)
                {
                    if(position.X >= ((gridPosition.X + last_slimeDirX) * 96.0))
                    {
                        gridPosition.X = gridPosition.X + last_slimeDirX;
                        position.X = gridPosition.X * 96;
                    }
                }
                else if(last_slimeDirX == -1)
                {
                    if (position.X <= ((gridPosition.X + last_slimeDirX) * 96.0))
                    {
                        gridPosition.X = gridPosition.X + last_slimeDirX;
                        position.X = gridPosition.X * 96;
                    }
                }
                else if(last_slimeDirY == 1)
                {
                    if (position.Y >= ((gridPosition.Y + last_slimeDirY) * 96.0))
                    {
                        gridPosition.Y = gridPosition.Y + last_slimeDirY;
                        position.Y = gridPosition.Y * 96;
                    }
                }
                else if (last_slimeDirY == -1)
                {
                    if (position.Y <= ((gridPosition.Y + last_slimeDirY) * 96.0))
                    {
                        gridPosition.Y = gridPosition.Y + last_slimeDirY;
                        position.Y = gridPosition.Y * 96;
                    }
                }

            }

            // process gems logics
            for (int i = 0; i < oMap.GetObjectList().Count; i++)
            {
                if ((oMap.GetObjectList()[i].GetType() == typeof(Gem)) && (oMap.GetObjectList()[i].GetGridPosition() == gridPosition))
                {
                    gemCollected += 1;
                    oMap.GetObjectList()[i].removeItselfFromGame();
                    break;
                }
                if ((oMap.GetObjectList()[i].GetType() == typeof(Slime)) && (oMap.GetObjectList()[i].GetGridPosition() == gridPosition))
                {
                    oMap.GetObjectList()[i].removeItselfFromGame();
                    gameMaster.rescueSlime();
                }

            }
        }
    }
}