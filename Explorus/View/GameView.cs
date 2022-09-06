﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Explorus
{

    public class GameView
    {
        private double fps;
        private GameForm oGameForm;

        private bool isPaused = false;

        private Image iPausedImage;
        private Image iPlayerImage;

        private Map map;

        public GameView()
        {
            oGameForm = new GameForm();
            oGameForm.MinimumSize = new Size(600, 600);
            oGameForm.Paint += GameRenderer;

            map = Map.GetInstance(); //caller le singleton de map

            Bitmap myBitmap = new Bitmap("./Resources/TilesSheet.png");
            Rectangle cloneRect = new Rectangle(0, 96, 96, 96);

            iPausedImage = Image.FromFile("./Resources/pause.png");
            // TODO: use the interface size instead
            iPausedImage = resizeImage(iPausedImage, new Size(500, 500));


            iPlayerImage = myBitmap.Clone(cloneRect, myBitmap.PixelFormat);
        }

        public void Show() { Application.Run(oGameForm); }

        public void Render()
        {
            if (oGameForm.Visible)
                oGameForm.BeginInvoke((MethodInvoker)delegate {
                    oGameForm.Refresh();
                });
        }

        public void Close()
        {
            if (oGameForm.Visible)
                oGameForm.BeginInvoke((MethodInvoker)delegate {
                    oGameForm.Close();
                });
        }
        private void GameRenderer(object sender, PaintEventArgs e)
        {
            if (isPaused)
            {
                e.Graphics.DrawImage(iPausedImage, new Point(0, 0));
            }
            else
            {
                Graphics graphic = e.Graphics;
                graphic.Clear(Color.Black);

                oGameForm.Text = "Labo GEI794 – FPS " + Convert.ToString(getFPS());
                int xSize = map.GetTypeMap().GetLength(0) * 96;
                int ySize = map.GetTypeMap().GetLength(1) * 96;

                float xScaling = ((float)oGameForm.Size.Width - 16)/(float)xSize;
                float yScaling = ((float)oGameForm.Size.Height - 42)/(float)ySize;

                float minScale = Math.Min(xScaling, yScaling);
                int xOffset = 0;
                int yOffset = 0;
                if (xScaling > yScaling)
                {
                    xOffset = (oGameForm.Size.Width - 16 - (int)(yScaling * (float)xSize))/ 2;
                }
                else
                {
                    yOffset = (oGameForm.Size.Height - 30 - (int)(xScaling * (float)ySize)) / 2;
                }
                

                for(int i = 0; i< map.GetObjectList().Count(); i++)
                {
                    Image img = map.GetObjectList()[i].GetImage();
                    e.Graphics.DrawImage(img, new Rectangle(new Point((int)(map.GetObjectList()[i].GetPosition().X * minScale) + xOffset, (int)(map.GetObjectList()[i].GetPosition().Y * minScale) + yOffset), new Size((int)(img.Size.Width * minScale), (int)(img.Size.Height * minScale))));
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
            Keys currentInput = oGameForm.getCurrentInput();
            oGameForm.resetCurrentInput();
            return currentInput;
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public bool getIsPaused()
        {
            return isPaused;
        }
        public void setIsPaused(bool state)
        {
            isPaused = state;
        }
        public Map getMap()
        {
            return map;
        }
    }
}
