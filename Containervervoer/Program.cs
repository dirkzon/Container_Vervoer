using System;
using System.Collections.Generic;
using Containervervoer.Logic;
using Containervervoer.Logic.Logic;

namespace Containervervoer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ship width,length,height");
            string result = Console.ReadLine();
            string[] values = result.Split(",");

            var ship = new Ship(Convert.ToInt32(values[0]), Convert.ToInt32(values[0]), Convert.ToInt32(values[0]));
            Random r = new Random();
            var Containers = new List<Container>();
            for (int i = 0; i < 5; i++)
            {
                var container = new Container(r.Next(4000, 30000), ContainerType.ValuableAndCooled);
                Containers.Add(container);
            }
            for (int i = 0; i < 200; i++)
            {
                var container = new Container(r.Next(4000, 30000), ContainerType.Normal);
                Containers.Add(container);
            }
            for (int i = 0; i < 5; i++)
            {
                var container = new Container(r.Next(4000, 30000), ContainerType.Valuable);
                Containers.Add(container);
            }
            for (int i = 0; i < 30; i++)
            {
                var container = new Container(r.Next(4000, 30000), ContainerType.Cooled);
                Containers.Add(container);
            }
            ContainerDistributor.DistributeContainers(Containers, ship);     
        }
    }
}
