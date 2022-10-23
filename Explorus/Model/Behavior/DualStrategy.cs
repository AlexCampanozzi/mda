using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model.Behavior
{
    [Serializable]
    public class DualStrategy : IBehavior
    {
        Behaviors behaviors = new Behaviors();
        (Direction, int, int, Direction) IBehavior.specialBehavior(ToxicSlime slime)
        {
            (_, bool SlimusFound, _, _, _) = behaviors.findPlayer(slime);
            Direction newDir = new Direction(0,0), playerDir = new Direction(0,0);
            int playerPosX = 0, playerPosY = 0;

            if (SlimusFound)
            {
                if (!slime.behaviorTimer.IsRunning)
                {
                    slime.behaviorTimer.Start();
                    Random rnd = new Random();
                    slime.setPursuit(rnd.Next(2) < 1);
                    (newDir, playerPosX, playerPosY, playerDir) = behaviors.pursuit(slime, slime.getPursuit());
                }
                else if (slime.behaviorTimer.ElapsedMilliseconds >= 5000)
                {
                    slime.behaviorTimer.Reset();
                    slime.behaviorTimer.Start();
                    Random rnd = new Random();
                    slime.setPursuit(rnd.Next(2) < 1);
                    (newDir, playerPosX, playerPosY, playerDir) = behaviors.pursuit(slime, slime.getPursuit());
                }
                else
                {
                    (newDir, playerPosX, playerPosY, playerDir) = behaviors.pursuit(slime, slime.getPursuit());
                }
            }
            else if (slime.behaviorTimer.IsRunning)
            {
                if (slime.behaviorTimer.ElapsedMilliseconds >= 5000)
                {
                    slime.behaviorTimer.Reset();
                }
                else (newDir, playerPosX, playerPosY, playerDir) = behaviors.pursuit(slime, slime.getPursuit());
            }
            // continue until wall
            if (newDir.X == 0 && newDir.Y == 0)
            {
                newDir = behaviors.random(slime, slime.getLastDirection());
            }
            return (newDir, playerPosX, playerPosY, playerDir);
        }
    }
}
