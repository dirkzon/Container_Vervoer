using Containervervoer.Logic;
using Xunit;

namespace Containervervoer.Tests.Logic
{
    public class ShipTests
    {
        Ship ship = new Ship(2,2,3);

        public ShipTests()
        {
            ship.LoadValuableContainer(new Container(8000, ContainerType.Valuable));
            ship.LoadCooledContainer(new Container(10000, ContainerType.Cooled));
            ship.LoadNormalContainer(new Container(15000, ContainerType.Normal));
        }

        [Fact]
        public void LoadCooledContainer_ShouldLoadContainer()
        {
            //Arrange
            var container = new Container(12000, ContainerType.Cooled);

            //Act

            //Assert
            Assert.True(ship.LoadCooledContainer(container));
        }

        [Theory]
        [InlineData(ContainerType.ValuableAndCooled)]
        [InlineData(ContainerType.Normal)]
        [InlineData(ContainerType.Valuable)]
        public void LoadCooledContainer_ShouldNotLoadContainer(ContainerType type)
        {
            //Arrange
            var container = new Container(12000, type);

            //Act

            //Assert
            Assert.False(ship.LoadCooledContainer(container));
        }

        [Fact]
        public void LoadCooledValuableContainer_ShouldLoadContainer()
        {
            //Arrange
            var container = new Container(12000, ContainerType.ValuableAndCooled);

            //Act

            //Assert
            Assert.True(ship.LoadCooledValuableContainer(container));
        }

        [Theory]
        [InlineData(ContainerType.Cooled)]
        [InlineData(ContainerType.Normal)]
        [InlineData(ContainerType.Valuable)]
        public void LoadCooledValuableContainer_ShouldNotLoadContainer(ContainerType type)
        {
            //Arrange
            var container = new Container(12000, type);

            //Act

            //Assert
            Assert.False(ship.LoadCooledValuableContainer(container));
        }

        [Fact]
        public void LoadValuableContainer_ShouldLoadContainer()
        {
            //Arrange
            var container = new Container(12000, ContainerType.Valuable);

            //Act

            //Assert
            Assert.True(ship.LoadValuableContainer(container));
        }

        [Theory]
        [InlineData(ContainerType.Cooled)]
        [InlineData(ContainerType.Normal)]
        [InlineData(ContainerType.ValuableAndCooled)]
        public void LoadValuableContainer_ShouldNotLoadContainer(ContainerType type)
        {
            //Arrange
            var container = new Container(12000, type);

            //Act

            //Assert
            Assert.False(ship.LoadValuableContainer(container));
        }

        [Fact]
        public void LoadNormalContainer_ShouldLoadContainer()
        {
            //Arrange
            var container = new Container(12000, ContainerType.Normal);

            //Act

            //Assert
            Assert.True(ship.LoadNormalContainer(container));
        }

        [Theory]
        [InlineData(ContainerType.Cooled)]
        [InlineData(ContainerType.Valuable)]
        [InlineData(ContainerType.ValuableAndCooled)]
        public void LoadNormalContainer_ShouldNotLoadContainer(ContainerType type)
        {
            //Arrange
            var container = new Container(12000, type);

            //Act

            //Assert
            Assert.False(ship.LoadNormalContainer(container));
        }

        [Fact]
        public void GetShipTotalWeight_ShouldReturnShipsWeight()
        {
            //Arrange
            var expected = 33000;
            //Act
            var actual = ship.GetShipTotalWeight();
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PrintShip_ShouldReturnUrlWithPositionsAndWeights()
        {
            //Arrange
            var expected = "https://i872272core.venus.fhict.nl/ContainerVisualizer/index.html?length=2&width=2&stacks=2,1/3,&weights=8,15/10,";
            //Act
            var actual = ship.PrintShip();
            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
