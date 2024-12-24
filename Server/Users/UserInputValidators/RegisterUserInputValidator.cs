using System.Text.RegularExpressions;
using BusTicketsApp.Server.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Users.UserInputValidators;

public class RegisterUserInputValidator : AbstractValidator<RegisterUserInput>
{
    public RegisterUserInputValidator(ApplicationDbContext dbContext)
    {
        RuleFor(input => input.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Entered email is not vallid")
            .MustAsync(async (email, cancellationToken) =>
            {
                User? u = await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
                return u is null;
            }).WithMessage("Email address is already in use");
        RuleFor(input => input.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .Length(10, 20).WithMessage("Password should be 10-20 characters long");
        RuleFor(input => input.Phone)
            .NotEmpty().WithMessage("Phone cannot be empty")
            .Matches(new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$"))
            .WithMessage("Phone number is not valid");

    }

}