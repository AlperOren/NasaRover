using MarsRover.Mars;

namespace MarsRover.InstructionValidator
{
    public class CoordinateInstructionParameter
    {
        public Position Position { get; set; }
        public Direction Direction { get; set; }

        public CoordinateInstructionParameter(Position position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }
    }
}

