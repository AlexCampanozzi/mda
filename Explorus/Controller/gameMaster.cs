using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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

        public void checkGems(Point gridPosition)
        {
            oMap = Map.GetInstance();
            for (int i = 0; i < oMap.GetObjectList().Count; i++)
            {
                if(oMap.GetObjectList()[i].GetGridPosition() == gridPosition)
                {
                    if (oMap.GetObjectList()[i].GetType() == typeof(Gem))
                    {
                        gemCollected++;
                        oMap.GetObjectList()[i].removeItselfFromGame();
                        break;
                    }
                    else if (oMap.GetObjectList()[i].GetType() == typeof(Slime))
                    {
                        oMap.GetObjectList()[i].removeItselfFromGame();
                        rescueSlime();
                    }
                }

            }
        }

        public void checkDoor(int collisionx, int collisiony)
        {
            oMap = Map.GetInstance();
            if (keyStatus)
            {
                for (int i = 0; i < oMap.GetObjectList().Count; i++)
                {
                    if (oMap.GetObjectList()[i].GetType() == typeof(Door))
                    {
                        oMap.GetObjectList()[i].removeItselfFromGame();
                        oMap.removeObjectFromMap(collisionx, collisiony);
                        useKey();
                        break;
                    }
                }
            }
        }

        public bool GetKeyStatus()
        {
            return keyStatus;
        }

        private void useKey()
        {
            keyStatus = false;
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
