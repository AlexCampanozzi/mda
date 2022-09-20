/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Explorus.Model;

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
        private bool EndOfGame;

        private int numLevel = 3;
        private int currentLevel = 1;

        private CompoundGameObject compoundGameObject = Map.GetInstance().GetCompoundGameObject();

        private GameMaster()
        {

            foreach (GameObject currentObject in compoundGameObject.getComponentGameObjetList())
            {
                if (currentObject.GetType() == typeof(Gem) || currentObject.GetType() == typeof(ToxicSlime))
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
            if (numberOfGem > 0) return gemCollected * 100 / numberOfGem;
            else return 0;
        }

        public void rescueSlime()
        {
            if (!EndOfLevel && keyStatus)
            {
                EndOfLevel = true;
                if (currentLevel < numLevel) nextLevel();
                else EndOfGame = true;
            }
        }

        public void nextLevel()
        {
            numberOfGem = 0;
            gemCollected = 0;
            keyStatus = false;

            // recreate map
            Map oldmap = Map.GetInstance();
            oldmap.resetMap();
            Thread.Sleep(1000);

            Map map = Map.GetInstance();
            //compoundGameObject = map.GetCompoundGameObject();

            foreach (GameObject currentObject in map.GetObjectList())
            {
                if (currentObject.GetType() == typeof(Gem) || currentObject.GetType() == typeof(ToxicSlime))
                {
                    numberOfGem++;
                }
            }

            currentLevel++;
            if (numberOfGem>0) EndOfLevel = false;
        }

        public int getCurrentLevel()
        {
            return currentLevel;
        }
        public bool isLevelOver()
        {
            return EndOfLevel;
        }

        public bool isGameOver()
        {
            return EndOfGame;
        }
    }
}
