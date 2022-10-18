using Explorus.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Controller
{
    public class PhysicCommand : ICommand
    {
        private PhysicsThread physics = PhysicsThread.GetInstance();
        public PhysicCommand( )
        {
        }
        public void Execute()
        {

            physics.Execute();
        }
        public void Undo()
        {
            /*
            if (game.level > 0)
            {
                game.DownLevel();
                game.DisplayLevel();
            }
            else
            {
                game.Finish();
            }
            */
        }
    }
}
