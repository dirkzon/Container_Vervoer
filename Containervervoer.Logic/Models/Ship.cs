using System.Collections.Generic;
using System.Linq;

namespace Containervervoer.Logic
{
    public class Ship
    {
        public List<Row> Rows = new List<Row>();
        public int MaxWeight { get; set; }
        public int MinWeight { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public int  Height { get; set; }

        public Ship(int width, int length, int height)
        {
            Width = width;
            Length = length;
            Width = width;
            Height = height;
            MaxWeight = width * length * 150000;
            MinWeight = MaxWeight / 2;
            CreateRows(length, width, height);
        }

        //aan de hand van de lengte en breedte rijen aanmaken

        public void CreateRows(int length, int width, int height)
        {
            for (int i = 0; i < length; i++)
            {
                Row row;
                if (i == 0)
                {
                    row = new Row(i, RowTpe.FrontRow, width, height);
                }
                else if (i == length - 1)
                {
                    row = new Row(i, RowTpe.BackRow, width, height);
                }
                else
                {
                    row = new Row(i, RowTpe.MiddleRow, width, height);
                }
                Rows.Add(row);
            }
        }

        //proberen om een gekoelde container te plaatsen in de lichtste rij
        public bool LoadCooledContainer(Container container)
        {
            if (container.Type == ContainerType.Cooled)
            {
                var frontRow = Rows.Find(row => row.RowType == RowTpe.FrontRow);
                return frontRow.PlaceNonValuableContainer(container);
            }

            return false;
        }

        //proberen om een gekoelde en waardevolle container te plaatsen in de lichtste rij
        public bool LoadCooledValuableContainer(Container container)
        {
            if (container.Type == ContainerType.ValuableAndCooled)
            {
                var frontRow = Rows.Find(row => row.RowType == RowTpe.FrontRow);
                return frontRow.PlaceValuableContainer(container);
            }

            return false;
        }

        //proberen om een waardevolle container te plaatsen in de lichtste rij
        public bool LoadValuableContainer(Container container)
        {
            if (container.Type == ContainerType.Valuable)
            {
                var sortedRows = Rows.Where(row => row.RowType == RowTpe.BackRow || row.RowType == RowTpe.FrontRow);
                return sortedRows.Any(row => row.PlaceValuableContainer(container));
            }

            return false;
        }

        //proberen om een normale container te plaatsen in de lichtste rij
        public bool LoadNormalContainer(Container container)
        {
            if (container.Type == ContainerType.Normal)
            {
                var rowsSortedByLightest = Rows.OrderBy(row => row.RowWeight);
                return rowsSortedByLightest.Any(row => row.PlaceNonValuableContainer(container));
            }
            return false;
        }

        //berekent het totale gewicht van het schip
        public int GetShipTotalWeight()
        {
            int totalWeight = 0;
            foreach (var row in Rows)
            {
                totalWeight += row.RowWeight;
            }
            return totalWeight;
        }

        //check om te kijken of het schip genoeg gewicht heeft
        public bool CheckIfShipHasEnoughWeight()
        {
            int currentWeight = GetShipTotalWeight();
            if (currentWeight >= MinWeight && currentWeight <= MaxWeight) return true;
            return false;
        }

        //check om te kijken of het schip in balans is
        public bool CheckIfShipIsBalanced()
        {
            int leftWeight = 0;
            int rightWeight = 0;
            foreach (var row in Rows)
            {
                leftWeight += row.GetLeftWeight();
                rightWeight += row.GetRightWeight();
            }
            var percentage = GetWeightDifference(leftWeight, rightWeight);
            if (percentage > 20) return false;
            return true;
        }

        private static decimal GetWeightDifference(int leftweight, int rightweight)
        {
            decimal difference;
            if (leftweight > rightweight)
            {
                difference = leftweight - rightweight;
            }
            else
            {
                difference = rightweight - leftweight;
            }
            decimal totalWeight = leftweight + rightweight;
            return (difference / totalWeight) * 100;
        }

        //print de posities van de containers in het schip
        private string GetContainerPositionValues()
        {
            string output = string.Empty;
            for (int i = 0; i < Width; i++)
            {
                string ding = string.Empty;
                for(int j = 0; j < Length; j++)
                {
                    ding += Rows[j].PrintStackPositions(i);
                    if (j != Length -1)
                    {
                        ding += ",";
                    }
                }

                output += ding;
                if (i != Width -1)
                {
                    output += "/";
                }
            }

            return output;
        }

        //print de gewichten van de containers in het schip
        private string GetContainerWeightValues()
        {
            string output = string.Empty;
            for (int i = 0; i < Width; i++)
            {
                string ding = string.Empty;
                for (int j = 0; j < Length; j++)
                {
                    ding += Rows[j].PrintStackWeights(i);
                    if (j != Length - 1)
                    {
                        ding += ",";
                    }
                }

                output += ding;
                if (i != Width - 1)
                {
                    output += "/";
                }
            }

            return output;
        }

        //voegt de posities en gewichten van de containers in het schip samen in een url
        public string PrintShip()
        {
            string positions = GetContainerPositionValues();
            string weights = GetContainerWeightValues();
            return $"https://i872272core.venus.fhict.nl/ContainerVisualizer/index.html?length={Length}&width={Width}&stacks={positions}&weights={weights}";
        }
    }
}
