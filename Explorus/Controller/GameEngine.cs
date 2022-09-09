/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Explorus.Controller;


namespace Explorus
{
    public enum GameState
    {
        Resumed,
        Paused,
        End,
    }

    public class GameEngine
    {
        private GameView oView;
        private GameState currentGameState;
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
            while (oView.notClosed())
            {
                long currentTime = getTime();
                long elapsed = currentTime - previousTime;
                previousTime = currentTime;
                double fps = 1.0 / (elapsed / 1000.0);
                oView.setFPS(fps);


                lag += elapsed;
                
                if (currentGameState != GameState.End)
                {
                    processInput();
                }
                else
                {
                    Thread.Sleep(4000);
                    Application.Exit();
                }

                if (currentGameState == GameState.Resumed)
                {
                    if(lag >= MS_PER_UPDATE)
                    {
                        while (lag >= MS_PER_UPDATE)
                        {
                            update();
                            lag -= MS_PER_UPDATE;
                        }
                    }
                }
                oView.Render();

                Thread.Sleep(1);
            }
            Thread.Sleep(5000);
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
                    currentGameState = GameState.Resumed;
                    oView.setIsPaused(false);
                    break;

                case Keys.P:
                    Console.WriteLine("Pause");
                    currentGameState = GameState.Paused;
                    oView.setIsPaused(true);
                    break;

                default:
                    break;
            }

            oView.getMap().GetCompoundGameObject().processInput();

        }
        private void update()
        {
            GameMaster gameMaster = GameMaster.GetInstance();

            if (gameMaster.isLevelOver()) 
            { 
                currentGameState = GameState.End;
                oView.setIsOver(true);
            }

            // process movement

            oView.getMap().GetCompoundGameObject().update(currentInput);
            

            gameMaster.update();
            oView.getHeader().setKey(gameMaster.GetKeyStatus()); // à changer de place
            oView.getHeader().setGem(gameMaster.getGemStatus());

        }

        public Keys GetCurrentInput()
        {
            return this.currentInput;
        }

        public void SetCurrentInput(Keys key)
        {
            currentInput = key;
        }

        public GameState GetCurrentGameState()
        {
            return currentGameState;
        }

    }
}
