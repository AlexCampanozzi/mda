using System.Drawing;

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
            //mettre la gestion de collisiotn de clé vs. slime ici, donc la clé va gérer sa propre suppression (peut être. a voir)
        }
}