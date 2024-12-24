using BusTicketsApp.Server.Data;
using FluentValidation;

namespace BusTicketsApp.Server.Buses.BusInputValidators;

public class AddBusInputValidator : AbstractValidator<AddBusInput>
{
    public AddBusInputValidator(ApplicationDbContext dbContext)
    {
        RuleFor(input => input.BusNumber)
            .NotEmpty().WithMessage("Bus number cannot be empty");
        RuleFor(input => input.NumberOfSeats)
            .GreaterThanOrEqualTo(10).WithMessage("Number of seats cannot be less than 10")
            .LessThanOrEqualTo(100).WithMessage("Number of seats cannot be greater than 100");
    }
    
}