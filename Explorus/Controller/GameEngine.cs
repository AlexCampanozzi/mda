using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Forms;

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
            long previousTime = getTime();
            double lag = 0.0;
            int MS_PER_UPDATE = 60;

            while (true)
            {
                long currentTime = getTime();
                long elapsed = currentTime - previousTime;
                previousTime = currentTime;
                double fps = 1.0 / (elapsed / 1000.0);
                oView.setFPS(fps);


                lag += elapsed;

                processInput();

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

        private void processInput()
        {
            Keys currentInput = oView.getCurrentInput();

            switch (currentInput)
            {
                case Keys.R:
                    Console.WriteLine("Resume");
                    break;

                case Keys.P:
                    Console.WriteLine("Pause");
                    break;

                case Keys.Left:
                    Console.WriteLine("Left");
                    break;

                case Keys.Right:
                    Console.WriteLine("Right");
                    break;

                case Keys.Up:
                    Console.WriteLine("Up");
                    break;

                case Keys.Down:
                    Console.WriteLine("Down");
                    break;

                default:
                    break;
            }
        }

    }
}
