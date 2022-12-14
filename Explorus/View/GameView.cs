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
using System.Windows.Media.TextFormatting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Explorus
{

    public sealed class GameView
    {
        private static readonly GameView instance = new GameView();

        private Keys currentInput = Keys.None;
        private double fps;
        private GameForm oGameForm;

        private Image pauseImage;
        private Image resumeImage;
        private Image gameOverImage;
        private Image menuImage;

        private PaintEventArgs paint;

        private Map map;
        private Header header;

        private bool formOpen = true;
        private FormWindowState windowState;
        private bool wasMinimized = false;
        private bool hadLostFocus = false;

        private MenuWindow menuWindow = MenuWindow.Instance;
        public float rewindTime = 0;

        static GameView()
        {

        }
        private GameView()
        {
            load();
        }

        private void load()
        {
            oGameForm = new GameForm();
            oGameForm.MinimumSize = new Size(600, 600);
            oGameForm.Paint += GameRenderer;

            oGameForm.FormClosed += new FormClosedEventHandler(Form_Closed);
            oGameForm.Resize += new EventHandler(Form_Resize);
            oGameForm.LostFocus += new EventHandler(Form_LostFocus);
            //oGameForm.GotFocus += new EventHandler(Form_GainFocus);
          

            map = Map.Instance;
            header = Header.GetInstance();
            windowState = oGameForm.WindowState;

            pauseImage = Image.FromFile("./Resources/pause_menu.png");
            resumeImage = Image.FromFile("./Resources/resuming.png");
            gameOverImage = Image.FromFile("./Resources/gameover.png");

            oGameForm.SubscribeToInput(this);

            xSize = map.GetTypeMap().GetLength(0) * 96;
            ySize = map.GetTypeMap().GetLength(1) * 96 + 96;
        }

        public static GameView Instance
        {
            get
            {
                return instance;
            }
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
            if (GameEngine.GetInstance().GetState().Name() != "Start")
            {
                windowState = oGameForm.WindowState;

                if (this.windowState == FormWindowState.Minimized)
                {
                    wasMinimized = true;
                    GameEngine.GetInstance().ChangeState(new PauseState(GameEngine.GetInstance()));
                }
                /*
                if (this.windowState == FormWindowState.Normal)
                {
                    wasMinimized = false;
                    GameEngine.GetInstance().ChangeState(new ResumeState(GameEngine.GetInstance()));
                }*/

            }

            pauseImage = resizeImage(pauseImage, new Size(oGameForm.Size.Width / 2, oGameForm.Size.Height / 3));
            resumeImage = resizeImage(gameOverImage, new Size(oGameForm.Size.Width / 2, oGameForm.Size.Height / 3));
            gameOverImage = resizeImage(gameOverImage, new Size(oGameForm.Size.Width / 2, oGameForm.Size.Height / 3));
            menuImage = resizeImage(menuImage, new Size(oGameForm.Size.Width, oGameForm.Size.Height));

        }

        private void Form_LostFocus(object sender, System.EventArgs e)
        {
            if (GameEngine.GetInstance().GetState().Name() != "Start")
            {
                hadLostFocus = true;
                GameEngine.GetInstance().ChangeState(new PauseState(GameEngine.GetInstance()));
            }

        }

        /*
        private void Form_GainFocus(object sender, System.EventArgs e)
        {
            if (GameEngine.GetInstance().GetState().Name() != "Start")
            {
                if (hadLostFocus)
                {
                    hadLostFocus = false;
                    GameEngine.GetInstance().ChangeState(new ResumeState(GameEngine.GetInstance()));

                }
            }
        }*/

        public void Close()
        {
            Application.Exit();
        }

        private int xSize;
        private int ySize;
        float xScaling;
        float yScaling;

        float minScale;
        int xOffset;
        int yOffset;

        private void GameRenderer(object sender, PaintEventArgs e)
        {
            paint = e;
            string gameState = GameEngine.GetInstance().GetState().Name();
            oGameForm.Text = "Niveau " + GameMaster.Instance.getCurrentLevel() + " ۰•● ❤ ●•۰ " + gameState;

            if (fps != 0)
            {
                oGameForm.Text = oGameForm.Text + " ۰•● ❤ ●•۰  FPS: " + fps.ToString();
            }

            //pour rendre le jeu transparent
            ColorMatrix matrix = new ColorMatrix();
            matrix.Matrix33 = 0.5f;
            ImageAttributes imgAtt = new ImageAttributes();
            imgAtt.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);


            e.Graphics.Clear(Color.Black);

            

            xScaling = ((float)oGameForm.Size.Width - 16) / (float)xSize;
            yScaling = ((float)oGameForm.Size.Height - 42) / (float)ySize;

            minScale = Math.Min(xScaling, yScaling); 
            xOffset = 0;
            yOffset = 0;

            if (xScaling > yScaling)
            {
                xOffset = (oGameForm.Size.Width - 16 - (int)(yScaling * (float)xSize)) / 2;
            }
            else
            {
                yOffset = (oGameForm.Size.Height - (int)(xScaling * (float)ySize)) / 2;
            }

            List<GameObject> compoundGameObjectList = map.GetCompoundGameObject().getComponentGameObjetList();
            lock (compoundGameObjectList)
            {
                for (int i = 0; i < compoundGameObjectList.Count; i++)
                {
                    Image img = compoundGameObjectList[i].GetImage();
                    int size_offset = 0;

                    try
                    {
                        if (compoundGameObjectList[i].GetImage().Size.Height <= 48)
                        {
                            size_offset = 24;
                        }
                    }
                    catch
                    {

                    }

                    if (gameState != "Play" && gameState != "Replay")
                    {
                        e.Graphics.DrawImage(img, new Rectangle(new Point((int)((compoundGameObjectList[i].GetPosition().X + size_offset) * minScale) + xOffset, (int)((compoundGameObjectList[i].GetPosition().Y + size_offset) * minScale) + yOffset + (int)(96.0 * minScale)), new Size((int)(img.Size.Width * minScale), (int)(img.Size.Height * minScale))), 0, 0, img.Size.Width, img.Size.Height, GraphicsUnit.Pixel, imgAtt);
                    }
                    else
                    {
                        e.Graphics.DrawImage(img, new Rectangle(new Point((int)((compoundGameObjectList[i].GetPosition().X + size_offset) * minScale) + xOffset, (int)((compoundGameObjectList[i].GetPosition().Y + size_offset) * minScale) + yOffset + (int)(96.0 * minScale)), new Size((int)(img.Size.Width * minScale), (int)(img.Size.Height * minScale))));
                    }

                }
            }
            
            e.Graphics.DrawImage(header.getHeaderImage(), new Rectangle(new Point(xOffset, yOffset + (int)(60.0 * yScaling)), new Size((int)(1152.0 * minScale), (int)(96.0 * minScale))));
                
            if (gameState == "Pause" || gameState == "Start" || gameState == "Audio" || gameState == "Level")
            {
                if (menuWindow.IsChanged)
                {
                    menuImage = menuWindow.getMenuWindow(e);
                    menuImage = resizeImage(menuWindow.getMenuWindow(e), new Size(oGameForm.Size.Width, oGameForm.Size.Height));
                }
                e.Graphics.DrawImage(menuImage, new Point((int)minScale + 100, (int)(minScale + 70)));

            }

            if (gameState == "Resume")
            {
                e.Graphics.DrawImage(resumeImage, new Point(oGameForm.Size.Width / 4, oGameForm.Size.Height / 4));
            }

            if (gameState == "Stop" || GameMaster.Instance.HasSlimusDied)
            {
                e.Graphics.DrawImage(gameOverImage, new Point(oGameForm.Size.Width / 4, oGameForm.Size.Height / 4));
            }

            if (gameState == "Replay")
            {
                oGameForm.Text += " " + rewindTime;
                string text1 = "Replay: " + rewindTime;
                using (Font font1 = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point))
                {
                    Rectangle rect = new Rectangle(oGameForm.Size.Width / 8, oGameForm.Size.Height / 10, oGameForm.Size.Width / 4, oGameForm.Size.Height / 20);

                    e.Graphics.FillRectangle(Brushes.Black, rect);
                    e.Graphics.DrawRectangle(Pens.Black, rect);
                    e.Graphics.DrawString(text1, font1, Brushes.White, rect);
                }
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
