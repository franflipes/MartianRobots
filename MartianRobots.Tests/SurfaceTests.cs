using System;
using System.Collections.Generic;
using System.Text;
using MartianRobots.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

using MartianRobots.Classes;
using MartianRobots.Enums;

namespace MartianRobots.Tests
{
    [TestFixture]
    public class SurfaceTests:MasterTests
    {
        private Surface _surface;
        private Robot _robotPointingNorth;

        [SetUp]
        public void Setup()
        {
            base.Setup();
            //var services = new ServiceCollection();
            _services.AddSingleton<IMovable, Robot>();
            _services.AddSingleton<Surface>();
            //_serviceProvider = services.BuildServiceProvider(true);
            base.EndSetUp();

            Orientation orientation = Orientation.N;

            _robotPointingNorth = ActivatorUtilities.CreateInstance<Robot>(_serviceProvider, "Robot1", orientation);
            _surface = new Surface(1, 1);
           
        }

        [Test]
        public void MoveRobotForwardOnEdgeCauseException()
        {
            _surface.AddRobot((Robot)_robotPointingNorth, new Tuple<int, int>(1, 1));
           
            Assert.Throws<IndexOutOfRangeException>(() => {
                _surface.MoveRobot(_robotPointingNorth, Orientation.N);
            });


        }

        [Test]
        public void ExistRobotOnSurface()
        {
            _surface.AddRobot((Robot)_robotPointingNorth, new Tuple<int, int>(1, 1));

            bool existsRobot=_surface.ExistAnyRobotOnSurface(new Tuple<int, int>(1, 1));

            Assert.IsTrue(existsRobot);


        }
    }
}
