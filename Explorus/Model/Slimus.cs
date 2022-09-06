using System;
using System.Drawing;
using System.Windows.Forms;

namespace Explorus
{
    public class Slimus : GameObject
    {
        private int slimeVelocity = 1;
        private int slimeDirX = 0;
        private int slimeDirY = 0;
        public int gemCollected = 0;

        public Slimus(Point pos) : base(pos, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat))
        {
            position = pos;


        }

        public Slimus(int x, int y) : base(x, y, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat))
        {

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
                    break;

                case Keys.Right:
                    Console.WriteLine("right");
                    slimeDirX = 1;
                    slimeDirY = 0;
                    break;

                case Keys.Up:
                    Console.WriteLine("up");
                    slimeDirX = 0;
                    slimeDirY = -1;
                    break;

                case Keys.Down:
                    Console.WriteLine("down");
                    slimeDirX = 0;
                    slimeDirY = 1;
                    break;

                default: // None or other
                    slimeDirX = 0;
                    slimeDirY = 0;
                    break;
            }
        }

        public override void update()
        {
            bool collision = false;
            // Process collisions            

            objectTypes[,] gridMap = Map.GetInstance().GetTypeMap();

            objectTypes nextGrid = gridMap[gridPosition.X + slimeDirX, gridPosition.Y + slimeDirY];


            if (nextGrid != objectTypes.Player)
            {
                //Console.WriteLine(nextGrid);
            }


            if (nextGrid == objectTypes.Wall)
            {
                collision = true;
            }

            Point newPosition = GetPosition();

            if (collision == false)
            {
                newPosition.X = (newPosition.X + slimeDirX * slimeVelocity);
                newPosition.Y = (newPosition.Y + slimeDirY * slimeVelocity);
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

            Map oMap = Map.GetInstance();
            // process gems logics
            for (int i = 0; i < oMap.GetObjectList().Count; i++)
            {
                if ((oMap.GetObjectList()[i].GetType() == typeof(Key)) && (oMap.GetObjectList()[i].GetPosition() == position))
                {
                    gemCollected += 1;
                    oMap.GetObjectList()[i].removeItselfFromGame();
                    Console.WriteLine(gemCollected);
                    break;
                }
            }
        }
    }
}