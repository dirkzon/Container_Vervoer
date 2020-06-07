using System;

namespace Containervervoer.Logic
{
    public class Container
    {
        public int Weight { get; set; }
        public ContainerType Type { get; set; }
        public int Height { get; set; }

        public Container(int weight, ContainerType type)
        {
            if (weight >= 4000 && weight <= 30000)
            {
                Weight = weight;
                Type = type;
            }
            else
            {
                throw new ArgumentException("container kan niet boven de 4000 of onder 30000 wegen");
            }
        }

        public int GetType()
        {
            return (int) Type;
        }

        public int GetWeight()
        {
            return Weight / 1000;
        }
    }
}
