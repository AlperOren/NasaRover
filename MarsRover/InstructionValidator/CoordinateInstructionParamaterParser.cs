using MarsRover.Mars;
using System;

namespace MarsRover.InstructionValidator
{
    public class CoordinateInstructionParamaterParser
    {

            private const int ParameterCount = 3;

            private const int InstructionInputCount = 2;


            public CoordinateInstructionParameter ParseParameters(string[] input)
            {
                Direction direction;
                Position position = null;

    
                if (input.Length != InstructionInputCount)
                    throw new ArgumentException("Incomplete instruction. Please ensure that the COORDINATE instruction is using format: COORDINATE X,Y,D");

               
                var instructionParams = input[1].Split(',');
                if (instructionParams.Length != ParameterCount)
                    throw new ArgumentException("Incomplete instruction. Please ensure that the COORDINATE instruction is using format: COORDINATE X,Y,D");

                if (!Enum.TryParse(instructionParams[instructionParams.Length - 1], true, out direction))
                    throw new ArgumentException("Invalid direction. Please select from one of the following directions: NORTH|EAST|SOUTH|WEST");

                var x = Convert.ToInt32(instructionParams[0]);
                var y = Convert.ToInt32(instructionParams[1]);
                position = new Position(x, y);

                return new CoordinateInstructionParameter(position, direction);
            
        }
    }
}
