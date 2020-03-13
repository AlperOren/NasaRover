using MarsRover.Grid;
using MarsRover.Mars;
using NUnit.Framework;

namespace MarsRoverTests
{
    public class TableLand
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestInvalidGridPosition()
        {
            // arrange
            RoverGrid squareGrid = new RoverGrid(5, 5);
            Position position = new Position(6, 6);

            // act
            var result = squareGrid.IsValidGridPosition(position);

            // assert
            Assert.IsFalse(result);
        }
    }
}