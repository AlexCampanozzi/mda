﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Controller
{
    class StartState : PauseState
    {
        public StartState(GameEngine engine) : base(engine)
        {
            this.engine = engine;
        }

        public override void stateUpdate()
        {
            engine.processInput(GameView.Instance.getCurrentInput());
        }

        public override double Lag(double lag, int MS_PER_UPDATE)
        {
            return lag;
        }
        public override string Name()
        {
            return "Start";
        }
    }
}
