using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MartianRobots.Classes;
using MartianRobots.Enums;
using MartianRobots.Helpers;
using MartianRobots.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MartianRobots
{
    //Manager class manage the different entities Mars and Robots, and help them to interact each other mostly when it´s time for the Robot to move around Mars
    public class MarsManager:IPlanetManager
    {
        //Planet Mars is created by the manager
        private Mars mars;

        //we keep track of the current robot as we deal with many of them along the input processing
        private Robot _currentRobot;
        private readonly IServiceProvider _service;

        //List of position plus orientation that a move forward would cause to lose a robot
        private IList<Tuple<int, int, Orientation>> _scents;
        

        public MarsManager(IServiceProvider service)
        {
            _service = service;            
            _scents = new List<Tuple<int, int, Orientation>>();
        }

        #region Manage to create Planet and Robots
        public void CreatePlanet(int boundaryRight, int boundaryTop) 
        {
            mars = ActivatorUtilities.CreateInstance<Mars>(_service,boundaryRight,boundaryTop);
        }

        public void CreateRobot(int nRobots,int posX, int posY, string orientation)
        {
            Robot robot = ActivatorUtilities.CreateInstance<Robot>(_service, "robot" + nRobots, OrientationHelper.GetOrientation(orientation));
            //Robot robot = new Robot("robot" + nRobots,new Tuple<int, int>(posX, posY),OrientationHelper.GetOrientation(orientation));
            _currentRobot = robot;
            ((Mars)mars).ListOfRobots.Add((Robot)_currentRobot);
            ((Mars)mars).AddRobot(robot, new Tuple<int, int>(posX, posY));



        }
        #endregion

        public void MoveRobot(string instructions)
        {
            char[] instructionsArray = instructions.ToCharArray();

            //loop through instructions
            foreach (char instruction in instructionsArray)
            {
                if (instruction == 'R')
                {
                    _currentRobot.TurnRight();
                }
                else if (instruction == 'L')
                {
                    _currentRobot.TurnLeft();
                }
                else if (instruction == 'F')
                {
                    //If exists any scent with same position and orientation, then a move forward would cause in a robot loss, 
                    //so we disobey and continue with next instruction
                    if (_scents.Any(s => s.Item1 == mars.GetRobotLastKnownPosition(_currentRobot).Item1 && s.Item2 == mars.GetRobotLastKnownPosition(_currentRobot).Item2 && s.Item3 == _currentRobot.Orientation))
                        continue;
                     _currentRobot.MoveForward();

                    //once robot´s moved forward, we are in good place to ask the planet if the robot´s alive,
                    //if not we use last'known position and same orientation to save as a scent and return because the robot caused lost
                    //if (_currentRobot.IsLost)
                    if (!mars.IsRobotAlive(_currentRobot))
                    {
                        Console.WriteLine(mars.GetRobotLastKnownPosition(_currentRobot).Item1 + " " + mars.GetRobotLastKnownPosition(_currentRobot).Item2 + " " + _currentRobot.Orientation + " LOST");
                        _scents.Add(new Tuple<int, int, Orientation>(mars.GetRobotLastKnownPosition(_currentRobot).Item1, mars.GetRobotLastKnownPosition(_currentRobot).Item2, _currentRobot.Orientation));
                        return;
                    }
                }
            }
            Console.WriteLine(mars.GetRobotLastKnownPosition(_currentRobot).Item1 + " " + mars.GetRobotLastKnownPosition(_currentRobot).Item2 + " " + _currentRobot.Orientation );
        }
    }
}
