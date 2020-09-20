using System;
using System.Collections.Generic;
using System.Text;
using MartianRobots.Classes;
using MartianRobots.Enums;

namespace MartianRobots.Helpers
{
    public static class Delegates
    {
        public delegate void Notify(Robot robot, Orientation orientation);
    }
}
