using GEI797Labo.Models;
using GEI797Labo.Models.Definitions;
using GEI797Labo.Views;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace GEI797Labo.Controllers
{
    [Flags]
    internal enum Direction
    {
        None = 0,
        Down = 1,
        Right = 2,
        Up = 4,
        Left = 8
    }

    internal class GameController : IGameController, IDisposable
    {
        private const int DEFAULT_SIZE = 320;
        private const int SPRITE_SIZE = 50;

        private const long MS_PER_FRAME = 1000 / 60;
        private const float VELOCITY = 2.0f;        

        private readonly IGameModel oModel;
        private readonly IGameView oView;

        private readonly Thread oGameLoopThread;

        private Direction eSpriteDirection = Direction.None;
        private Shape oSprite;

        private bool stopGame = false;


        public GameController(IGameModel model)
        {
            IsDisposed = false;

            oModel = model;
            oView = new GameView(oModel, this);
            oGameLoopThread = new Thread(GameLoop);

            InitGameObject();
        }

        private void InitGameObject()
        {
            oSprite = new ShapeCircle(Color.Yellow, SPRITE_SIZE, SPRITE_SIZE, 10, 10);
            oModel.Shapes.Add(oSprite);

            // TODO : Add a shape to detect collisions...
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                if (oView != null)
                    oView.CloseWindow();
                if (oModel != null)
                    oModel.Dispose();
                IsDisposed = true;

                GC.SuppressFinalize(this);
            }
        }

        public void GameLoop()
        {
            long previous = GetCurrentTime();
            long lag = 0;

            while (IsRunning)
            {
                long current = GetCurrentTime();
                long elapsed = current - previous;
                previous = current;
                lag += elapsed;

                // Must satisfy the fps delay before processing the
                // updates and rendering
                if (lag >= MS_PER_FRAME)
                {
                    ProcessInput();

                    while (lag >= MS_PER_FRAME)
                    {
                        Update();
                        lag -= MS_PER_FRAME;
                    }

                    oView.Render();
                    Thread.Sleep(1);
                }
            }

            Dispose();
        }

        #region Getter / Setter

        public bool IsWindowLess { get; set; } = false;

        public bool IsDisposed { get; private set; }

        public int DefaultSize
        {
            get { return DEFAULT_SIZE; }
        }

        public bool IsRunning
        {
            get { return !stopGame && (IsWindowLess || oGameLoopThread.IsAlive); }
        }

        public void StartGame()
        {
            if (!IsRunning && !IsWindowLess)
            {
                oGameLoopThread.Start();
                oView.Show();
            }
        }

        public void StopGame()
        {
            if (IsRunning)
                stopGame = true;
        }

        public bool IsGameStopped { get { return stopGame; } }

        private long GetCurrentTime()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        #endregion

        #region Update

        private void Update()
        {
            // Windows form border limits
            if (oSprite.Position_X < 0)
                oSprite.Position_X = 0;
            if (oSprite.Position_Y < 0)
                oSprite.Position_Y = 0;
            if (oSprite.Position_X >= DEFAULT_SIZE - oSprite.Width)
                oSprite.Position_X = DEFAULT_SIZE - oSprite.Width;
            if (oSprite.Position_Y >= DEFAULT_SIZE - oSprite.Height)
                oSprite.Position_Y = DEFAULT_SIZE - oSprite.Height;
        }

        #endregion

        #region Key_Event

        private void ProcessInput()
        {
            if (eSpriteDirection == Direction.Up)
            {
                oSprite.Position_Y -= VELOCITY;
            }
            if (eSpriteDirection == Direction.Down)
            {
                oSprite.Position_Y += VELOCITY;
            }
            if (eSpriteDirection == Direction.Left)
            {
                oSprite.Position_X -= VELOCITY;
            }
            if (eSpriteDirection == Direction.Right)
            {
                oSprite.Position_X += VELOCITY;
            }
        }

        public void User_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) 
                eSpriteDirection |= Direction.Up;
            if (e.KeyCode == Keys.Down) 
                eSpriteDirection |= Direction.Down;
            if (e.KeyCode == Keys.Left) 
                eSpriteDirection |= Direction.Left;
            if (e.KeyCode == Keys.Right) 
                eSpriteDirection |= Direction.Right;
        }

        public void User_KeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) 
                eSpriteDirection &= ~Direction.Up;
            if (e.KeyCode == Keys.Down) 
                eSpriteDirection &= ~Direction.Down;
            if (e.KeyCode == Keys.Left) 
                eSpriteDirection &= ~Direction.Left;
            if (e.KeyCode == Keys.Right) 
                eSpriteDirection &= ~Direction.Right;
        }

        #endregion
    }
}
