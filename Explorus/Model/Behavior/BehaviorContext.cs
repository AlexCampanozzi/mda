using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model.Behavior
{
    [Serializable]
    public class BehaviorContext
    {
        private IBehavior _behavior;
        private ToxicSlime _slime;
        public BehaviorContext(ToxicSlime slime) { _slime = slime; }
        public BehaviorContext(ToxicSlime slime, IBehavior behavior) 
        { 
            _slime = slime;
            _behavior = behavior; 
        }
        public void SetBehaviorStrategy(IBehavior behavior)
        {
            _behavior = behavior;
        }

        public (Direction, int, int, Direction) choosePath()
        {

            (Direction newDir, int playerPosX, int playerPosY, Direction playerDir)= _behavior.specialBehavior(_slime);
            return (newDir, playerPosX, playerPosY, playerDir);
        }
    }
}
