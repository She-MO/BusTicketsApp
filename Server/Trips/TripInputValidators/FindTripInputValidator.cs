using BusTicketsApp.Server.Trips.TripInputs;
using FluentValidation;

namespace BusTicketsApp.Server.Trips.TripInputValidators;

public class FindTripInputValidator : AbstractValidator<FindTripInput>
{
    public FindTripInputValidator()
    {
        RuleFor(input => input.numberOfPassengers)
            .GreaterThan(0).WithMessage("Number of passengers cannot be less than 1");
        RuleFor(input => input.numberOfPassengers)
            .LessThanOrEqualTo(5).WithMessage("Number of passengers cannot be greater than 5");
        RuleFor(input => input.Date)
            .Must(date => date >= DateOnly.FromDateTime(DateTime.Today)).WithMessage("Date cannot be less than Today")
            .LessThan(DateOnly.FromDateTime(DateTime.Today.AddMonths(1))).WithMessage("You can only search for trips that depart within 1 month");
    }
    
}