
using ElevatorChallenge.Interfaces;
using ElevatorChallenge.Configuration;
using ElevatorChallenge.Entities;


namespace ElevatorChallenge.Models
{
    public class ElevatorFac
    {
        public static IElevator CreateElevator(int id, ElevatorType type, ElevatorSettings settings)
        {
            return type switch
            {
                ElevatorType.Standard => new Elevator(id, type, settings.StandardCapacity, settings.MovementSpeed),
                ElevatorType.HighSpeed => new Elevator(id, type, settings.HighSpeedCapacity, settings.MovementSpeed / 2),
                ElevatorType.Glass => new Elevator(id, type, settings.GlassCapacity, settings.MovementSpeed),
                ElevatorType.Freight => new Elevator(id, type, settings.FreightCapacity, settings.MovementSpeed * 2),
                _ => throw new ArgumentException("Unknown elevator type")
            };
        }
    }
}