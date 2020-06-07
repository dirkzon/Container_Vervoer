using Containervervoer.Logic;
using Xunit;

namespace Containervervoer.Tests
{
    public class RowTests
    {
        Row row = new Row(1, RowTpe.MiddleRow, 4, 4);

        public RowTests()
        {
            row.PlaceValuableContainer(new Container(18000, ContainerType.Normal));
            row.PlaceValuableContainer(new Container(15000, ContainerType.Normal));
            row.PlaceValuableContainer(new Container(21000, ContainerType.Normal));
            row.PlaceValuableContainer(new Container(12000, ContainerType.Valuable));
            row.PlaceNonValuableContainer(new Container(6000, ContainerType.Cooled));
            row.PlaceNonValuableContainer(new Container(25000, ContainerType.ValuableAndCooled));
        }

        [Theory]
        [InlineData(ContainerType.Normal)]
        [InlineData(ContainerType.Cooled)]
        public void PlaceNonValuableContainer_ShouldPlaceNormalContainer(ContainerType type)
        {
            //Arrange
            var container = new Container(12000, type);
            //Act

            //Assert
            Assert.True(row.PlaceNonValuableContainer(container));
        }


        [Theory]
        [InlineData(ContainerType.Valuable)]
        [InlineData(ContainerType.ValuableAndCooled)]

        public void PlaceValuableContainer_ShouldPlaceValuableContainer(ContainerType type)
        {
            //Arrange
            var container = new Container(12000, type);

            //Act

            //Assert
            Assert.True(row.PlaceValuableContainer(container));
        }


        [Fact]
        public void PrintStackPositions_ShouldPrintRowPositions()
        {
            //Arrange
            var expected = "4-1";

            //Act
            var actual = row.PrintStackPositions(1);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-3)]
        [InlineData(20)]
        public void PrintStackPositions_ShouldReturnZero(int index)
        {
            //Arrange
            var expected = "0";

            //Act
            var actual = row.PrintStackPositions(index);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PrintStackWeights_ShouldPrintRowWeights()
        {
            //Arrange
            var expected = "25-15";

            //Act
            var actual = row.PrintStackWeights(1);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-3)]
        [InlineData(20)]
        public void PrintStackWeights_ShouldReturnZero(int index)
        {
            //Arrange
            var expected = "0";

            //Act
            var actual = row.PrintStackWeights(index);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
