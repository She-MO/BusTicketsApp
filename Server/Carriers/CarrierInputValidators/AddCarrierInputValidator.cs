using BusTicketsApp.Server.Data;
using FluentValidation;

namespace BusTicketsApp.Server.Carriers.CarrierInputValidators;

public class AddCarrierInputValidator : AbstractValidator<AddCarrierInput>
{
    public AddCarrierInputValidator(ApplicationDbContext dbContext)
    {
        RuleFor(input => input.Name)
            .NotEmpty().WithMessage("Carrier name cannot be empty")
            .Length(3, 30).WithMessage("Carrier name should be 3-30 characters long");
    }
    
}