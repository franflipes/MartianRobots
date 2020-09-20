using NUnit.Framework;
using MartianRobots.Classes;
using MartianRobots.Interfaces;
using MartianRobots.Enums;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace MartianRobots.Tests
{
    [TestFixture]
    public class RobotTests:MasterTests
    {
        //private static ServiceProvider _serviceProvider;
        private IMovable _robotPointingNorth;
        private IMovable _robotPointingWrong;
        [SetUp]
        public void Setup()
        {
            base.Setup();
            //var services = new ServiceCollection();
            _services.AddSingleton<IMovable, Robot>();
            //_serviceProvider = services.BuildServiceProvider(true);
            base.EndSetUp();
            Tuple<int, int> initial_pos = new Tuple<int, int>(0, 0);
            Orientation orientation = Orientation.N;
            Orientation wrongOrientation = Orientation.NA;


            
            _robotPointingNorth = ActivatorUtilities.CreateInstance<Robot>(_serviceProvider,"Robot1",  orientation);

            _robotPointingWrong = ActivatorUtilities.CreateInstance<Robot>(_serviceProvider, "Robot2", wrongOrientation);
        }

        [Test]
        public void TurnLeftFromNorthToWest()
        {
            _robotPointingNorth.TurnLeft();
            Assert.AreEqual(Orientation.W,((Robot)_robotPointingNorth).Orientation);
        }

        [Test]
        public void TurnRightFromNorthToEast()
        {
            _robotPointingNorth.TurnRight();
            Assert.AreEqual(Orientation.E, ((Robot)_robotPointingNorth).Orientation);
        }

        [Test]
        public void TurnAround()
        {
            _robotPointingNorth.TurnRight();
            _robotPointingNorth.TurnRight(); 
            _robotPointingNorth.TurnRight();
            _robotPointingNorth.TurnRight();
            Assert.AreEqual(Orientation.N, ((Robot)_robotPointingNorth).Orientation);
        }


        //[Test]
        //public void MoveForward()
        //{
        //    _robotPointingNorth.MoveForward();
        //    Assert.Greater( ((Robot)_robotPointingNorth).Position.Item2, ((Robot)_robotPointingNorth).PreviousPosition.Item2);
            
        //}

        [Test]
        public void TurnThrowsAnException()
        {
            Assert.Catch<Exception>(() => _robotPointingWrong.TurnRight());
        }
    }
}
