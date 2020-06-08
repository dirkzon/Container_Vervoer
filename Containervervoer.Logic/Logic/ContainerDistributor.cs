using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Containervervoer.Logic.Logic
{
    public static class ContainerDistributor
    {
        public static void DistributeContainers(List<Container> containers, Ship ship)
        {

            var cooledContainers = new List<Container>();    
            var valuableContainers = new List<Container>();
            var cooledValuableContainers = new List<Container>();
            var normalContainers = new List<Container>();

            foreach (var container in containers)
            {
                switch (container.Type)
                {
                    case ContainerType.Cooled:
                        cooledContainers.Add(container);
                        break;
                    case ContainerType.Valuable:
                        valuableContainers.Add(container);
                        break;
                    case ContainerType.ValuableAndCooled:
                        cooledValuableContainers.Add(container);
                        break;
                    case ContainerType.Normal:
                        normalContainers.Add(container);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            cooledContainers = OrderContainersByWeight(cooledContainers);
            valuableContainers = OrderContainersByWeight(valuableContainers);
            cooledValuableContainers = OrderContainersByWeight(cooledValuableContainers);
            normalContainers = OrderContainersByWeight(normalContainers);

            static List<Container> OrderContainersByWeight(List<Container> containerList)
            {
                return containerList.OrderBy(c => c.Weight).ToList();
            }

            foreach (var container in cooledValuableContainers.Where(container => !ship.LoadCooledValuableContainer(container)))
            {
                ShowError(container);
            }

            foreach (var container in valuableContainers.Where(container => !ship.LoadValuableContainer(container)))
            {
                ShowError(container);
            }

            foreach (var container in cooledContainers.Where(container => !ship.LoadCooledContainer(container)))
            {
                ShowError(container);
            }

            foreach (var container in normalContainers.Where(container => !ship.LoadNormalContainer(container)))
            {
                ShowError(container);
            }

            if (ship.CheckIfShipIsBalanced())
            {
                if (ship.CheckIfShipHasEnoughWeight())
                {
                    var process = new ProcessStartInfo(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe")
                    {
                        Arguments = ship.PrintShip()
                    };
                    Process.Start(process);
                }
                else
                {
                    Console.WriteLine("Ship does not have enough weight");
                }
            }
            else
            {
                Console.WriteLine("Ship is out of balance");
            }
        }

        private static void ShowError(Container c)
        {
            Console.WriteLine($"Could not Load container of type:{c.Type}, and weight:{c.Weight}");
        }
    }
}
