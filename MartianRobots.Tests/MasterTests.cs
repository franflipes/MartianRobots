using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;

namespace MartianRobots.Tests
{
    
    public class MasterTests
    {
        protected static ServiceProvider _serviceProvider;
        protected static IServiceCollection _services;

        [SetUp]
        public void Setup()
        {
            _services = new ServiceCollection();
        }

        protected void EndSetUp()
        {
            _serviceProvider = _services.BuildServiceProvider(true);
        }
    }
}
