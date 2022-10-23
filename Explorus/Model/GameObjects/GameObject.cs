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
        ToxicSlime,
        Player,
        Door,
        Empty,
        Gem,
        Slime,
        Bubble,
    }

    [Serializable]
    public class GameObject
    {
        protected Collider collider;
        [NonSerialized]
        protected Image image;
        protected Point position;   
        protected Point gridPosition;
        private Keys currentInput = Keys.None;

        private int ID;
        public  GameObject(Point pos, Image img, int iD)
        {
            position = pos;
            image = img;
            ID = iD;
        }

        public int GetID()
        {
            return ID;
        }

        public Collider GetCollider()
        {
            return collider;
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
            CompoundGameObject compoundGameObject = Map.Instance.GetCompoundGameObject();
            compoundGameObject.remove(this);
        }

    }
}