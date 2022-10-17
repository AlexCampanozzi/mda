using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model.Behavior
{
    public class PursuitStrategy : IBehavior
    {
        Behaviors behaviors = new Behaviors();

        public (Direction, int, int, Direction) specialBehavior(ToxicSlime slime)
        {
            (Direction newDir, int playerPosX, int playerPosY, Direction playerDir) = behaviors.pursuit(slime, true);

            if (newDir.X == 0 && newDir.Y == 0)
            {
                newDir = behaviors.random(slime, slime.lastDirection);
            }

            slime.lastDirection = newDir;

            return (newDir, playerPosX, playerPosY, playerDir);
        }

    }
}
