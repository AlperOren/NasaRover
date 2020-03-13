using MarsRover.Grid.Interface;
using MarsRover.InstructionValidator.Interface;
using MarsRover.Mars;
using MarsRover.Mars.Interface;


namespace MarsRover.Operations
{
    public class Operation
    {
        public IMars Marse { get; private set; }
        public IRoverGrid Board { get; private set; }
        public IInstructionInputParser  Parser { get; private set; }

        public Operation(IMars marseRover, IRoverGrid grid, IInstructionInputParser  inputParser)
        {
            Marse = marseRover;
            Board= grid;
            Parser = inputParser;
        }

        public string ProcessInstruction(string[] input)
        {
            var instruction = Parser.ParseInstruction(input);
            if (instruction != Instruction.Coordinate && Marse.Position == null) return string.Empty;

            switch (instruction)
            {
                case Instruction.Coordinate:
                    var placeinstructionParameter = Parser.ParseInstructionParameter(input);
                    if (Board.IsValidGridPosition(placeinstructionParameter.Position))
                        Marse.Place(placeinstructionParameter.Position, placeinstructionParameter.Direction);
                    break;
                case Instruction.Move:
                    var newPosition = Marse.GetNextPosition();
                    if (Board.IsValidGridPosition(newPosition))
                        Marse.Position = newPosition;
                    break;
                case Instruction.Left:
                    Marse.RotateLeft();
                    break;
                case Instruction.Right:
                    Marse.RotateRight();
                    break;
                case Instruction.Summary:
                    return GetSummary();
            }
            return string.Empty;
        }

        public string GetSummary()
        {
            return string.Format("Output: {0},{1},{2}", Marse.Position.X,
                Marse.Position.Y, Marse.Direction.ToString().ToUpper());
        }
    }
}