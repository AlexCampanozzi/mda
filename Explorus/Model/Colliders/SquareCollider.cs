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

        public SquareCollider(GameObject parent, int X, int Y):base(parent)
        {
            sizeX = X;
            sizeY = Y;
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
            // TODO : Calculer quand les colliders s'intersectent
            
            return false;
        }
    }
}
