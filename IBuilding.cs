using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Interfaces
{
    public interface IBuilding
    {
        int FloorCount { get; }
        IReadOnlyList<IElevator> Elevators { get; }
        void AddWaitingPassengers(int floor, int count);
        int GetWaitingPassengers(int floor);
    }
}
