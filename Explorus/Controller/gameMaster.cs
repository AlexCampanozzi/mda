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
        private int gemCollected = 0;

        private bool EndOfLevel;

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
            if (gemCollected == numberOfGem)
            {
                keyStatus = true;
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

        public void GemCollected()
        {
            gemCollected++;
        }

        public int getGemStatus()
        {
            return gemCollected * 100 / numberOfGem;
        }

        public void rescueSlime()
        {
            EndOfLevel = true;
        }

        public bool isLevelOver()
        {
            return EndOfLevel;
        }
    }
}
