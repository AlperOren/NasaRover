using MarsRover.Grid.Interface;
using MarsRover.Mars;
using System;

namespace MarsRover.Grid
{
    public class RoverGrid : IRoverGrid
    {
        private int Rows;
        private int Columns;

        public RoverGrid(int rows, int cols)
        {
            this.Rows = rows;
            this.Columns = cols;
        }

        public bool IsValidGridPosition(Position position)
        {
            return position.X < Columns && position.X >= 0 &&
                   position.Y < Rows && position.Y >= 0;
        }
    }
}