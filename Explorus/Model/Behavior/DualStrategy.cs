using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model.Behavior
{
    public class DualStrategy : IBehavior
    {
        (Direction, int, int, Direction) IBehavior.specialBehavior(ToxicSlime slime)
        {
            throw new NotImplementedException();
        }
    }
}
