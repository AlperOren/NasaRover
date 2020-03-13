using MarsRover.InstructionValidator;
using MarsRover.Mars;
using NUnit.Framework;
using System;

namespace MarsRoverTests
{
    public class TestInstructionValidator
    {
        [Test]
        public void TestValidCoordinateInstruction()
        {
            // arrange
            var instructionInputParser = new InstructionInputParser();
            string[] rawInput = "COORDINATE".Split(' ');

            // act
            var instruction = instructionInputParser.ParseInstruction(rawInput);

            // assert
            Assert.AreEqual(Instruction.Coordinate, instruction);
        }

        [Test]
        public void TestInvalidCoordinateInstructionAndParams()
        {
            // arrange
            var inputParser = new InputParser();
            string[] rawInput = "COORDINATE 3,1".Split(' ');

            // act and assert
            var exception = Assert.Throws<ArgumentException>(delegate
            { var coordinateinstructionParameter = inputParser.ParseInstructionParameter(rawInput); });
            Assert.That(exception.Message, Is.EqualTo("Incomplete instruction. Please ensure that the COORDINATE instruction is using format: COORDINATE X,Y,D"));

        }

        [Test]
        public void TestValidCoordinateInstructionAndParams()
        {
            // arrange
            var inputParser = new InputParser();
            string[] rawInput = "COORDINATE 4,3,WEST".Split(' ');

            // act
            var coordinateinstructionParameter = inputParser.ParseInstructionParameter(rawInput);

            // assert
            Assert.AreEqual(4, coordinateinstructionParameter.Position.X);
            Assert.AreEqual(3, coordinateinstructionParameter.Position.Y);
            Assert.AreEqual(Direction.West, coordinateinstructionParameter.Direction);
        }

        [Test]
        public void TestInvalidCoordinateParamsFormat()
        {
            // arrange
            var paramParser = new CoordinateInstructionParamaterParser();
            string[] rawInput = "COORDINATE 3,3,SOUTH,2".Split(' ');

            // act and assert
            var exception = Assert.Throws<ArgumentException>(delegate { paramParser.ParseParameters(rawInput); });
            Assert.That(exception.Message, Is.EqualTo("Incomplete instruction. Please ensure that the COORDINATE instruction is using format: COORDINATE X,Y,D"));
        }

        [Test]
        public void TestInvalidCoordinateDirection()
        {
            // arrange
            var paramParser = new CoordinateInstructionParamaterParser();
            string[] rawInput = "COORDINATE 2,4,OneDirection".Split(' ');

            // act and assert
            var exception = Assert.Throws<ArgumentException>(delegate { paramParser.ParseParameters(rawInput); });
            Assert.That(exception.Message, Is.EqualTo("Invalid direction. Please select from one of the following directions: NORTH|EAST|SOUTH|WEST"));
        }
    }
}
