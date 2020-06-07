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

        public bool LoadCooledContainer(Container container)
        {
            if (container.Type == ContainerType.Cooled)
            {
                var frontRow = Rows.Find(row => row.RowType == RowTpe.FrontRow);
                return frontRow.PlaceNonValuableContainer(container);
            }

            return false;
        }

        public bool LoadCooledValuableContainer(Container container)
        {
            if (container.Type == ContainerType.ValuableAndCooled)
            {
                var frontRow = Rows.Find(row => row.RowType == RowTpe.FrontRow);
                return frontRow.PlaceValuableContainer(container);
            }

            return false;
        }

        public bool LoadValuableContainer(Container container)
        {
            if (container.Type == ContainerType.Valuable)
            {
                var sortedRows = Rows.Where(row => row.RowType == RowTpe.BackRow || row.RowType == RowTpe.FrontRow);
                return sortedRows.Any(row => row.PlaceValuableContainer(container));
            }

            return false;
        }

        public bool LoadNormalContainer(Container container)
        {
            if (container.Type == ContainerType.Normal)
            {
                var rowsSortedByLightest = Rows.OrderBy(row => row.RowWeight);
                return rowsSortedByLightest.Any(row => row.PlaceNonValuableContainer(container));
            }

            return false;
        }

        public bool CheckShipWeight()
        {
            int currentWeight = GetShipTotalWeight();
            if (currentWeight >= MinWeight && currentWeight <= MaxWeight) return true;
            return false;
        }

        public int GetShipTotalWeight()
        {
            int totalWeight = 0;
            foreach (var row in Rows)
            {
                totalWeight += row.RowWeight;
            }
            return totalWeight;
        }

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

        public string PrintShip()
        {
            string positions = GetContainerPositionValues();
            string weights = GetContainerWeightValues();
            string url = $"https://i872272core.venus.fhict.nl/ContainerVisualizer/index.html?length={Length}&width={Width}&stacks={positions}&weights={weights}";
            return url;
        }
    }
}
