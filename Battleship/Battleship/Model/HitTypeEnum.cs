using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Model
{
    public enum HitTypeEnum
    {
        Miss = 0,
        Hit = 1,
        Sink = 2,
        AlreadyHit
    }
}
