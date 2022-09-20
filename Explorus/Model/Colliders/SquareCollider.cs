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

        public override bool isColliderTouching(Collider otherCollider)
        {
            if(otherCollider is SquareCollider)
            {
                Rectangle myBox = new Rectangle(parent.GetPosition(), getSize());
                Rectangle theirBox = new Rectangle(otherCollider.parent.GetPosition(), ((SquareCollider)otherCollider).getSize());

                if (myBox.IntersectsWith(theirBox))
                    return true;

                return false;
            }
            else if(otherCollider is CircleCollider)
            {
                int distX = Math.Abs(parent.GetPosition().X - otherCollider.parent.GetPosition().X);
                int distY = Math.Abs(parent.GetPosition().Y - otherCollider.parent.GetPosition().Y);

                if (distX > getX() + ((CircleCollider)otherCollider).getRadius())
                    return false;
                if (distY > getY() + ((CircleCollider)otherCollider).getRadius())
                    return false;

                if (distX <= getY() / 2)
                    return true;
                if (distX <= getX() / 2)
                    return true;

                if (Math.Pow(distX - getX() / 2, 2) + Math.Pow(distY - getY() / 2, 2) <= Math.Pow(((CircleCollider)otherCollider).getRadius(), 2))
                    return true;

                return false;
            }
            return false;
        }
    }
}
