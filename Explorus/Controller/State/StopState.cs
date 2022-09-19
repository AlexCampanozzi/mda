﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;



namespace Explorus.Controller
{
    class StopState : State
    {
        public StopState(GameEngine engine) : base(engine)
        {
            this.engine = engine;
        }

        public override void stateUpdate()
        {
            Thread.Sleep(4000);
            Application.Exit();
        }
        public override double Lag(double lag, int MS_PER_UPDATE)
        {
            return lag;
        }
        public override void draw(Graphics graphic, PaintEventArgs e, Image image)
        {
            graphic.Clear(Color.Black);
            e.Graphics.DrawImage(image, new Point(0, 0));
        }

    }
}
