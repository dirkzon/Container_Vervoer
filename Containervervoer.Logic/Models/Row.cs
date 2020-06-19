using System;
using System.Collections.Generic;
using System.Linq;

namespace Containervervoer.Logic
{
    public class Row
    {
        public List<Stack> Stacks = new List<Stack>();
        public int RowWeight { get; set; }
        public int Position { get; set; }
        public RowTpe RowType { get; set; }
        public int MaxHeight { get; set; }

        public Row(int position, RowTpe type, int length, int height)
        {
            Position = position;
            RowType = type;
            MaxHeight = height;
            CreateStacks(length);
        }

        //maakt aan de hand van de breete van het schip de stacks aan
        private void CreateStacks(int width)
        {
            for (var i = 0; i < width; i++)
            {
                Stacks.Add(new Stack(i));
            }
        }

        //sorteerd alle stacks in de row op gewicht van lichtste naar zwaarste
        public IEnumerable<Stack> GetStacksByWeight()
        {
            return Stacks.OrderBy(stack => stack.StackWeight);
        }

        //probeert een niet waardevolle container te plaatsen
        public bool PlaceNonValuableContainer(Container container)
        {
            var sortedStacks = GetStacksByWeight();
            foreach (var stack in sortedStacks)
            {
                if (stack.CheckIfWeightCanFit(container) 
                    && stack.CheckIfHeightCanFit(MaxHeight))
                {
                    stack.LoadContainer(container);
                    UpdateWeight();
                    return true;
                }
            }
            return false;
        }

        //probeert een waardevolle container te plaatsen
        public bool PlaceValuableContainer(Container container)
        {
            var sortedStacks = GetStacksByWeight();
            foreach (var stack in sortedStacks)
            {
                if (stack.ContainsValuable != true 
                    && stack.CheckIfWeightCanFit(container) 
                    && stack.CheckIfHeightCanFit(MaxHeight))
                {
                    stack.LoadContainer(container);
                    UpdateWeight();
                    return true;
                }
            }
            return false;
        }

        //berekent het nieuwe gewicht van de row
        private void UpdateWeight()
        {
            int totalWeight = 0;
            foreach (var stack in Stacks)
            {
                totalWeight += stack.StackWeight;
            }

            RowWeight = totalWeight;
        }

        //berekent het gewicht van alle stacks aan de linker kant van de row
        public int GetLeftWeight()
        {
            var half = Math.Floor((decimal) Stacks.Count / 2);
            var leftStacks = Stacks.Where(s => s.XPosition <= half);
            return GetWeightOfStack(leftStacks);
        }

        //berekent het gewicht van alle stacks aan de rechter kant van de row

        public int GetRightWeight()
        {
            var half = Math.Ceiling((decimal)Stacks.Count / 2);
            var rightStacks = Stacks.Where(s => s.XPosition > half);
            return GetWeightOfStack(rightStacks);
        }

        //berekent het totale gewicht van een lijst van stacks
        private int GetWeightOfStack(IEnumerable<Stack> stacks)
        {
            int weight = 0;
            foreach (var stack in stacks)
            {
                weight += stack.StackWeight;
            }
            return weight;
        }

        //print de posities van de container in een stack aan de hand van een positie
        public string PrintStackPositions(int index)
        {
            if (index > Stacks.Count || index < 0)
            {
                return "0";
            }
            return Stacks[index].PrintStackPositions();
        }

        //print de gewichten van de container in een stack aan de hand van een positie
        public string PrintStackWeights(int index)
        {
            if (index > Stacks.Count || index < 0)
            {
                return "0";
            }
            return Stacks[index].PrintStackWeights();
        }
    }
}
