using MartianRobots.Classes;
using MartianRobots.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MartianRobots.Helpers
{
    public class InstructionManager
    {
        List<IInstruction> _instructions = new List<IInstruction>();

        public InstructionManager()
        {
            _instructions.Add(
                new InstructionCode()
                {
                    Key = 'R',
                    Description = "Turn Right",
                    Code = new Action<Robot, IList<Scent>, Mars>(
                        (x,y,z) =>
                        {
                                x.TurnRight();
                        }
                    )
                }
            );

            _instructions.Add(
                new InstructionCode()
                {
                    Key = 'L',
                    Description = "Turn Left",
                    Code = new Action<Robot, IList<Scent>, Mars>(
                        (x,y,z) =>
                        {
                            x.TurnLeft();
                        }
                    )
                }
            );

            _instructions.Add(
                new InstructionCode()
                {
                    Key = 'F',
                    Description = "Move Forward",
                    Code = new Action<Robot, IList<Scent>, Mars>(
                        (r,s,m) =>
                        {
                            if (s.Any(s => s.Node.Item1 == m.GetRobotLastKnownPosition(r).Item1 && s.Node.Item2 == m.GetRobotLastKnownPosition(r).Item2 && s.Node.Item3 == r.Orientation))
                                return;
                            r.MoveForward();

                            //once robot´s moved forward, we are in good place to ask the planet if the robot´s alive,
                            //if not we use last'known position and same orientation to save as a scent and return because the robot caused lost
                            //if (_currentRobot.IsLost)
                            if (!m.IsRobotAlive(r))
                            {
                                Console.WriteLine(m.GetRobotLastKnownPosition(r).Item1 + " " + m.GetRobotLastKnownPosition(r).Item2 + " " + r.Orientation + " LOST");
                                s.Add(new Scent(m.GetRobotLastKnownPosition(r).Item1, m.GetRobotLastKnownPosition(r).Item2, r.Orientation));
                                return;
                            }
                        }
                    )
                }
            );

            
        }


        public void ExecuteInstruction(Robot r, IList<Scent> s,Mars m,char c) 
        {
            IInstruction instruction = _instructions.Find(x => ((Instruction)x).Key == c);

            instruction.ExecuteInstruction(r, s, m);
        }

        internal void LoadUpInstructions(string instructionsFile)
        {
            JsonReader<InstructionCustom> jsonReader = new JsonReader<InstructionCustom>(instructionsFile);
            List<IInstruction>  list = jsonReader.ReadListObjects().ToList<IInstruction>();
            _instructions.AddRange(list);
        }
    }
}
