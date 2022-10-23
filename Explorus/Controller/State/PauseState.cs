using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Controller
{
    public class PauseState : State
    {
        public PauseState(GameEngine engine) : base(engine)
        {
            this.engine = engine;
        }

        public override void stateUpdate()
        {
            engine.processInput(engine.getsetCurrentInput());
        }

        public override double Lag(double lag, int MS_PER_UPDATE)
        {
            return lag;
        }

        public override string Name()
        {
            return "Pause";
        }

    }
}
