using System.Text.RegularExpressions;
using BusTicketsApp.Server.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Users.UserInputValidators;

public class LogInUserInputValidator : AbstractValidator<LogInUserInput>
{
    public LogInUserInputValidator(ApplicationDbContext dbContext)
    {
        RuleFor(input => input.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Entered email is not vallid");
        RuleFor(input => input.Password)
            .NotEmpty().WithMessage("Password cannot be empty");

    }
}