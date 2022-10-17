using Explorus.Threads;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Explorus.Controller

{
    public class LevelState : PauseState
    {
        

        public LevelState(GameEngine engine) : base(engine)
        {
            this.engine = engine;
        }

        public override string Name()
        {
            return "Level";
        }


    }
}
