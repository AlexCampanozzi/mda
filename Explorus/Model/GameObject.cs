using System.Drawing;

namespace Explorus
{
    public enum objectTypes
    {
        Wall,
        Key,
        Player,
        Door,
        Empty
    }
    public class GameObject
    {
        Image image;
        Point position;
        public  GameObject(Point pos, Image img)
        {
            position = pos;
            image = img;
        }
        public GameObject(int x, int y, Image img)
        {
            position = new Point(x, y);
            image = img;
        }

        public Point GetPosition()
        {
            return position;
        }

        public void SetPosition(Point pos){
            position = pos;
        }

        public void SetPosition(int x, int y)
        {
            position = new Point(x, y);
        }

        public Image GetImage()
        {
            return image;
        }
    }
}