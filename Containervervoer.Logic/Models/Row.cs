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

        private void CreateStacks(int width)
        {
            for (var i = 0; i < width; i++)
            {
                Stacks.Add(new Stack(i));
            }
        }

        public IEnumerable<Stack> GetStacksByWeight()
        {
            return Stacks.OrderBy(stack => stack.StackWeight);
        }

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

        private void UpdateWeight()
        {
            int totalWeight = 0;
            foreach (var stack in Stacks)
            {
                totalWeight += stack.StackWeight;
            }

            RowWeight = totalWeight;
        }

        public string PrintStackPositions(int index)
        {
            if (index > Stacks.Count || index < 0)
            {
                return "0";
            }
            return Stacks[index].PrintStackPositions();
        }

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
