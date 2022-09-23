/*
 * RENDERTHREAD = (run -> RENDERTHREAD). 
 */

using Explorus.Controller;
using Explorus.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace Explorus.Threads
{

     public sealed class RenderThread
    {
        private static RenderThread instance = null;
        private static readonly object padlock = new object();

        GameView gameView = GameView.Instance;


        private RenderThread()
        {
        }

        public static RenderThread GetInstance()
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new RenderThread();
                    }
                }
            }
            return instance;
        }

        
        public void Run()
        {
            while (true)
            {
                gameView.Render();
                Thread.Sleep(16);
            }

        }

    }
}
