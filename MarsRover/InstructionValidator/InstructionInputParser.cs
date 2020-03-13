using MarsRover.InstructionValidator.Interface;
using MarsRover.Mars;
using System;

namespace MarsRover.InstructionValidator
{
    public class InstructionInputParser : IInstructionInputParser
    {
        public Instruction ParseInstruction(string[] rawInput)
        {
            Instruction instruction;
            if (!Enum.TryParse(rawInput[0], true, out instruction))
                throw new ArgumentException("Sorry, Cannot validate your instruction!. Please try again using the following format: COORDINATE X,Y,D|MOVE|LEFT|RIGHT|SUMMARY");
            return instruction;
        }
        public CoordinateInstructionParameter ParseInstructionParameter(string[] input)
        {
            var coordinateInstructionParameter = new CoordinateInstructionParamaterParser();
            return coordinateInstructionParameter.ParseParameters(input);
        }
    }
}