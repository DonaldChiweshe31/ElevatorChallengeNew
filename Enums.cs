using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Entities
{
 public enum ElevatorStatus
    {
        Idle,
        GoingUp,
        GoingDown,
        DoorsOpen,
        Maintenance
    }

    public enum ElevatorType
    {
        Standard,
        HighSpeed,
        Glass,
        Freight,
    }
}
