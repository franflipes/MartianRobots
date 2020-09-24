using MartianRobots.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Classes
{
    public class Scent
    {
        
        public Tuple<int, int, Orientation> Node { get; set; }
        public Scent( int x, int y,Orientation ori)
        {
            Node = new Tuple<int, int, Orientation>(x, y, ori);
        }


    }
}
