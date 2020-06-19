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
                throw new ArgumentException("container can't weigh above 30000 or below 4000");
            }
        }

        //geeft het type van de container
        public new int GetType()
        {
            return (int) Type;
        }

        //geeft het gewicht van de container
        public int GetWeight()
        {
            return Weight / 1000;
        }
    }
}
