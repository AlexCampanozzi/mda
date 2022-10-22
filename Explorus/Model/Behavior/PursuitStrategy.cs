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

            if (newDir == null)
            {
                newDir = behaviors.random(slime, slime.getLastDirection());
            }

            return (newDir, playerPosX, playerPosY, playerDir);
        }

    }
}
