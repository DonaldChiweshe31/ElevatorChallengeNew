using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorChallenge.Entities;


namespace ElevatorChallenge.Entities
{
   public static class Constants
    {
        public const int Standard_Capacity = 10;
        public const int High_Speed_Capacity = 8;
        public const int Glass_Capacity = 6;
        public const int Freight_Capacity = 20;

        //Per floor timer
        public const int Standard_Speed = 1000;
        public const int High_Speed = 500;
        public const int Door_Operation_Time = 2000;
    }

}
