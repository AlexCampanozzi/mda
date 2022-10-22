using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model.Behavior
{
    public class AmbushStrategy : IBehavior
    {
        Behaviors behaviors = new Behaviors();
        public (Direction, int, int, Direction) specialBehavior(ToxicSlime slime)
        {
            Direction newDir, playerDir = new Direction(0, 0);
            int playerPosX = 0, playerPosY = 0;

            newDir = behaviors.ambush(slime);
            if (newDir == null)
            {
                newDir = behaviors.random(slime, slime.getLastDirection());
            }

            return (newDir, playerPosX, playerPosY, playerDir);
        }
    }
}
