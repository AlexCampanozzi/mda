using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus
{
    public class IGameEngine
    {
        GameEngine ge = new GameEngine();

        //Facade for the user to start and stop the game

        public void Start()
        {
            ge.Start();
        }

        public void Stop()
        {
            ge.Stop();
        }
    }
}
