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

        //plaatst een container in de stack. de containers worden van onder geladen
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

        //verhoogt de positie van alle containers
        private void IncreaseHeightOfContainers()
        {
            foreach (var container in Containers)container.Height++;
        }

        //checkt of de container in de stack kan geplaatst kan worden aan de hand van gewicht
        public bool CheckIfWeightCanFit(Container container)
        {
            int weightOnFirstContainer = GetWeightOnFirstContainer();
            if (weightOnFirstContainer + container.Weight <= 120000) return true;
            return false;
        }

        //berekent het gewicht op de laagste container
        private int GetWeightOnFirstContainer()
        {
            var firstContainer = Containers.Find(c => c.Height == 0);
            if (firstContainer == null) return 0;
            return StackWeight - firstContainer.Weight;
        }

        //checkt of de container in de stack kan geplaatst kan worden aan de hand van de maximale hoogte
        public bool CheckIfHeightCanFit(int maxHeight)
        {
            if (Containers.Count + 1 > maxHeight) return false;
            return true;
        }

        //print de posities van de container in een stack
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

        //print de gewichten van de container in een stack
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
