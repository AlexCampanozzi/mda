﻿/*
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
using Explorus.Threads;

namespace Explorus
{
    /*
    public enum GameState
    {
        Resumed,
        Paused,
        End,
    }*/

    public class GameEngine
    {
        private GameView oView;
        private State state;
        private Keys currentInput;

        public GameEngine()
        {
            
        }

        public void Start()
        {
            oView = new GameView();
            Thread thread = new Thread(new ThreadStart(GameLoop));
            thread.Start();

            Thread physicsThread = new Thread(new PhysicsThread().Run);
            physicsThread.Start();

            oView.Show();
            this.state = new PlayState(this);
        }

        public void Stop()
        {
            oView.Close();
        }

        public void ChangeState(State state)
        {
            this.state = state;
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
                if(state != null)
                {
                    state.stateUpdate();

                    lag = state.Lag(lag, MS_PER_UPDATE);

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

        public void processInput() //public not a fan
        {
            currentInput = oView.getCurrentInput();

            switch (currentInput)
            {
                case Keys.R:
                    Console.WriteLine("Resume");
                    ChangeState(new PlayState(this));
                    //currentGameState = GameState.Resumed;
                    oView.setIsPaused(false);
                    break;

                case Keys.P:
                    Console.WriteLine("Pause");
                    ChangeState(new PauseState(this));
                    //currentGameState = GameState.Paused;
                    oView.setIsPaused(true);
                    break;

                default:
                    break;
            }

            oView.getMap().GetCompoundGameObject().processInput();

        }
        public void update() //public not a fan
        {
            GameMaster gameMaster = GameMaster.GetInstance();

            if (gameMaster.isLevelOver()) 
            {
                ChangeState(new StopState(this));
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

        public State getState()
        {
            return this.state;
        }


    }
}
