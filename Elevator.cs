using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ElevatorChallenge.Interfaces;
using ElevatorChallenge.Entities;
using Microsoft.Extensions.Logging;

namespace ElevatorChallenge.Models
{
    public class Elevator : IElevator
    {
        private readonly List<int> _destinationFloors = new List<int>();
        private readonly SemaphoreSlim _movementSemaphore = new SemaphoreSlim(1, 1); 
        private readonly int _movementSpeed;
        private readonly ILogger<Elevator> _logger;

        private int _currentFloor;
        private ElevatorStatus _state = ElevatorStatus.Idle;
        private int _passengerCount;

        public Elevator(int id, ElevatorType type, int capacity, int movementSpeed, ILogger<Elevator> logger = null)
        {
            Id = id;
            Type = type;
            Capacity = capacity;
            _movementSpeed = movementSpeed;
            _currentFloor = 1;
            _logger = logger;

            _logger?.LogInformation("Elevator {Id} ({Type}) created with capacity {Capacity}", Id, Type, Capacity);
        }

        public int Id { get; }
        public int CurrentFloor => _currentFloor;
        public ElevatorStatus State => _state;
        public ElevatorType Type { get; }
        public int Capacity { get; }
        public int PassengerCount => _passengerCount;
        public IReadOnlyList<int> DestinationFloors => _destinationFloors.AsReadOnly();

        public async Task MoveToFloorAsync(int floor)
        {
            await _movementSemaphore.WaitAsync();

            try
            {
                if (floor == _currentFloor)
                    return;

                _state = floor > _currentFloor ? ElevatorStatus.GoingUp : ElevatorStatus.GoingDown;

                _logger?.LogInformation("Elevator {Id} moving from floor {CurrentFloor} to floor {TargetFloor} ({Direction})",
                    Id, _currentFloor, floor, _state);

                int direction = _state == ElevatorStatus.GoingUp ? 1 : -1;
                int floorsToMove = Math.Abs(floor - _currentFloor);

                for (int i = 0; i < floorsToMove; i++)
                {
                    await Task.Delay(_movementSpeed);
                    _currentFloor += direction;

                    _logger?.LogDebug("Elevator {Id} passed floor {Floor}", Id, _currentFloor);

                    // Check if we need to stop at this floor
                    if (_destinationFloors.Contains(_currentFloor))
                    {
                        await StopAtFloorAsync(_currentFloor);
                    }
                }

                _state = ElevatorStatus.Idle;
                _logger?.LogInformation("Elevator {Id} arrived at floor {Floor}", Id, _currentFloor);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error moving elevator {Id} to floor {Floor}", Id, floor);
                throw;
            }
            finally
            {
                _movementSemaphore.Release();
            }
        }

        public async Task<bool> AddDestinationAsync(int floor, int passengers = 1)
        {
            if (passengers <= 0)
                throw new ArgumentException("Passenger count must be positive.");

            if (_passengerCount + passengers > Capacity)
            {
                _logger?.LogWarning("Elevator {Id} cannot accept {Passengers} passengers. Current: {Current}/{Capacity}",
                    Id, passengers, _passengerCount, Capacity);
                return false;
            }

            if (!_destinationFloors.Contains(floor))
            {
                _destinationFloors.Add(floor);
                _destinationFloors.Sort();
                _logger?.LogInformation("Elevator {Id} added destination floor {Floor}", Id, floor);
            }

            _passengerCount += passengers;
            _logger?.LogInformation("Elevator {Id} now has {Passengers}/{Capacity} passengers",
                Id, _passengerCount, Capacity);

            if (_state == ElevatorStatus.Idle)
            {
                _ = Task.Run(() => MoveToNextDestinationAsync());
            }

            return true;
        }

        public void Update()
        {
            // Check if destinations are idle
            if (_state == ElevatorStatus.Idle && _destinationFloors.Any())
            {
                _logger?.LogDebug("Elevator {Id} starting movement to next destination", Id);
                _ = Task.Run(() => MoveToNextDestinationAsync());
            }
        }

        private async Task MoveToNextDestinationAsync()
        {
            if (!_destinationFloors.Any())
                return;

            int nextFloor = _destinationFloors[0];
            await MoveToFloorAsync(nextFloor);
        }

        private async Task StopAtFloorAsync(int floor)
        {
            _logger?.LogInformation("Elevator {Id} stopping at floor {Floor}", Id, floor);

            _state = ElevatorStatus.DoorsOpen;
            _destinationFloors.Remove(floor);

            // Simulate passengers exiting and entering
            int exitingPassengers = new Random().Next(0, _passengerCount + 1);
            _passengerCount -= exitingPassengers;

            _logger?.LogInformation("Elevator {Id} - {Exiting} passengers exited, {Remaining} remaining",
                Id, exitingPassengers, _passengerCount);

            // Use a constant door operation time (e.g., 2000ms) or get it from configuration
            await Task.Delay(2000); // Fixed: Replaced Constants.Door_Operation_Time with a value

            _state = ElevatorStatus.Idle;
            _logger?.LogInformation("Elevator {Id} doors closed", Id);
        }
     
    }
}
