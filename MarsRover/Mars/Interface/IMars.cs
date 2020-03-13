using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Mars.Interface
{
    public interface IMars
    {
        Direction Direction { get; set; }
        Position Position { get; set; }
        void Place(Position position, Direction direction);
        Position GetNextPosition();
        void RotateLeft();
        void RotateRight();  
        void Rotate(int rotationNumber);
    }
}
