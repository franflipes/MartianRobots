using MartianRobots.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MartianRobots.Classes
{
    public class Surface
    {
        

        //The current and real position of the robot, modeled as an Array because we can raise exceptions 
        //if robot goes out-of-bounds 
        private Robot[,] _surface;

        //This is redundant to the Array until the robot gets lost(doesn´t exist in the array), 
        //at this point the position becomes the Last-Known position
        //It´s easier to work with and to get a position for a robot quicker than using the Array above
        private List<Tuple<Robot, Tuple<int, int>>> _listOfRobotPosition;
        public Surface(int boundaryX, int boundaryY)
        {
           _surface= new Robot[boundaryX + 1, boundaryY + 1];
            _listOfRobotPosition = new List<Tuple<Robot, Tuple<int, int>>>();
        }

        //Add a robot to the surface(Array), subscribe to the robot´s movement to make the move in the Arrays 
        //and also add the robot to the List
        public void AddRobot(Robot robot, Tuple<int,int> pos)
        {
            _surface[pos.Item1, pos.Item2] = robot;
            robot.MoveForwardEvent += MoveRobot;
            _listOfRobotPosition.Add(new Tuple<Robot, Tuple<int, int>>(robot, pos));

        }

        //Ask to the surface there is any robot on the Array at some position.
        public bool ExistAnyRobotOnSurface(Tuple<int, int> pos) 
        {
            return _surface[pos.Item1, pos.Item2] == null ? false : true;
                
        }

        //function that moves the robot in the surface array and update the list depending the Orientation.
        //If the robot goes out-of-bounds then it throws an outOfRangeException, but we do not update the list, 
        //keeping this one as the Last-Known position(current or previuos one) 
        public void MoveRobot(Robot robot,Orientation ori)
        {
            int index = _listOfRobotPosition.FindIndex(x => x.Item1 == robot);
            Tuple<int, int> currentPos = _listOfRobotPosition[index].Item2;
            Tuple<int, int> newPos = null;

            try
            {
                //The idea is throwing an exception if next position it is out of bounds, and set robot to null on this position,
                //but keep the position in listOfRobotPosition as a remainder of the previous
                //We can later on get position from list and ask to surface array to find out a robot is lost
                if (ori == Orientation.N)
                {
                    newPos = new Tuple<int, int>(currentPos.Item1, currentPos.Item2 + 1);
                }
                else if (ori == Orientation.E)
                {
                    newPos = new Tuple<int, int>(currentPos.Item1+1, currentPos.Item2);
                }
                else if (ori == Orientation.S)
                {
                    newPos = new Tuple<int, int>(currentPos.Item1, currentPos.Item2 - 1);
                }
                else if (ori == Orientation.W)
                {
                    newPos = new Tuple<int, int>(currentPos.Item1-1, currentPos.Item2);
                }
                //_listOfRobotPosition.Remove(_listOfRobotPosition.First(x => x.Item1 == robot));
                _surface[newPos.Item1, newPos.Item2] = robot;
                _surface[currentPos.Item1, currentPos.Item2] = null;
                _listOfRobotPosition[index] = new Tuple<Robot, Tuple<int, int>>(robot, new Tuple<int, int>(newPos.Item1, newPos.Item2));

            }
            catch (IndexOutOfRangeException e)
            {
                _surface[currentPos.Item1, currentPos.Item2] = null;
                throw e;
            }
            
        }

        //We return the last known position that comes from the List, not the Array, 
        //last known position could be current position or position right before the robot gets lost

        internal Tuple<int, int> GetRobotLastKnownPosition(Robot robot)
        {
            return _listOfRobotPosition.First(x => x.Item1 == robot).Item2;
        }
    }
}
