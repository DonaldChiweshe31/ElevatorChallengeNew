using System;
using System.Collections.Generic;
using System.Linq;
using ElevatorChallenge.Interfaces;

namespace ElevatorChallenge.Entities
{
    public class Building : IBuilding
    {
        private readonly Dictionary<int, int> _passengersWaiting;

        public Building(int floorCount, IEnumerable<IElevator> elevators)
        {
            FloorCount = floorCount; // Fixed: Assign to property, not parameter
            Elevators = elevators.ToList();
            _passengersWaiting = new Dictionary<int, int>();

            for (int i = 1; i <= floorCount; i++)
            {
                _passengersWaiting[i] = 0;
            }
        }

        public int FloorCount { get; }
        public IReadOnlyList<IElevator> Elevators { get; }

        public void AddWaitingPassengers(int floor, int count)
        {
            if (floor < 1 || floor > FloorCount)
                throw new ArgumentException($"Invalid floor number. Must be between 1 and {FloorCount}.");

            if (count < 0)
                throw new ArgumentException("Passengers count can't be negative.");

            _passengersWaiting[floor] += count;
        }

        public int GetWaitingPassengers(int floor)
        {
            if (floor < 1 || floor > FloorCount)
                throw new ArgumentException($"Invalid floor number. Should be between 1 and {FloorCount}.");

            return _passengersWaiting[floor];
        }
    }
}