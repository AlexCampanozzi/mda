/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Explorus.Model
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

        }

        public virtual void processInput()
        {

        }

        public void removeItselfFromGame()
        {
            CompoundGameObject compoundGameObject = Map.GetInstance().GetCompoundGameObject();
            compoundGameObject.remove(this);
        }

    }
}