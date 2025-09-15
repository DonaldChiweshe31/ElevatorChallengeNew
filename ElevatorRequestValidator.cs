using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ElevatorChallenge.Validators
{
   

    namespace ElevatorChallenge.Validators
    {
        public class ElevatorRequestValidator : AbstractValidator<ElevatorRequest>
        {
            public ElevatorRequestValidator(int maxFloors)
            {
                RuleFor(x => x.Floor)
                    .InclusiveBetween(1, maxFloors)
                    .WithMessage($"Floor must be between 1 and {maxFloors}");

                RuleFor(x => x.Passengers)
                    .GreaterThan(0)
                    .WithMessage("Passenger count must be positive");

                RuleFor(x => x.Passengers)
                    .LessThanOrEqualTo(20)
                    .WithMessage("Cannot exceed 20 passengers per request");
            }
        }
    }
}
