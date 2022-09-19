using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Controller
{
    abstract public class State
    {
        protected GameEngine engine;

        public State (GameEngine engine)
        {
            this.engine = engine;
        }

        public abstract void stateUpdate();
        public abstract double Lag(double lag, int MS_PER_UPDATE);
    }
}
