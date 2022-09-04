using System.Drawing;

namespace Explorus
{
    public class Collectable : GameObject
    {
        public Collectable(Point pos, Image img) : base(pos, img)
        {

        }

        public Collectable(int x, int y, Image img) : base(x, y, img)
        {

        }
    }
}