using System;
using System.Collections.Generic;
using System.Text;
using MartianRobots.Interfaces;

namespace MartianRobots.Classes
{
    public class Mars : Planet 
    {

        #region ctor
        public Mars(int boundaryTop, int boundaryRight):base(boundaryTop, boundaryRight)
        {
            
            ListOfRobots = new List<Robot>();   
        }
        #endregion

        public List<Robot> ListOfRobots { get; set; }

        public Tuple<int, int> GetRobotLastKnownPosition(IMovable robot)
        {
            return _surface.GetObjectLastKnownPosition(robot);
        }

    }
}
