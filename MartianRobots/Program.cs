using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using MartianRobots.Classes;
using MartianRobots.Interfaces;

namespace MartianRobots
{
    class Program
    {
        private static ServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            RegisterServices();
            IServiceScope scope = _serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<ConsoleApplication>().Run(args);
            DisposeServices();

            

            //string fileName = args[0];

            //int lineNumber = 0;
            //int nRobots=0;

            //MarsManager manager = new MarsManager();

            //FileStream fileStream = new FileStream(fileName, FileMode.Open);
            //using (StreamReader reader = new StreamReader(fileStream))
            //{
            //    string line;
                

            //    while ((line = reader.ReadLine()) != null)
            //    {
            //        lineNumber++;
            //        if (lineNumber == 1)
            //        {
            //            var boundaries = line.Split(" ");
            //            manager.CreatePlanet(int.Parse(boundaries[0]), int.Parse(boundaries[1]));
            //        }
            //        else if (lineNumber % 2 == 0)
            //        {
            //            nRobots++;
            //            var pos_ori = line.Split(" ");
            //            manager.CreateRobot(nRobots,int.Parse(pos_ori[0]), int.Parse(pos_ori[1]), pos_ori[2]);
            //        }
            //        else if (lineNumber != 1 && lineNumber % 2 == 1)
            //        {
            //            manager.MoveRobot(line);
            //        }   
            //    }   
            //}
            //Console.ReadLine();
            
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IBounded, Mars>();
            services.AddSingleton<IMovable, Robot>();
            services.AddSingleton<IPlanetManager, MarsManager>();
            services.AddSingleton<Surface>();
            services.AddSingleton<ConsoleApplication>();
            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
