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

        private int lifeStatus = 6;
        private int bubbleStatus = 6;

        private bool EndOfLevel;
        Stopwatch timer = new Stopwatch();

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

        public void lostLife()
        {
            lifeStatus--;
            if (lifeStatus == 0) EndOfLevel = true;
        }
        public int getLifeStatus()
        {
            return lifeStatus * 100 / 6;
        }

        public void useBubble()
        {
            if(bubbleStatus>0) bubbleStatus--;
        }

        public void generateBubble()
        {
            bubbleStatus++;
        }

        public int getBubbleStatus()
        {
            if (bubbleStatus < 6)
            {
                if (!timer.IsRunning) timer.Start();
                else
                {
                    if(timer.ElapsedMilliseconds >= 500)
                    {
                        timer.Stop();
                        generateBubble();
                        timer.Reset();
                    }
                }
            }
            return bubbleStatus * 100 / 6;
        }
    }
}
