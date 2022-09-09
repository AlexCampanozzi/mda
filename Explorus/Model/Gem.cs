/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System;
using System.Drawing;

namespace Explorus
{
    public class Gem : Collectable
    {
        public Gem(Point pos) : base(pos, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(480, 0, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat))
        {

        }

        public Gem(int x, int y) : base(x, y, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(480, 0, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat))
        {

        }

        public override void update()
        {
            objectTypes[,] gridMap = Map.GetInstance().GetTypeMap();

            if (gridMap[gridPosition.X, gridPosition.Y] == objectTypes.Player)
            {
                Console.WriteLine("deeeleeeete");
            }
        }
    }

}