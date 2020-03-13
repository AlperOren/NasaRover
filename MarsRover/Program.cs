using MarsRover.DependencyInjection;
using MarsRover.Grid;
using MarsRover.Grid.Interface;
using MarsRover.InstructionValidator;
using MarsRover.InstructionValidator.Interface;
using MarsRover.Mars.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using MarsRover.Mars;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<MarsRoverApp>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

     
            var config = LoadConfiguration();

            services.AddSingleton(config);
            services.AddTransient<IMars, Mars.Mars>();
            services.AddTransient<IRoverGrid>(s => new RoverGrid(5,5));

            services.AddTransient<IInstructionInputParser, InstructionInputParser>();

            services.AddTransient<MarsRoverApp>();

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
           .Build();

          return config;
        }
    }
}