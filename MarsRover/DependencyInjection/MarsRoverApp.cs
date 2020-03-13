using MarsRover.InstructionValidator.Interface;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using System;
using Serilog.Debugging;
using System.IO;
using MarsRover.Grid.Interface;
using MarsRover.Mars.Interface;

namespace MarsRover.DependencyInjection
{
    public class MarsRoverApp
    {

        const string description =
@"  **************************************
  **************************************
  **                                  **
  **        - MARS ROBOTIC ROVER -    **
  **                                  **
  **************************************
  **************************************


  1: Place the ROBOTIC ROVER on a 5 x 5 grid
     using the following command:

     COORDINATE X,Y,D (Where X and Y are integers and D 
     must be either NORTH, SOUTH, EAST or WEST)
    E.g. COORDINATE 3,3,WEST

  2: When the ROBOTIC ROVER is placed, give following instructions to Robot.
                  
     LEFT   – turns the Robot 90 degrees left.
     RIGHT  – turns the Robot 90 degrees right.
     MOVE   – Moves the Robot 1 unit in the facing direction.
     EXIT   – Closes the Robot Rover. NASA my missing is completed! Let me come home, please...
";


        private readonly IConfiguration _config;
        private readonly IInstructionInputParser _instructionInputParser;
        private readonly IRoverGrid _roverGrid;
        private readonly IMars _marse;
        public MarsRoverApp(
            IConfiguration config,
            IInstructionInputParser instructionInputParser,
            IRoverGrid roverGrid,
            IMars marse
            )
        {
            _config = config;
            _instructionInputParser = instructionInputParser;
            _roverGrid = roverGrid;
            _marse = marse;
        }

        public void Run()
        {
            var fileName = _config.GetValue<string>("MarsRoverLog:LogOutputDirectory");
            var filePath = $"{Directory.GetCurrentDirectory()}\\{fileName}.txt";

            var log = new LoggerConfiguration()
             .WriteTo.Console()
             .WriteTo.File(filePath, rollingInterval: RollingInterval.Day)
             .CreateLogger();

            try
            {
                var simulator = new Operations.Operation(_marse, _roverGrid, _instructionInputParser);

                var stopApplication = false;
                Console.WriteLine(description);
                do
                {
                    var command = Console.ReadLine();
                    if (command == null) continue;

                    if (command.Equals("EXIT")) {
                        stopApplication = true;
                        log.Information("Hey Nasa, my mission is completed. I am returning to earth");
                    }                                  
                    else
                    {
                        try
                        {
                            var output = simulator.ProcessInstruction(command.Split(' '));
                            if (!string.IsNullOrEmpty(output))
                                Console.WriteLine(output);
                        }
                        catch (ArgumentException exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                    }
                } while (!stopApplication);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Nasa, Robot is faling to send data. {ex.InnerException.Message}");
            }
            finally {
                Log.CloseAndFlush();
            }

        }

    }
}

