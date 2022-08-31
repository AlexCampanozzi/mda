using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Explorus
{
    public class GameEngine
    {
        private GameView oView;
        private Thread thread;

        public GameEngine()
        {
            oView = new GameView();
            Thread thread = new Thread(new ThreadStart(GameLoop));
            thread.Start();
            oView.Show();
        }

        private void GameLoop()
        {
            //oView.Render();
            //Thread.Sleep(5000);
            //oView.Close();
            long previousTime = getTime();
            double lag = 0.0;
            int MS_PER_UPDATE = 60;

            while (true)
            {
                long currentTime = getTime();
                long elapsed = currentTime - previousTime;
                System.Console.WriteLine(elapsed);
                previousTime = currentTime;
                double fps = 1.0 / (elapsed/1000.0);
                oView.setFPS(fps);


                lag += elapsed;

                //processInput();

                while (lag >= MS_PER_UPDATE)
                {
                    //update();
                    lag -= MS_PER_UPDATE;
                }

                oView.Render();
                Thread.Sleep(1);
            }
        }

        private long getTime()
        {
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            return milliseconds;
        }
    }
}
