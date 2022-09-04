namespace Explorus
{
    public class GameObject
    {
        Image2D image;
        Position position;
        public  GameObject(Position pos, Image2D img)
        {
            position = pos;
            image = img;
        }
        public GameObject(int x, int y, Image2D img)
        {
            position = new Position(x, y);
            image = img;
        }

        public Position GetPosition()
        {
            return position;
        }

        public void SetPosition(Position pos){
            position = pos;
        }

        public void SetPosition(int x, int y)
        {
            position = new Position(x, y);
        }

        public void SetImage(Image2D img)
        {
            image = img;
        }
    }
}