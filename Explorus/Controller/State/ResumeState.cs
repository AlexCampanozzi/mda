using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Controller
{
    class ResumeState : State
    {
        public ResumeState(GameEngine engine) : base(engine)
        {
            this.engine = engine;
        }

        public override void stateUpdate()
        {
            Thread.Sleep(3000);
            engine.ChangeState(new PlayState(engine));
        }

        public override double Lag(double lag, int MS_PER_UPDATE)
        {
            return lag;
        }
        public override string Name()
        {
            return "Resume";
        }
    }
}
