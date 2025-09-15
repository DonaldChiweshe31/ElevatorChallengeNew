using ElevatorChallenge.Configuration;
using ElevatorChallenge.Interfaces;
using ElevatorChallenge.Models;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ElevatorChallenge.Entities
{
    public interface IElevatorFactory
    {
        IElevator CreateElevator(int id);
    }

    public class ElevatorFactory : IElevatorFactory
    {
        private readonly ElevatorSettings _settings;
        private readonly ILogger<Elevator> _logger;

        public ElevatorFactory(IOptions<ElevatorSettings> settings, ILogger<Elevator> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public IElevator CreateElevator(int id)
        {
            // Determine elevator type based on ID or configuration
            var elevatorType = id % 3 == 0 ? ElevatorType.HighSpeed :
                              id % 3 == 1 ? ElevatorType.Glass : ElevatorType.Standard;

            int capacity = elevatorType == ElevatorType.HighSpeed ? _settings.HighSpeedCapacity :
                          elevatorType == ElevatorType.Glass ? _settings.GlassCapacity :
                          _settings.StandardCapacity;

            return new Elevator(
                id,
                elevatorType,
                capacity,
                _settings.MovementSpeed,
                _logger
            );
        }
    }
}