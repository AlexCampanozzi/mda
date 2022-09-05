using System;
using System.Drawing;
using System.Windows.Forms;

namespace Explorus
{
    public class Key : Collectable
    {
        public Key(Point pos) : base(pos, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(528, 0, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat))
        {

        }

        public Key(int x, int y) : base(x, y, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(528, 0, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat))
        {

        }

        public override void update()
        {
            Map oMap = Map.GetInstance();
            objectTypes[,] gridMap = oMap.typeMap;

            for (int i = 0; i < oMap.objectMap.Count; i++)
            {
                if (oMap.objectMap[i].GetType() == typeof(Slimus))
                {
                    //Console.WriteLine(oMap.objectMap[i]);
                }

            }
        }
    }
}