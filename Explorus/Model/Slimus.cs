using System;
using System.Drawing;
using System.Windows.Forms;

namespace Explorus
{
    public class Slimus : GameObject
    {
        private int slimeVelocity = 5;
        private int slimeDirX = 0;
        private int slimeDirY = 0;

        public Slimus(Point pos) : base(pos, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat))
        {
            position = pos;
        }

        public Slimus(int x, int y) : base(x, y, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 96, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat))
        {

        }

        public override void processInput()
        {
            switch (currentInput)
            {
                case Keys.Left:
                    slimeDirX = -1;
                    slimeDirY = 0;
                    break;

                case Keys.Right:
                    Console.WriteLine("right");
                    slimeDirX = 1;
                    slimeDirY = 0;
                    break;

                case Keys.Up:
                    slimeDirX = 0;
                    slimeDirY = -1;
                    break;

                case Keys.Down:
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

            Point newPosition = GetPosition();

            newPosition.X = (newPosition.X + slimeDirX * slimeVelocity);
            newPosition.Y = (newPosition.Y + slimeDirY * slimeVelocity);
            SetPosition(newPosition);
            position = newPosition;
        }
    }
}