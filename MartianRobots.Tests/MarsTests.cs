using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using MartianRobots.Classes;
using MartianRobots.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MartianRobots.Tests
{
    [TestFixture]
    public class MarsTests:MasterTests
    {
        //private static ServiceProvider _serviceProvider;
        private IBounded _planet;

        [SetUp]
        public void Setup()
        {
            base.Setup();
            _services.AddSingleton<IBounded, Mars>();
            base.EndSetUp();

            _planet = ActivatorUtilities.CreateInstance<Mars>(_serviceProvider, 1, 1);

        }

        [Test]
        public void IsOutOfBounds()
        {
            Tuple<int, int> point = new Tuple<int, int>(2, 1);
            bool isOutOfBounds = _planet.IsOutOfBounds(point);
            Assert.True(isOutOfBounds);

        }

        [Test]
        public void IsNotOutOfBounds()
        {
            Tuple<int, int> point = new Tuple<int, int>(1, 1);
            bool isOutOfBounds = _planet.IsOutOfBounds(point);
            Assert.False(isOutOfBounds);

        }
    }

}
