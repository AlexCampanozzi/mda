using System;
using System.Collections.Generic;
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
        Empty,
        Gem,
        Slime,
    }
    public class GameObject
    {
        Image image;
        protected Point position;   
        protected Point gridPosition;
        private Keys currentInput = Keys.None;


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

        public virtual Image GetImage()
        {
            return image;
        }

        public virtual void SetImage(Image img)
        {
            image = img;
        }
        public Keys GetCurrentInput()
        {
            return this.currentInput;
        }

        public void SetCurrentInput(Keys key)
        {
            currentInput = key;
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

            List<GameObject> compoundGameObjectList = Map.GetInstance().GetCompoundGameObject().getComponentGameObjetList();
            Map oMap = Map.GetInstance();
            for (int i = 0; i < oMap.GetObjectList().Count; i++)
            {
                if (compoundGameObjectList[i] == this)
                {
                    compoundGameObjectList.RemoveAt(i);
                }
            }

            //CompoundGameObject compoundGameObjectList = Map.GetInstance().GetCompoundGameObject();
            //compoundGameObjectList.remove(this);

        }

    }
}