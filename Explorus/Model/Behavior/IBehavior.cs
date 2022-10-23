using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model.Behavior
{

    public interface IBehavior
    {
        (Direction, int, int, Direction) specialBehavior(ToxicSlime slime);
    }
}
