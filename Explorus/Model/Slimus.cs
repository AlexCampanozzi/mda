using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Explorus
{
    public class Slimus : GameObject
    {
        private int slimeVelocity = 1;
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

        public Slimus(Point pos) : base(pos, states[1])
        {
            //position = pos;
            image = states[1];
            goalPosition = pos;
        }

        public Slimus(int x, int y) : base(x, y, states[1])
        {
            image = states[1];
            goalPosition.X = x;
            goalPosition.Y = y;

        }

        public override void processInput()
        {
            switch (currentInput)
            {
                case Keys.Left:
                    Console.WriteLine("left");
                    slimeDirX = -1;
                    slimeDirY = 0;
                    last_movement = 30;
                    break;

                case Keys.Right:
                    Console.WriteLine("right");
                    slimeDirX = 1;
                    slimeDirY = 0;
                    last_movement = 10;
                    break;

                case Keys.Up:
                    Console.WriteLine("up");
                    slimeDirX = 0;
                    slimeDirY = -1;
                    last_movement = 20;
                    break;

                case Keys.Down:
                    Console.WriteLine("down");
                    slimeDirX = 0;
                    slimeDirY = 1;
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
            int[] state_order ={1,2,3,2};
            int current_state = state_order[last_state];
            if (frame_count > 30) 
            {
                last_state += 1;
                if (last_state > 3) { last_state=0; }
                frame_count = 0;
            }

            image = states[last_movement + current_state];
            frame_count +=1; 
        }

        public override void update()
        {
            bool collision = false;
            // Process collisions            

            objectTypes[,] gridMap = Map.GetInstance().typeMap;

            objectTypes nextGrid = gridMap[gridPosition.X + slimeDirX, gridPosition.Y + slimeDirY];
            

            if (nextGrid != objectTypes.Player)
            {
                Console.WriteLine(nextGrid);
            }


            if (nextGrid == objectTypes.Wall)
            {
                collision = true;
            }

            Point newPosition = GetPosition();
            SetImage();

            if (collision == false)
            {
                // mouvements intermédiaire pour voir slimus glisser
                if (goalPosition == newPosition)
                {
                    goalPosition.X = (goalPosition.X + slimeDirX * 96);
                    goalPosition.Y = (goalPosition.Y + slimeDirY * 96);
                    last_slimeDirX = slimeDirX;
                    last_slimeDirY = slimeDirY;
                }
                newPosition.X = (newPosition.X + last_slimeDirX * slimeVelocity);
                newPosition.Y = (newPosition.Y + last_slimeDirY * slimeVelocity);
                SetPosition(newPosition);
                position = newPosition;


                // occur some problems with the speed due to arrondissement
                if(position.X % 96.0 == 0)
                {
                    gridPosition.X = Convert.ToInt32(Math.Floor(position.X / 96.0));
                    
                }
                if(position.Y % 96.0 == 0)
                {
                    gridPosition.Y = Convert.ToInt32(Math.Floor(position.Y / 96.0));
 
                }

            }
            SetGridPosition(gridPosition); //maybe useless since public 

            // process gems logics
            if (gridMap[gridPosition.X, gridPosition.Y] == objectTypes.Key) // IMPORTANT CHANGE TO GEMS
            {
                Console.WriteLine("WAAAAAAAAAAAAAAAAAAAAZZZZZZZZZZZAAAAAAAAAAAAAAAAAAAAAAA");
            }
        }
    }
}