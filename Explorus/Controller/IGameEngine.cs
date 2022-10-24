using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Controller
{
    public class IGameEngine
    {
        GameEngine ge = GameEngine.GetInstance();

        //Facade for the user to start and stop the game

        public void Start(bool windowless)
        {
            ge.Start(windowless);
        }

        public void Stop()
        {
            ge.Stop();
        }
    }
}
