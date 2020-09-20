using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Interfaces
{
    public interface IPlanetManager
    {
        public void CreatePlanet(int boundaryRight, int boundaryTop);
        public void CreateRobot(int nRobots, int posX, int posY, string orientation);

        public void MoveRobot(string instructions);
    }
}
