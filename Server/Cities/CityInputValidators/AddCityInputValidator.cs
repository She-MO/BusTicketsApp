using BusTicketsApp.Server.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
//using Org.BouncyCastle.Crypto.Macs;

namespace BusTicketsApp.Server.Cities.CityInputValidators;

public class AddCityInputValidator : AbstractValidator<AddCityInput>
{
    public AddCityInputValidator(ApplicationDbContext dbContext)
    {
        RuleFor(input => input.Name)
            .NotEmpty().WithMessage("City name cannot be empty")
            .Length(3, 30).WithMessage("City name must be 3-30 characters long")
            .MustAsync(async (cityName, cancellationToken) =>
            {
                var city = await dbContext.Cities.AsNoTracking().FirstOrDefaultAsync(city => city.Name == cityName, cancellationToken);
                return city is null;
            }).WithMessage("City with this name already exists");
    }
}