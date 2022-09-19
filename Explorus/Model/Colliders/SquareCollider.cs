using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Explorus.Model
{
    public class SquareCollider : Collider
    {
        int sizeX;
        int sizeY;

        public SquareCollider(GameObject parent, int Xsize, int Ysize):base(parent)
        {
            sizeX = Xsize;
            sizeY = Ysize;
        }

        public int getX()
        {
            return sizeX;
        }

        public int getY()
        {
            return sizeY;
        }

        public Size getSize()
        {
            return new Size(sizeX, sizeY);
        }

        public bool isColliderTouching(SquareCollider otherCollider)
        {
            Rectangle myBox = new Rectangle(parent.GetPosition(), getSize());
            Rectangle theirBox= new Rectangle(otherCollider.parent.GetPosition(), otherCollider.getSize());

            if (myBox.IntersectsWith(theirBox))
                return true;

            return false;
        }

        public bool isColliderTouching(CircleCollider otherCollider)
        {
            int distX = Math.Abs(parent.GetPosition().X - otherCollider.parent.GetPosition().X);
            int distY = Math.Abs(parent.GetPosition().Y - otherCollider.parent.GetPosition().Y);

            if (distX > getX() + otherCollider.getRadius())
                return false;
            if (distY > getY() + otherCollider.getRadius())
                return false;

            if (distX <= getY() / 2)
                return true;
            if (distX <= getX() / 2)
                return true;

            if (Math.Exp(distX - getX() / 2) + Math.Exp(distY - getY() / 2) <= Math.Exp(otherCollider.getRadius()))
                return true;

            return false;
        }
    }
}
