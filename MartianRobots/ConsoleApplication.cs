using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using MartianRobots.Interfaces;

namespace MartianRobots
{
    class ConsoleApplication
    {
        private readonly IPlanetManager _manager;
        public ConsoleApplication(IPlanetManager manager)
        {
            _manager = manager;
        }

        public void Run(string[] args)
        {
            string fileName = args[0];

            int lineNumber = 0;
            int nRobots = 0;


            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line;


                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;
                    if (lineNumber == 1)
                    {
                        var boundaries = line.Split(" ");
                        _manager.CreatePlanet(int.Parse(boundaries[0]), int.Parse(boundaries[1]));
                    }
                    else if (lineNumber % 2 == 0)
                    {
                        nRobots++;
                        var pos_ori = line.Split(" ");
                        _manager.CreateRobot(nRobots, int.Parse(pos_ori[0]), int.Parse(pos_ori[1]), pos_ori[2]);
                    }
                    else if (lineNumber != 1 && lineNumber % 2 == 1)
                    {
                        _manager.MoveRobot(line);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
