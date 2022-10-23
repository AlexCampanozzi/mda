﻿/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Explorus.Model;
using Explorus.Threads;

namespace Explorus.Controller
{
    public sealed class GameMaster
    {
        private static readonly GameMaster instance = new GameMaster();
        private static readonly object padlock = new object();

        private int numberOfGem = 0;

        private bool keyStatus = false;
        private int gemCollected = 0;

        //private int lifeStatus = 6;
        private int lifeStatus = 1;
        private int bubbleStatus = 6;

        private bool EndOfLevel;
        Stopwatch timer = new Stopwatch();
        private bool EndOfGame;

        private int numLevel = 3;
        private int currentLevel = 1;

        private bool hasSlimusDied = false;

        private GameView oView = GameView.Instance;

        private CompoundGameObject compoundGameObject = Map.Instance.GetCompoundGameObject();

        private GameMaster()
        {
            
            GameEngine engine = GameEngine.GetInstance();
        }
        static GameMaster()
        {

        }
        

        public static GameMaster Instance
        {
            get
            {
                return instance;
            }
        }

        public bool HasSlimusDied { get => hasSlimusDied; }

        public void update()
        {
            if (gemCollected == numberOfGem)
            {
                if (keyStatus != true)
                {
                    keyStatus = true;
                    oView.getHeader().setKey(keyStatus);
                }
            }
            getBubbleStatus();
        }

        public bool GetKeyStatus()
        {
            return keyStatus;
        }

        public void useKey()
        {
            keyStatus = false;
            oView.getHeader().setKey(keyStatus);
        }

        public void GemCollected()
        {
            gemCollected++;
            oView.getHeader().setGem(gemCollected * 100 / numberOfGem);
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
            Map oldmap = Map.Instance;
            oldmap.resetMap();
            Thread.Sleep(1000);

            Map map = Map.Instance;
            //compoundGameObject = map.GetCompoundGameObject();

            foreach (GameObject currentObject in map.GetObjectList())
            {
                if (currentObject.GetType() == typeof(Gem) || currentObject.GetType() == typeof(ToxicSlime))
                {
                    numberOfGem++;
                }
            }

            currentLevel++;
            if (numberOfGem > 0) EndOfLevel = false;
        }

        public void resetLevel()
        {
            hasSlimusDied = false;
            lifeStatus = 6;
            numberOfGem = 0;
            gemCollected = 0;
            keyStatus = false;
            EndOfGame = false;

            oView.getHeader().setLife(lifeStatus * 100 / 6);

            // recreate map
            Map oldmap = Map.Instance;
            oldmap.resetMap();
            Thread.Sleep(1000);

            Map map = Map.Instance;
            //compoundGameObject = map.GetCompoundGameObject();

            foreach (GameObject currentObject in map.GetObjectList())
            {
                if (currentObject.GetType() == typeof(Gem) || currentObject.GetType() == typeof(ToxicSlime))
                {
                    numberOfGem++;
                }
            }

            currentLevel = 0;
            if (numberOfGem > 0) EndOfLevel = false;
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

        public void lostLife()
        {
            lifeStatus--;
            oView.getHeader().setLife(lifeStatus * 100 / 6);
            if (lifeStatus == 0)
            {
                EndOfGame = true;
                PhysicsThread physics = PhysicsThread.GetInstance();
                physics.resetBuffers();
                hasSlimusDied = true;
            }
                
        }
        public int getLifeStatus()
        {
            return lifeStatus * 100 / 6;
        }

        public void setSlimeAmount(int slimes)
        {
            numberOfGem = slimes;
        }

        public void useBubble()
        {
            if (bubbleStatus == 6) bubbleStatus -= 6;
            oView.getHeader().setBubble(bubbleStatus);
        }

        public int getBubbleStatus()
        {
            if (bubbleStatus < 6)
            {
                if (!timer.IsRunning) timer.Start();
                else
                {
                    if (timer.ElapsedMilliseconds >= 500)
                    {
                        timer.Stop();
                        bubbleStatus++;
                        timer.Reset();

                        oView.getHeader().setBubble(bubbleStatus * 100 / 6);
                    }
                }
            }
            return bubbleStatus * 100 / 6;
        }
    }
}
