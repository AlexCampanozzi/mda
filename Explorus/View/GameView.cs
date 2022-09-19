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
using System.IO.Ports;
using System.Drawing.Drawing2D;

namespace Explorus
{

    public sealed class GameView
    {
        private static GameView instance = null;
        private static readonly object padlock = new object();

        private Keys currentInput = Keys.None;
        private double fps;
        private GameForm oGameForm;

        private Image iPausedImage;
        private Image iEndImage;

        private Map map;
        private Header header;

        private bool formOpen;

        public GameView()
        {
            oGameForm = new GameForm();
            oGameForm.MinimumSize = new Size(600, 600);
            oGameForm.Paint += GameRenderer;
            formOpen = true;
            oGameForm.FormClosed += new FormClosedEventHandler(FormClosed);
            map = Map.GetInstance();
            header = Header.GetInstance();

            iPausedImage = Image.FromFile("./Resources/pause.png");
            // TODO: use the interface size instead
            iPausedImage = resizeImage(iPausedImage, oGameForm.Size);

            iEndImage = resizeImage(Image.FromFile("./Resources/EndOfLevel.png"), oGameForm.Size);
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
        private void FormClosed(object sender, FormClosedEventArgs e)
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

        public void Close()
        {
            Console.WriteLine("its inside close function");

            if (oGameForm.Visible)
                oGameForm.BeginInvoke((MethodInvoker)delegate {
                    oGameForm.Close();
                    Console.WriteLine("its doing the close");
                });
        }

        public void Form_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Console.WriteLine("closing function");
            Application.Exit();
        }

        private void GameRenderer(object sender, PaintEventArgs e)
        {

            State gameState = GameEngine.GetInstance().GetState();

            ColorMatrix matrix = new ColorMatrix();
            matrix.Matrix33 = 0.5f;
            ImageAttributes imgAtt = new ImageAttributes();
            imgAtt.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            if (gameState.GetType().Name == "StopState")
            {
                //e.Graphics.Clear(Color.Black);
                e.Graphics.DrawImage(iEndImage, new Point(0, 0));
            }
            /*if (gameState.GetType().Name == "PauseState")
            {
                //e.Graphics.Clear(Color.Black);
                Bitmap bitmap = new Bitmap(iPausedImage);
                e.Graphics.DrawImage(bitmap, new Point(0, 0));

            }*/
            else
            {
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

                    if(gameState.GetType().Name == "PauseState")
                    {
                        e.Graphics.DrawImage(img, new Rectangle(new Point((int)((compoundGameObjectList[i].GetPosition().X + size_offset) * minScale) + xOffset, (int)((compoundGameObjectList[i].GetPosition().Y + size_offset) * minScale) + yOffset + (int)(96.0 * minScale)), new Size((int)(img.Size.Width * minScale), (int)(img.Size.Height * minScale))), 0, 0, img.Size.Width, img.Size.Height, GraphicsUnit.Pixel, imgAtt);
                    }
                    else
                    {
                        e.Graphics.DrawImage(img, new Rectangle(new Point((int)((compoundGameObjectList[i].GetPosition().X + size_offset) * minScale) + xOffset, (int)((compoundGameObjectList[i].GetPosition().Y + size_offset) * minScale) + yOffset + (int)(96.0 * minScale)), new Size((int)(img.Size.Width * minScale), (int)(img.Size.Height * minScale))));
                    }
                }
                
                e.Graphics.DrawImage(header.getHeaderImage(), new Rectangle(new Point(xOffset, yOffset + (int)(60.0 * yScaling)), new Size((int)(1152.0 * minScale), (int)(96.0 * minScale))));
                
                if (gameState.GetType().Name == "PauseState")
                {
                    iPausedImage = resizeImage(iPausedImage, new Size(oGameForm.Size.Width/3, oGameForm.Size.Height/3));
                    e.Graphics.DrawImage(iPausedImage, new Point(oGameForm.Size.Width / 4, oGameForm.Size.Height / 4)); //je laisse étienne gérer
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
    }
}
