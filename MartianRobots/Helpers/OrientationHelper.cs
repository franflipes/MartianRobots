using System;
using System.Collections.Generic;
using System.Text;
using MartianRobots.Enums;

namespace MartianRobots.Helpers
{
    public class OrientationHelper
    {
        public static Orientation GetOrientation(string orientation)
        {
            switch (orientation) 
            {
                case "N":
                    return Orientation.N;
                case "E":
                    return Orientation.E;
                case "S":
                    return Orientation.S;
                case "W":
                    return Orientation.W;
                default:
                    return Orientation.NA;
            }
        }

        public static Orientation ChangeOrientation(Orientation ini, char turn)
        {

            Orientation newOrientation=Orientation.NA;

            if (ini == Orientation.N)
            {
                if (turn == 'R')
                {
                    newOrientation = Orientation.E;
                }
                else if (turn == 'L')
                {
                    newOrientation = Orientation.W;
                }
            }
            else if (ini == Orientation.E)
            {
                if (turn == 'R')
                {
                    newOrientation = Orientation.S;
                }
                else if (turn == 'L')
                {
                    newOrientation = Orientation.N;
                }
            }
            else if (ini == Orientation.S)
            {
                if (turn == 'R')
                {
                    newOrientation = Orientation.W;
                }
                else if (turn == 'L')
                {
                    newOrientation = Orientation.E;
                }
            }
            else if (ini == Orientation.W)
            {
                if (turn == 'R')
                {
                    newOrientation = Orientation.N;
                }
                else if (turn == 'L')
                {
                    newOrientation = Orientation.S;
                }
            }
            else 
            {
                throw new Exception("Wrong Orientation");
            }

            return newOrientation;
        }

    }
}
