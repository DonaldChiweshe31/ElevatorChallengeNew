using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using ElevatorChallenge.Interfaces;
using ElevatorChallenge.Service;
using ElevatorChallenge.Configuration;
using ElevatorChallenge.Entities;
using System.Threading.Tasks;
using System.IO;
using System;
using Microsoft.Extensions.Options;
using System.Linq;

namespace ElevatorChallenge
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var logger = services.GetRequiredService<ILogger<Program>>();

                    try
                    {
                        logger.LogInformation("Starting Elevator Simulation");

                        var controller = services.GetRequiredService<IElevatorController>();
                        await controller.RunAsync();

                        logger.LogInformation("Elevator Simulation completed");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred starting the elevator simulation");
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fatal error: {ex.Message}");
                throw;
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var configPath = @"C:\Users\user\source\repos\ElevatorChallenge\Configuration\appsettings.json";

                    Console.WriteLine($"Loading config from: {configPath}");

                    if (!File.Exists(configPath))
                    {
                        throw new FileNotFoundException($"Configuration file not found at: {configPath}");
                    }

                    config.AddJsonFile(configPath, optional: false, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) =>
                {
                    // Register configuration
                    services.Configure<ElevatorSettings>(context.Configuration.GetSection("ElevatorSettings"));

                    // Register services
                    services.AddSingleton<IElevatorController, ElevatorController>();

                    // Register Building with a factory method to provide required parameters
                    services.AddSingleton<IBuilding>(serviceProvider =>
                    {
                        var settings = serviceProvider.GetRequiredService<IOptions<ElevatorSettings>>().Value;
                        var elevatorFactory = serviceProvider.GetRequiredService<IElevatorFactory>();

                        // Create elevators based on configuration
                        var elevators = Enumerable.Range(1, settings.DefaultElevators)
                            .Select(id => elevatorFactory.CreateElevator(id))
                            .ToList();

                        return new Building(settings.DefaultFloors, elevators);
                    });

                    // Register ElevatorFactory if you don't have one
                    services.AddSingleton<IElevatorFactory, ElevatorFactory>();

                    services.AddLogging(configure =>
                    {
                        configure.AddConsole();
                        configure.AddConfiguration(context.Configuration.GetSection("Logging"));
                    });
                });
    }
}