/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using Explorus.Controller;
using Explorus.Model;
using Explorus.Threads;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Explorus
{
    public sealed class GameEngine
    {
        private static GameEngine instance = null;
        private static readonly object padlock = new object();

        private GameView oView;
        private State state;
        private Keys currentInput;
        private bool FPSOn = false;
        private MenuState menu;

        private PhysicsThread physics;
        private AudioThread audio;
        private RenderThread render;

        private Thread physicsThread;
        private Thread renderThread;
        private Thread audioThread;


        public GameEngine()
        {
        }

        public static GameEngine GetInstance()
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GameEngine();
                    }
                }
            }
            return instance;
        }

        public void Start()
        {
            this.state = new PlayState(this);
            menu = new MenuState(this);
            oView = GameView.Instance;

            Thread thread = new Thread(new ThreadStart(GameLoop));
            thread.Start();

            physics = PhysicsThread.GetInstance();
            physicsThread = new Thread(physics.Run);
            physicsThread.Start();

            render = RenderThread.GetInstance();
            renderThread = new Thread(render.Run);
            renderThread.Start();

            audio = AudioThread.Instance;
            audioThread = new Thread(audio.Run);
            audioThread.Start();

            oView.Show();
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
                fps = Math.Floor(fps);
                int readyForFPS = 0;

                if (FPSOn && readyForFPS == 0)
                {
                    readyForFPS = 100;
                    oView.setFPS(fps);
                }
                else
                {
                    oView.setFPS(0);
                }
                readyForFPS--;

                lag += elapsed;

                if (state != null)
                {
                    state.stateUpdate();
                    lag = state.Lag(lag, MS_PER_UPDATE);

                }

                //oView.Render();

                Thread.Sleep(1);
            }
        }

        public Thread getPhysicsThread()
        {
            return physicsThread;
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
                case Keys.P:
                    Console.WriteLine("current option is " + menu.GetMenuOption().ToString());
                    ChangeState(new PauseState(this));
                    break;

                case Keys.Escape:
                    ChangeState(new PauseState(this));
                    break;

                case Keys.Q:
                    ChangeState(new StopState(this));
                    break;

                case Keys.F:
                    FPSOn = !FPSOn;
                    break;

                default:
                    break;
            }

            if (this.state.Name() == "Pause")
            {
                switch (currentInput)
                {
                    case Keys.R:
                        ChangeState(new ResumeState(this));
                        break;
                        
                    case Keys.Up:
                        menu.SetOption(Option.Music);
                        break;

                    case Keys.Down:
                        menu.SetOption(Option.Sound);
                        break;

                    case Keys.Right:
                        menu.ChangeVolume(1);
                        break;

                    case Keys.Left:
                        menu.ChangeVolume(-1);
                        break;

                    case Keys.M:
                        menu.ChangeVolume(0);
                        break;
                }

            }

            oView.getMap().GetCompoundGameObject().processInput();

        }
        public void update() //public not a fan
        {
            GameMaster gameMaster = GameMaster.Instance;

            if (gameMaster.isGameOver())
            {
                ChangeState(new StopState(this));
            }
            //if (gameMaster.isLevelOver()) oView.setIsOver(true);
            else
            {
                // process movement

                oView.getMap().GetCompoundGameObject().update(currentInput);


                gameMaster.update();

                /*oView.getHeader().setKey(gameMaster.GetKeyStatus()); // C'ÉTAIT ENTRES AUTRES À CAUSE DE ÇA LE MEMORY LEAK FUCK YOU
                oView.getHeader().setGem(gameMaster.getGemStatus());
                oView.getHeader().setLife(gameMaster.getLifeStatus());
                oView.getHeader().setBubble(gameMaster.getBubbleStatus());*/

            }
        }
        public Keys GetCurrentInput()
        {
            return this.currentInput;
        }

        public void SetCurrentInput(Keys key)
        {
            currentInput = key;
        }

        public State GetState()
        {
            return this.state;
        }


    }
}
