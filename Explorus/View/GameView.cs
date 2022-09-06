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

        public Map map;

        public GameView()
        {
            oGameForm = new GameForm();
            oGameForm.Paint += GameRenderer;

            map = Map.GetInstance(); //caller le singleton de map

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

                for(int i = 0; i< map.GetObjectMap().Count(); i++)
                {
                    //Console.WriteLine(map.objectMap[i].GetType());

                    if (map.GetObjectMap()[i].GetType() == typeof(Slimus))
                    {
                        Point point = map.GetObjectMap()[i].GetPosition();
                        //SlimePositionX = point.X;
                        //SlimePositionY = point.Y;
                        //map.objectMap[i].SetPosition(SlimePositionX, SlimePositionY);
                    }

                    e.Graphics.DrawImage(map.GetObjectMap()[i].GetImage(), map.GetObjectMap()[i].GetPosition());
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
            //SlimePositionX += x;
            //SlimePositionY += y;
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
                
    }
}
