using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model.Behavior
{
    [Serializable]
    public class AmbushStrategy : IBehavior
    {
        Behaviors behaviors = new Behaviors();
        public (Direction, int, int, Direction) specialBehavior(ToxicSlime slime)
        {
            Direction newDir, playerDir = new Direction(0, 0);
            int playerPosX = 0, playerPosY = 0;
            bool found;

            (found, newDir) = behaviors.ambush(slime);
            if (found)
            {
                newDir = behaviors.random(slime, newDir);
            }
            else
            {
                newDir = behaviors.random(slime, slime.getLastDirection());
            }

            return (newDir, playerPosX, playerPosY, playerDir);
        }
    }
}
