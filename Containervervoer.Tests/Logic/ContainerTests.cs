using System;
using Containervervoer.Logic;
using Xunit;

namespace Containervervoer.Tests
{
    
    public class ContainerTests
    {
        Container container = new Container(15000, ContainerType.Valuable);

        [Fact]
        public void GetType_ShouldGetContainerTypeAsInt()
        {
            //Arrange
            var expected = 2;
            //Act
            var actual = container.GetType();
            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetWeight_ShouldGetWeightAsTwoDecimals()
        {
            //Arrange
            var expected = 15;
            //Act
            var actual = container.GetWeight();
            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
