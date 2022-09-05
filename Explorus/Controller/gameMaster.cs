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
        private int numberOfKey = 0;

        private GameMaster()
        {
            for (int i = 0; i < oMap.objectMap.Count; i++)
            {
                if (oMap.objectMap[i].GetType() == typeof(Key))
                {
                    numberOfKey++;
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
            
            for (int i = 0; i < oMap.objectMap.Count; i++)
            {
                if (oMap.objectMap[i].GetType() == typeof(Slimus))
                {
                    if (((Slimus) oMap.objectMap[i]).gemCollected == numberOfKey)
                    {
                        openDoor();
                    }
                }
            }


        }

        private void openDoor()
        {

        }

        private void triggerLevelComplete()
        {

        }
    }
}
