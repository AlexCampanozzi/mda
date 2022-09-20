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
    public class CompoundGameObject
    {
        private List<GameObject> componentList = new List<GameObject>();

        public void add(GameObject currentObject,int x, int y)
        {
            componentList.Add(currentObject);
            currentObject.SetGridPosition(new Point(x, y));
        }

        public void remove(GameObject toBeDeletedObject)
        {
            for (int i = 0; i < componentList.Count; i++)
            {
                if (componentList[i] == toBeDeletedObject)
                {
                    componentList.RemoveAt(i);
                }
            }
        }

        public void update(Keys currentInput)
        {
            for (int i = 0; i < componentList.Count; i++)
            {
                componentList[i].SetCurrentInput(currentInput);
                componentList[i].update();
            }
        }

        public void processInput()
        {
            for(int i = 0; i < componentList.Count; i++)
            {
                componentList[i].processInput();
            }
        }

        public List<GameObject> getComponentGameObjetList()
        {
            return componentList;
        }
    }
}
