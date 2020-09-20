using System;
using System.Collections.Generic;
using System.Text;
using MartianRobots.Helpers;

namespace MartianRobots.Interfaces
{
    public interface IMovable
    {
        void TurnRight();
        void TurnLeft();
        void MoveForward();

        public event Delegates.Notify MoveForwardEvent;
    }
}
