using MarsRover.InstructionValidator;
using MarsRover.Mars;
using System;

namespace MarsRoverTests
{
    internal class InputParser
    {
        public Instruction ParseInstruction(string[] rawInput)
        {
            Instruction instruction;
            if (!Enum.TryParse(rawInput[0], true, out instruction))
                throw new ArgumentException("Sorry, your instruction was not recognised. Please try again using the following format: COORDINATE X,Y,D|MOVE|LEFT|RIGHT|SUMMARY");
            return instruction;
        }

        // Extracts the parameters from the user input and returns it       
        public CoordinateInstructionParameter ParseInstructionParameter(string[] input)
        {
            var coordinateInstructionParameter = new CoordinateInstructionParamaterParser(); 
            return coordinateInstructionParameter.ParseParameters(input);
        }
    }
}