using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Interfaces
{
    public interface IMovable
    {
        void TurnRight();
        void TurnLeft();
        void MoveForward();
    }
}
