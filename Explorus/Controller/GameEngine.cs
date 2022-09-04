using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing.Text;

enum gameState
{
    Resumed,
    Paused,
}


namespace Explorus
{
    public class GameEngine
    {
        private GameView oView;
        private int slimeVelocity = 5;
        private int slimeDirX = 0;
        private int slimeDirY = 0;
        private gameState currentGameState;

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
            int MS_PER_UPDATE = 10;

            while (true)
            {
                long currentTime = getTime();
                long elapsed = currentTime - previousTime;
                previousTime = currentTime;
                double fps = 1.0 / (elapsed / 1000.0);
                oView.setFPS(fps);


                lag += elapsed;

                processInput();
                if (currentGameState == gameState.Resumed)
                {
                    while (lag >= MS_PER_UPDATE)
                    {
                        update();
                        lag -= MS_PER_UPDATE;
                    }
                }
                else
                {
                    oView.displayPause();
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
                    currentGameState = gameState.Resumed;
                    oView.isPaused = false;
                    break;

                case Keys.P:
                    Console.WriteLine("Pause");
                    currentGameState = gameState.Paused;
                    oView.isPaused = true;
                    break;

                case Keys.Left:
                    Console.WriteLine("Left");
                    slimeDirX = -1;
                    slimeDirY = 0;
                    break;

                case Keys.Right:
                    Console.WriteLine("Right");
                    slimeDirX = 1;
                    slimeDirY = 0;
                    break;

                case Keys.Up:
                    Console.WriteLine("Up");
                    slimeDirX = 0;
                    slimeDirY = -1;
                    break;

                case Keys.Down:
                    Console.WriteLine("Down");
                    slimeDirX = 0;
                    slimeDirY = 1;
                    break;

                default:
                    slimeDirX = 0;
                    slimeDirY = 0;
                    break;
            }
        }
        private void update()
        {
            oView.moveRectangle(slimeDirX*slimeVelocity, slimeDirY * slimeVelocity);
        }

    }
}
