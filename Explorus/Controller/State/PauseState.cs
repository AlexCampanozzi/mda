using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Controller
{
    class PauseState : State
    {
        public PauseState(GameEngine engine) : base(engine)
        {
            this.engine = engine;
        }

        public override void stateUpdate()
        {
            engine.processInput();
        }

        public override double Lag(double lag, int MS_PER_UPDATE)
        {
            return lag;
        }

        public override string Name()
        {
            return "Pause";
        }

/*
        delegate void SetTextCallback();
        public override void SetText()
        {
            if (GameView.Instance.getGameForm().InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                GameView.Instance.getGameForm().Invoke(d, new object[] {"Pause"});
            }

            else
            {
                GameView.Instance.getGameForm().Text = "Pause";
            }

        }*/
    }
}
