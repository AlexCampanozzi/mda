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

        public bool isPaused = false;

        private Image iPausedImage;
        private Image iPlayerImage;

        private Map map;

        public GameView()
        {
            oGameForm = new GameForm();
            oGameForm.Paint += GameRenderer;

            map = new Map(new Bitmap("./Resources/map.png"));

            Bitmap myBitmap = new Bitmap("./Resources/TilesSheet.png");
            Rectangle cloneRect = new Rectangle(0, 96, 96, 96);

            iPausedImage = Image.FromFile(Application.StartupPath + "/../../Resources/pause.PNG");
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

                //e.Graphics.DrawImage(iPlayerImage, new Point(SlimePositionX, SlimePositionY));

                for(int i = 0; i< map.objectMap.Count(); i++)
                {
                    //Console.WriteLine(map.objectMap[i].GetType());

                    if (map.objectMap[i].GetType() == typeof(Slimus))
                    {
                        Point point = map.objectMap[i].GetPosition();
                        //SlimePositionX = point.X;
                        //SlimePositionY = point.Y;
                        map.objectMap[i].SetPosition(SlimePositionX, SlimePositionY);
                    }

                    e.Graphics.DrawImage(map.objectMap[i].GetImage(), map.objectMap[i].GetPosition());
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

        public void moveSlimus(int x, int y)
        {
            SlimePositionX += x;
            SlimePositionY += y;
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
                
    }
}
