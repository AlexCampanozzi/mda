/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using Explorus.Model;
using Explorus.Controller;
using System.Drawing.Imaging;

namespace Explorus
{

    public sealed class GameView
    {
        private static GameView instance = null;
        private static readonly object padlock = new object();

        private Keys currentInput = Keys.None;
        private double fps;
        private GameForm oGameForm;

        private Image pauseImage;
        private Image resumeImage;
        private Image gameOverImage;

        private Map map;
        private Header header;

        private bool formOpen = true;
        private FormWindowState windowState;
        private bool wasMinimized = false;
        private bool hadLostFocus = false;

        public GameView()
        {
            oGameForm = new GameForm();
            oGameForm.MinimumSize = new Size(600, 600);
            oGameForm.Paint += GameRenderer;

            oGameForm.FormClosed += new FormClosedEventHandler(Form_Closed);
            oGameForm.Resize += new EventHandler(Form_Resize);
            oGameForm.LostFocus += new EventHandler(Form_LostFocus);
            oGameForm.GotFocus += new EventHandler(Form_GainFocus);

            map = Map.Instance;
            header = Header.GetInstance();
            windowState = oGameForm.WindowState;

            pauseImage = Image.FromFile("./Resources/pause.png");
            resumeImage = Image.FromFile("./Resources/resuming.png");
            gameOverImage = Image.FromFile("./Resources/gameover.png");

            oGameForm.SubscribeToInput(this);
        }

        public static GameView GetInstance()
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GameView();
                    }
                }
            }
            return instance;
        }

        public void inputSubscription(Keys newInput)
        {
            currentInput = newInput;
            oGameForm.resetCurrentInput();
        }
        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(1);
            formOpen = false;
        }

        public bool notClosed()
        {
            return formOpen;
        }

        public void Show() {
            Thread thread = new Thread(() =>
            {
                Application.Run(oGameForm);
            });
            thread.Start();
        }

        public void Render()
        {
            try
            {
                if (oGameForm.Visible)
                    oGameForm.BeginInvoke((MethodInvoker)delegate
                    {
                        oGameForm.Refresh();
                    });
            }
            catch
            {

            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            windowState = oGameForm.WindowState;

            if (this.windowState == FormWindowState.Minimized)
            {
                wasMinimized = true;
                Console.WriteLine("minimize");
                GameEngine.GetInstance().ChangeState(new PauseState(GameEngine.GetInstance()));
            }

            if (this.windowState == FormWindowState.Normal)
            {
                wasMinimized = false;
                GameEngine.GetInstance().ChangeState(new ResumeState(GameEngine.GetInstance()));
            }
        }

        private void Form_LostFocus(object sender, System.EventArgs e)
        {
            hadLostFocus = true;
            Console.WriteLine("game lost focus");
            GameEngine.GetInstance().ChangeState(new PauseState(GameEngine.GetInstance()));
        }

        private void Form_GainFocus(object sender, System.EventArgs e)
        {
            if (hadLostFocus)
            {
                hadLostFocus = false;
                Console.WriteLine("game regained focus");
                GameEngine.GetInstance().ChangeState(new ResumeState(GameEngine.GetInstance()));

            }
        }

        public void Close()
        {
            Application.Exit();
        }

        private void GameRenderer(object sender, PaintEventArgs e)
        {

            string gameState = GameEngine.GetInstance().GetState().Name();
            oGameForm.Text = gameState;

            //pour rendre le jeu transparent
            ColorMatrix matrix = new ColorMatrix();
            matrix.Matrix33 = 0.5f;
            ImageAttributes imgAtt = new ImageAttributes();
            imgAtt.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);


            e.Graphics.Clear(Color.Black);
            int xSize = map.GetTypeMap().GetLength(0) * 96;
            int ySize = map.GetTypeMap().GetLength(1) * 96 + 96;

            float xScaling = ((float)oGameForm.Size.Width - 16) / (float)xSize;
            float yScaling = ((float)oGameForm.Size.Height - 42) / (float)ySize;

            float minScale = Math.Min(xScaling, yScaling);
            int xOffset = 0;
            int yOffset = 0;

            if (xScaling > yScaling)
            {
                xOffset = (oGameForm.Size.Width - 16 - (int)(yScaling * (float)xSize)) / 2;
            }
            else
            {
                yOffset = (oGameForm.Size.Height - (int)(xScaling * (float)ySize)) / 2;
            }

            List<GameObject> compoundGameObjectList = map.GetCompoundGameObject().getComponentGameObjetList();
            for (int i = 0; i < compoundGameObjectList.Count; i++)
            {
                Image img = compoundGameObjectList[i].GetImage();
                int size_offset = 0;
                if(compoundGameObjectList[i].GetImage().Size.Height <= 48) { 
                    size_offset = 24; }

                if(gameState != "Play")
                {
                    e.Graphics.DrawImage(img, new Rectangle(new Point((int)((compoundGameObjectList[i].GetPosition().X + size_offset) * minScale) + xOffset, (int)((compoundGameObjectList[i].GetPosition().Y + size_offset) * minScale) + yOffset + (int)(96.0 * minScale)), new Size((int)(img.Size.Width * minScale), (int)(img.Size.Height * minScale))), 0, 0, img.Size.Width, img.Size.Height, GraphicsUnit.Pixel, imgAtt);
                }
                else
                {
                    e.Graphics.DrawImage(img, new Rectangle(new Point((int)((compoundGameObjectList[i].GetPosition().X + size_offset) * minScale) + xOffset, (int)((compoundGameObjectList[i].GetPosition().Y + size_offset) * minScale) + yOffset + (int)(96.0 * minScale)), new Size((int)(img.Size.Width * minScale), (int)(img.Size.Height * minScale))));
                }
            }
                
            e.Graphics.DrawImage(header.getHeaderImage(), new Rectangle(new Point(xOffset, yOffset + (int)(60.0 * yScaling)), new Size((int)(1152.0 * minScale), (int)(96.0 * minScale))));
                
            if (gameState == "Pause")
            {
                pauseImage = resizeImage(pauseImage, new Size(oGameForm.Size.Width/2, oGameForm.Size.Height/3));
                e.Graphics.DrawImage(pauseImage, new Point(oGameForm.Size.Width / 4, oGameForm.Size.Height / 4));
            }

            if (gameState == "Resume")
            {
                resumeImage = resizeImage(resumeImage, new Size(oGameForm.Size.Width / 2, oGameForm.Size.Height / 3));
                e.Graphics.DrawImage(resumeImage, new Point(oGameForm.Size.Width / 4, oGameForm.Size.Height / 4));
            }

            if (gameState == "Stop")
            {
                resumeImage = resizeImage(gameOverImage, new Size(oGameForm.Size.Width / 2, oGameForm.Size.Height / 3));
                e.Graphics.DrawImage(gameOverImage, new Point(oGameForm.Size.Width / 4, oGameForm.Size.Height / 4));
            }


        }
        public double getFPS()
        {
            return fps;
        }

        public void setFPS(double value)
        {
            fps = value;
        }

        public Keys getCurrentInput()
        {
            Keys output = currentInput;
            currentInput = Keys.None;
            return output;
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public Map getMap()
        {
            return map;
        }

        public Header getHeader()
        {
            return header;
        }
        public GameForm getGameForm()
        {
            return oGameForm;
        }
    }
}
