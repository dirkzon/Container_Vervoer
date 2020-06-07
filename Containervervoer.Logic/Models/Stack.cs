using System.Collections.Generic;

namespace Containervervoer.Logic
{
    public class Stack
    {
        public List<Container> Containers = new List<Container>();
        public int XPosition { get; set; }
        public bool ContainsValuable { get; set; }
        public int StackWeight { get; set; }

        public Stack(int position)
        {
            XPosition = position + 1;
        }

        public void LoadContainer(Container container)
        {
            if (container.Type == ContainerType.ValuableAndCooled || container.Type ==  ContainerType.Valuable) 
            {
                ContainsValuable = true;
            }
            IncreaseHeightOfContainers();
            container.Height = 0;
            Containers.Add(container);
            StackWeight += container.Weight;
        }

        private void IncreaseHeightOfContainers()
        {
            foreach (var container in Containers)
            {
                container.Height++;
            }
        }

        public bool CheckIfWeightCanFit(Container container)
        {
            int weightOnFirstContainer = GetWeightOnFirstContainer();
            if (weightOnFirstContainer + container.Weight <= 120000)
            {
                return true;
            }
            return false;
        }

        private int GetWeightOnFirstContainer()
        {
            var firstContainer = Containers.Find(c => c.Height == 0);
            if (firstContainer == null)
            {
                return 0;
            }
            return StackWeight - firstContainer.Weight;
        }

        public bool CheckIfHeightCanFit(int maxHeight)
        {
            if (Containers.Count + 1 > maxHeight)
            {
                return false;
            }

            return true;
        }

        public string PrintStackPositions()
        {
            string output = string.Empty;
            for (int i = Containers.Count; i-- > 0;)
            {
                output += Containers[i].GetType();
                if (i != 0)
                {
                    output += "-";
                }
            }

            return output;
        }

        public string PrintStackWeights()
        {
            string output = string.Empty;
            for (int i = Containers.Count; i-- > 0;)
            {
                output += Containers[i].GetWeight();
                if (i != 0)
                {
                    output += "-";
                }
            }

            return output;
        }
    }
}
