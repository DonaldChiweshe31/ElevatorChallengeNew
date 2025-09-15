using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ElevatorChallenge.Interfaces;
using ElevatorChallenge.Entities;
using ElevatorChallenge.Configuration;

namespace ElevatorChallenge.Service
{
    public class ElevatorController : IElevatorController
    {
        private readonly ILogger<ElevatorController> _logger;
        private readonly IBuilding _building;
        private readonly ElevatorSettings _settings;

        public ElevatorController(
            ILogger<ElevatorController> logger,
            IBuilding building,
            IOptions<ElevatorSettings> settings)
        {
            _logger = logger;
            _building = building;
            _settings = settings.Value;
        }

        public async Task RunAsync()
        {
            _logger.LogInformation("Elevator Controller started with {DefaultElevators} elevators and {DefaultFloors} floors",
                _settings.DefaultElevators, _settings.DefaultFloors);

            // Your simulation logic here

            await Task.CompletedTask;
        }
    }
}