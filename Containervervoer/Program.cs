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
            var ship = new Ship(5,4, 5);
            Random r = new Random();
            var Containers = new List<Container>();
            for (int i = 0; i < 3; i++)
            {
                var container = new Container(r.Next(4000, 30000), ContainerType.ValuableAndCooled);
                Containers.Add(container);
            }
            for (int i = 0; i < 75; i++)
            {
                var container = new Container(r.Next(4000, 30000), ContainerType.Normal);
                Containers.Add(container);
            }
            for (int i = 0; i < 5; i++)
            {
                var container = new Container(r.Next(4000, 30000), ContainerType.Valuable);
                Containers.Add(container);
            }
            for (int i = 0; i < 12; i++)
            {
                var container = new Container(r.Next(4000, 30000), ContainerType.Cooled);
                Containers.Add(container);
            }
            ContainerDistributor.DistributeContainers(Containers, ship);

            
        }
    }
}
