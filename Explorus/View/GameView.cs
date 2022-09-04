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

        private int SlimePositionX = 0;
        private int SlimePositionY = 0;

        private Image iPlayerImage;

        private Map map;

        public GameView()
        {
            oGameForm = new GameForm();
            oGameForm.Paint += GameRenderer;

            map = new Map(new Bitmap("./Resources/map.png"));

            Bitmap myBitmap = new Bitmap("./Resources/TilesSheet.png");
            Rectangle cloneRect = new Rectangle(0, 96, 96, 96);
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

            Graphics graphic = e.Graphics;
            graphic.Clear(Color.Black);

            oGameForm.Text = "Labo GEI794 – FPS " + Convert.ToString(getFPS());

            e.Graphics.DrawImage(iPlayerImage, new Point(SlimePositionX, SlimePositionY));

            for(int i = 0; i< map.objectMap.Count(); i++)
            {
                e.Graphics.DrawImage(map.objectMap[i].GetImage(), map.objectMap[i].GetPosition());
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

        public void moveRectangle(int x, int y)
        {
            SlimePositionX += x;
            SlimePositionY += y;
        }
                
    }
}
