using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Explorus
{
    public class CompoundGameObject
    {
        private List<GameObject> componentList = new List<GameObject>();

        public void add(GameObject currentObject,int x, int y)
        {
            componentList.Add(currentObject);
            currentObject.SetGridPosition(new Point(x, y));
            Console.WriteLine(currentObject.GetType());

        }
        public void remove(GameObject toBeDeletedObject) //not working
        {
            foreach (GameObject currentObject in componentList)
            {
                if (currentObject == toBeDeletedObject)
                {
                    componentList.Remove(currentObject);
                }
            }
        }

        public void update(Keys currentInput) //not working
        {
            foreach (GameObject currentObject in componentList)
            {
                currentObject.SetCurrentInput(currentInput);
                currentObject.update();
            }
        }

        public void processInput()
        {
            foreach(GameObject currentObject in componentList)
            {
                currentObject.processInput();
            }
        }

        public List<GameObject> getComponentGameObjetList()
        {
            return componentList;
        }
    }
}
