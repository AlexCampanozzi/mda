/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using Explorus.Controller;
using Explorus.Model;
using Explorus.Threads;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Explorus.Controller
{
    public enum Option
    {
        Start,
        Audio,
        Level,
        Exit
    }

    public sealed class GameEngine
    {
        private static GameEngine instance = null;
        private static readonly object padlock = new object();

        private GameView oView;
        private State state;
        private Keys currentInput;
        private bool FPSOn = false;
        private AudioState audioState;
        private LevelState levelState;
        private MenuWindow menuWindow;

        private PhysicsThread physics;
        private AudioThread audio;
        private RenderThread render;

        private Thread physicsThread;
        private Thread renderThread;
        private Thread audioThread;

        private Option currentOption;
        private Option[] startMenu = new Option[] {Option.Start, Option.Audio, Option.Level, Option.Exit };
        private Option[] pauseMenu = new Option[] { Option.Start, Option.Audio, Option.Exit };
        private AudioOption[] audioMenu = new AudioOption[] { AudioOption.Music, AudioOption.Sound, AudioOption.Back };
        private LevelOption[] levelMenu = new LevelOption[] { LevelOption.Slimes, LevelOption.Level, LevelOption.Back };
        private int menuIndex = 0;
        private int subMenuIndex = 0;
        private string lastMenuState;

        private List<CompoundGameObject> savedMaps = new List<CompoundGameObject>();
        private System.Windows.Forms.Timer timer1;




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
            //this.state = new PlayState(this);
            this.state = new StartState(this);
            audioState = new AudioState(this);
            
            menuWindow = MenuWindow.Instance;
            currentOption = Option.Start;
            menuWindow.setOption(currentOption);

            levelState = new LevelState(this);
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
                if (state.Name() != "Play" && state.Name() != "Replay")
                {
                    elapsed = 0;
                    lag = 0;
                }
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

            if (currentInput != Keys.None)
            {
                menuWindow.IsChanged = true;
            }


            oView.getMap().GetCompoundGameObject().processInput();

            switch (currentInput)
            {
                case Keys.P:
                    Console.WriteLine("current option is " + audioState.GetMenuOption().ToString());
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
                    case Keys.Up:
                        if(menuIndex != 0)
                        {
                            menuIndex--;
                        }
                        break;

                    case Keys.Down:
                        if (menuIndex != pauseMenu.Length-1)
                        {
                            menuIndex++;
                        }
                        break;
                }
                currentOption = pauseMenu[menuIndex];
                menuWindow.setOption(currentOption);

                if(currentInput == Keys.Space)
                {

                    switch (currentOption)
                    {
                        case Option.Start: 
                            ChangeState(new ResumeState(this));
                            break;
                        case Option.Audio:
                            ChangeState(audioState);
                            lastMenuState = "Pause";
                            break;
                        case Option.Exit:
                            menuIndex = 0;
                            ChangeState(new StopState(this));
                            break;
                        default:
                            break;
                    }
                }

            }

            if (this.state.Name() == "Start")
            {

                switch (currentInput)
                {
                    case Keys.Up:
                        if(menuIndex != 0)
                        {
                            menuIndex--;
                        }
                        break;

                    case Keys.Down:
                        if (menuIndex != startMenu.Length-1)
                        {
                            menuIndex++;
                        }
                        break;
                }
                currentOption = startMenu[menuIndex];
                menuWindow.setOption(currentOption);

                if(currentInput == Keys.Space)
                {

                    switch (currentOption)
                    {
                        case Option.Start:
                            oView.getMap().MapPath = GetLevelState().chosenLevelName();
                            oView.getMap().resetMap();

                            GameMaster gameMaster = GameMaster.Instance;
                            gameMaster.setSlimeAmount(GetLevelState().Slimes);

                            ChangeState(new PlayState(this));
                            switch(GetLevelState().chosenLevelName())
                            {
                                case "map_jazz.png":
                                    audio.setJazzMusic();
                                    break;
                                case "map_slam.png":
                                    audio.setSlamMusic();
                                    break;
                                default:
                                    audio.setGameMusic();
                                    break;
                            }
                            break;
                        case Option.Audio:
                            ChangeState(audioState);
                            lastMenuState = "Start";
                            break;
                        case Option.Level:
                            levelState = new LevelState(this);
                            ChangeState(levelState);
                            break;
                        case Option.Exit:
                            ChangeState(new StopState(this));
                            break;
                        default:
                            break;
                    }
                }

            }

            if (this.state.Name() == "Level")
            {
                switch (currentInput)
                {
                    case Keys.Up:
                        if (subMenuIndex != 0)
                        {
                            subMenuIndex--;
                        }
                        break;

                    case Keys.Down:
                        if (subMenuIndex != audioMenu.Length - 1)
                        {
                            subMenuIndex++;
                        }
                        break;

                    case Keys.Right:
                        levelState.Increment(1);
                        break;

                    case Keys.Left:
                        levelState.Increment(-1);
                        break;

                    case Keys.Space:
                        if (subMenuIndex == levelMenu.Length - 1)
                        {
                            subMenuIndex = 0;
                            ChangeState(new StartState(this));
                        }
                        break;
                }
                levelState.SetOption(levelMenu[subMenuIndex]);
            }
        

            if (this.state.Name() == "Audio")
            {
                switch (currentInput)
                {
                    case Keys.Up:
                        if (subMenuIndex != 0)
                        {
                            subMenuIndex--;
                        }                       
                        break;

                    case Keys.Down:
                        if (subMenuIndex != audioMenu.Length - 1)
                        {
                            subMenuIndex++;
                        }
                        break;

                    case Keys.Right:
                        audioState.ChangeVolume(1);
                        break;

                    case Keys.Left:
                        audioState.ChangeVolume(-1);
                        break;

                    case Keys.M:
                        audioState.ChangeVolume(0);
                        break;

                    case Keys.Space:
                        if(subMenuIndex == audioMenu.Length - 1)
                        {
                            subMenuIndex = 0;
                            if (lastMenuState == "Start")
                            {
                                ChangeState(new StartState(this));
                            }
                            if (lastMenuState == "Pause")
                            {
                                ChangeState(new PauseState(this));
                            }
                        }
                        break;
                }
                audioState.SetOption(audioMenu[subMenuIndex]);
            }


        }
        public void update() //public not a fan
        {
            GameMaster gameMaster = GameMaster.Instance;
            physics = PhysicsThread.GetInstance();


            if (gameMaster.isGameOver() && physics.invoker.IsRepeatDone() == false)
            {
                ChangeState(new ReplayState(this));
                // Replay 5 last seconds

                physics.invoker.ExecuteAll(savedMaps);
                gameMaster.update();
                oView.rewindTime = physics.invoker.remainingTime;

            }
            else if (gameMaster.isGameOver() && physics.invoker.IsRepeatDone())
            {
                Console.WriteLine("game Over");
                ChangeState(new StopState(this));
            }
            //if (gameMaster.isLevelOver()) oView.setIsOver(true);
            else
            {
                // process movement

                oView.getMap().GetCompoundGameObject().update(currentInput);


                gameMaster.update();

                /*oView.getHeader().setKey(gameMaster.GetKeyStatus()); // C'ÉTAIT ENTRES AUTRES À CAUSE DE ÇA LE MEMORY LEAK
                oView.getHeader().setGem(gameMaster.getGemStatus());
                oView.getHeader().setLife(gameMaster.getLifeStatus());
                oView.getHeader().setBubble(gameMaster.getBubbleStatus());*/


                saveMap();
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

        public AudioState GetAudioState()
        {
            return this.audioState;
        }

        public LevelState GetLevelState()
        {
                 return this.levelState;
        }

        private void saveMap()
        {

            if (savedMaps.Count > 300)
            {
                savedMaps.RemoveAt(0);
            }
            savedMaps.Add(oView.getMap().GetCompoundGameObject());
        }


    }
}
