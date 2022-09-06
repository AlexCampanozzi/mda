using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Controller
{
    public sealed class GameMaster
    {
        private static GameMaster instance = null;
        private static readonly object padlock = new object();

        private Map oMap = Map.GetInstance();
        private int numberOfGem = 0;

        private bool keyStatus = false;
        private int gemStatus = 0;

        private GameMaster()
        {
            for (int i = 0; i < oMap.GetObjectList().Count; i++)
            {
                if (oMap.GetObjectList()[i].GetType() == typeof(Gem))
                {
                    numberOfGem++;
                }
            }
        }

        public static GameMaster GetInstance()
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GameMaster();
                    }
                }
            }
            return instance;
        }

        public void update()
        {
            
            for (int i = 0; i < oMap.GetObjectList().Count; i++)
            {
                if (oMap.GetObjectList()[i].GetType() == typeof(Slimus))
                {
                    gemStatus = ((Slimus)oMap.GetObjectList()[i]).gemCollected * 100 / numberOfGem;
                    if (((Slimus) oMap.GetObjectList()[i]).gemCollected == numberOfGem)
                    {
                        keyStatus = true;
                        
                    }
                }
            }


        }

        public bool GetKeyStatus()
        {
            return keyStatus;
        }

        public void useKey()
        {
            keyStatus = false;
        }

        public int getGemStatus()
        {
            return gemStatus;
        }

        public void rescueSlime()
        {

        }
        private void triggerLevelComplete()
        {

        }
    }
}
