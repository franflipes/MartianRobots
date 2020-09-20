using MartianRobots.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Classes
{
    public class Planet:IBounded
    {

        protected Surface _surface;
        public Planet(int boundaryRight, int boundaryTop)
        {

            _surface = new Surface( boundaryRight, boundaryTop); 
            _boundaries = new Tuple<int, int>(boundaryTop, boundaryRight);
        }
        protected Tuple<int,int> _boundaries;

        #region Public Methods
        //Public boolean method that responds true/false if a position is out-of-bounds or not
        public Boolean IsOutOfBounds(Tuple<int, int> pos)
        {
            if (pos.Item1 < 0 || pos.Item1 > _boundaries.Item1 || pos.Item2 < 0 || pos.Item2 > _boundaries.Item2)
                return true;
            return false;
        }

        public Boolean IsRobotAlive(Robot robot)
        {
            Tuple<int, int>  pos = _surface.GetObjectLastKnownPosition(robot);

            return _surface.ExistAnyObjectOnSurface(pos) ? true : false;
            
        }

        public void AddRobot(Robot robot, Tuple<int, int> initialPosition)
        {
            _surface.AddObject(robot, initialPosition);
        }


        //We ask the surface for the last known position of a given robot
       
        #endregion
    }
}
