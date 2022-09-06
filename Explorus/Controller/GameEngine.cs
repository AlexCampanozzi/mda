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
using System.Security.Cryptography.X509Certificates;
using Explorus.Controller;

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
        private gameState currentGameState;
        private Keys currentInput;
        

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
                oView.Render();

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
            currentInput = oView.getCurrentInput();

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

                default:
                    slimeDirX = 0;
                    slimeDirY = 0;
                    break;
            }

            for (int i = 0; i < oView.map.GetObjectList().Count(); i++)
            {
                oView.map.GetObjectList()[i].processInput();
            }

            }
        private void update()
        {
            GameMaster gameMaster = GameMaster.GetInstance();

            Map oMap = Map.GetInstance();

            // process movement
            for (int i = 0; i < oView.map.GetObjectList().Count(); i++)
            {
                    oView.map.GetObjectList()[i].SetCurrentInput(currentInput); //list of game objects
                    oView.map.GetObjectList()[i].update();

            }

            gameMaster.update();


        }

    }
}
