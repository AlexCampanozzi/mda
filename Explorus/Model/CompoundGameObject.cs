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
            for (int i = 0; i < componentList.Count; i++)
            {
                if (componentList[i] == toBeDeletedObject)
                {
                    componentList.RemoveAt(i);
                }
            }
        }

        public void update(Keys currentInput) //not working
        {

        for (int i = 0; i < componentList.Count; i++)
            {
                componentList[i].SetCurrentInput(currentInput); //list of game objects
                componentList[i].update();
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
