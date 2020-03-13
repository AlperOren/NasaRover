using MarsRover.Mars;


namespace MarsRover.InstructionValidator.Interface
{
    public interface IInstructionInputParser
    {
        Instruction ParseInstruction(string[] rawInput);
     
        CoordinateInstructionParameter ParseInstructionParameter(string[] input);
    }
}
