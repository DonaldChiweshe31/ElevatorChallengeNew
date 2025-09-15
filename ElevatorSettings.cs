using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Configuration
{
    public class ElevatorSettings
    {
        public int DefaultFloors { get; set; }
        public int DefaultElevators { get; set; }
        public int MovementSpeed { get; set; }
        public int DoorOperationTime { get; set; }
        public int StandardCapacity { get; set; }
        public int HighSpeedCapacity { get; set; }
        public int GlassCapacity { get; set; }
        public int FreightCapacity { get; set; }
        public int SimulationSpeed { get; set; }
    }
}
