using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Controller
{
    class PlayState : State
    {
        public PlayState(GameEngine engine) : base(engine)
        {
            this.engine = engine;

        }

        public override void stateUpdate()
        {
            engine.processInput();
        }

        public override double Lag(double lag, int MS_PER_UPDATE)
        {
            if (lag >= MS_PER_UPDATE)
            {
                //while (lag >= MS_PER_UPDATE)
                //{
                    engine.update();
                    lag -= MS_PER_UPDATE;
                //}
            }
            
            return lag;
        }
        public override string Name()
        {
            return "Play";
        }
    }
}
