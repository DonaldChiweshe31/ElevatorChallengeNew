using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Validators
{
    public class ElevatorRequest
    {
        public int Floor { get; set; }
        public int Passengers { get; set; }
        public bool DirectionUp { get; set; }
    }
}
