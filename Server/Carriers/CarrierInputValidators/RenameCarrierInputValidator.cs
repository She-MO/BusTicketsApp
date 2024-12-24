using BusTicketsApp.Server.Data;
using FluentValidation;

namespace BusTicketsApp.Server.Carriers.CarrierInputValidators;

public class RenameCarrierInputValidator : AbstractValidator<RenameCarrierInput>
{
    public RenameCarrierInputValidator(ApplicationDbContext dbContext)
    {
        RuleFor(input => input.Name)
            .NotEmpty().WithMessage("Carrier name cannot be empty")
            .Length(3, 30).WithMessage("Carrier name should be 3-30 characters long");
    }
    
}