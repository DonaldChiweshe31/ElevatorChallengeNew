using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorChallenge.Entities;

namespace ElevatorChallenge.Interfaces
{

    public interface IElevator
    {
        int Id { get; }
        int CurrentFloor { get; }
        ElevatorStatus State { get; }
        ElevatorType Type { get; }
        int Capacity { get; }
        int PassengerCount { get; }
        IReadOnlyList<int> DestinationFloors { get; }

        Task MoveToFloorAsync(int floor);
        Task<bool> AddDestinationAsync(int floor, int passengers = 1);
        void Update();
    }
}
