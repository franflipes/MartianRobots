using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Interfaces
{
    public interface IBounded
    {
        Boolean IsOutOfBounds(Tuple<int, int> pos);
    }
}
