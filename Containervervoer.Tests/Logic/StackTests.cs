using Containervervoer.Logic;
using Xunit;

namespace Containervervoer.Tests
{
    public class StackTests
    {
        Stack stack = new Stack(2);

        [Fact]
        public void LoadNormalContainer_ShouldNotChangeContainsValuable()
        {
            //Arrange
            var Container = new Container(12000, ContainerType.Normal);
            //Act
            stack.LoadContainer(Container);
            //Assert
            Assert.False(stack.ContainsValuable);
        }

        [Fact]
        public void LoadValuableContainer_ShouldChangeContainsValuable()
        {
            //Arrange
            var Container = new Container(12000, ContainerType.Valuable);
            //Act
            stack.LoadContainer(Container);
            //Assert
            Assert.True(stack.ContainsValuable);
        }

        [Fact]
        public void CheckIfWeightCanFit_ShouldFit()
        {
            //Arrange
            var Container1 = new Container(12000, ContainerType.Normal);
            var Container2 = new Container(25000, ContainerType.Normal);
            //Act
            stack.LoadContainer(Container1);
            //Assert
            Assert.True(stack.CheckIfWeightCanFit(Container2));
        }

        [Fact]
        public void CheckIfWeightCanFit_ShouldNotFit()
        {
            //Arrange
            for (int i = 0; i < 5; i++)
            {
                stack.LoadContainer(new Container(30000, ContainerType.Normal));
            }

            var container = new Container(25000, ContainerType.Normal);
            //Act


            //Assert
            Assert.False(stack.CheckIfWeightCanFit(container));
        }

        [Fact]
        public void CheckIfHeightCanFit_ShouldFit()
        {
            //Arrange
            var Container1 = new Container(30000, ContainerType.Normal);
            var Container2 = new Container(30000, ContainerType.Normal);

            //Act
            stack.LoadContainer(Container1);

            //Assert
            Assert.True(stack.CheckIfHeightCanFit(4));
        }

        [Fact]
        public void CheckIfHeightCanFit_ShouldNotFit()
        {
            //Arrange
            var Container1 = new Container(30000, ContainerType.Normal);
            var Container2 = new Container(30000, ContainerType.Normal);
            var Container3 = new Container(30000, ContainerType.Normal);

            //Act
            stack.LoadContainer(Container1);
            stack.LoadContainer(Container2);
            stack.LoadContainer(Container3);

            //Assert
            Assert.False(stack.CheckIfHeightCanFit(2));
        }

        [Fact]
        public void PrintStackPositions_ShouldReturnContainerPositions()
        {
            //Arrange
            var Container1 = new Container(12000, ContainerType.Normal);
            var Container2 = new Container(8000, ContainerType.Cooled);
            var Container3 = new Container(23000, ContainerType.Valuable);
            var Container4 = new Container(18000, ContainerType.ValuableAndCooled);

            var expected = "4-2-3-1";

            //Act
            stack.LoadContainer(Container1);
            stack.LoadContainer(Container2);
            stack.LoadContainer(Container3);
            stack.LoadContainer(Container4);

            var actual = stack.PrintStackPositions();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PrintStackWeights_ShouldReturnContainerWeights()
        {
            //Arrange
            var Container1 = new Container(12000, ContainerType.Normal);
            var Container2 = new Container(8000, ContainerType.Cooled);
            var Container3 = new Container(23000, ContainerType.Valuable);
            var Container4 = new Container(18000, ContainerType.ValuableAndCooled);

            var expected = "18-23-8-12";

            //Act
            stack.LoadContainer(Container1);
            stack.LoadContainer(Container2);
            stack.LoadContainer(Container3);
            stack.LoadContainer(Container4);

            var actual = stack.PrintStackWeights();

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
