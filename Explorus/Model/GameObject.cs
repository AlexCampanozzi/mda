using System;
using System.Drawing;
using System.Windows.Forms;

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
        protected Point position;   
        public Point gridPosition;

        public Keys currentInput = Keys.None;
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

        public Point GetGridPosition()
        {
            return gridPosition;
        }

        public void SetGridPosition(Point pos)
        {
            gridPosition = pos;
        }


        public void SetPosition(int x, int y)
        {
            position = new Point(x, y);
        }

        public Image GetImage()
        {
            return image;
        }

        public virtual void update()
        {
            // TODO
        }

        public virtual void processInput()
        {

        }

        public void removeItselfFromGame()
        {
            Map oMap = Map.GetInstance();
            for (int i = 0; i < oMap.objectMap.Count; i++)
            {
                if (oMap.objectMap[i] == this)
                {
                    oMap.objectMap.RemoveAt(i);
                }
            }
        }

    }
}