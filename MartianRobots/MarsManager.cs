using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly InstructionManager _instructionManager;

        //List of position plus orientation that a move forward would cause to lose a robot
        private IList<Scent> _scents;
        

        public MarsManager(IServiceProvider service,IConfiguration configuration, InstructionManager instructionManager)
        {
            _service = service;            
            _scents = new List<Scent>();
            _instructionManager = instructionManager;
            _configuration = configuration;
            String instructionsFile=_configuration["Instructions"];
            _instructionManager.LoadUpInstructions(instructionsFile);
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
            mars.ListOfRobots.Add((Robot)_currentRobot);
            mars.AddRobot(robot, new Tuple<int, int>(posX, posY));



        }
        #endregion

        public void MoveRobot(string instructions)
        {
            char[] instructionsArray = instructions.ToCharArray();

            //loop through instructions
            foreach (char instruction in instructionsArray)
            {
                _instructionManager.ExecuteInstruction(_currentRobot,_scents,mars, instruction);
                //if (instruction == 'F')
                //{
                //    //If exists any scent with same position and orientation, then a move forward would cause in a robot loss, 
                //    //so we disobey and continue with next instruction
                //    if (_scents.Any(s => s.Node.Item1 == mars.GetRobotLastKnownPosition(_currentRobot).Item1 && s.Node.Item2 == mars.GetRobotLastKnownPosition(_currentRobot).Item2 && s.Node.Item3 == _currentRobot.Orientation))
                //        continue;
                //    _instructionManager.ExecuteInstruction(_currentRobot, instruction);

                //    //once robot´s moved forward, we are in good place to ask the planet if the robot´s alive,
                //    //if not we use last'known position and same orientation to save as a scent and return because the robot caused lost
                //    //if (_currentRobot.IsLost)
                //    if (!mars.IsRobotAlive(_currentRobot))
                //    {
                //        Console.WriteLine(mars.GetRobotLastKnownPosition(_currentRobot).Item1 + " " + mars.GetRobotLastKnownPosition(_currentRobot).Item2 + " " + _currentRobot.Orientation + " LOST");
                //        _scents.Add(new Scent(mars.GetRobotLastKnownPosition(_currentRobot).Item1, mars.GetRobotLastKnownPosition(_currentRobot).Item2, _currentRobot.Orientation));
                //        return;
                //    }
                //}
                //else 
                //{
                //    _instructionManager.ExecuteInstruction(_currentRobot, instruction);
                //}
                if (_currentRobot.IsLost)
                    return;
            }
            Console.WriteLine(mars.GetRobotLastKnownPosition(_currentRobot).Item1 + " " + mars.GetRobotLastKnownPosition(_currentRobot).Item2 + " " + _currentRobot.Orientation );
        }
    }
}
