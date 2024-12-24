using BusTicketsApp.Server.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Route = BusTicketsApp.Server.Data.Route;

namespace BusTicketsApp.Server.Routes.RouteInputValidators;

public class AddRouteInputValidator : AbstractValidator<AddRouteInput>
{
    public AddRouteInputValidator(ApplicationDbContext dbContext)
    {
            RuleFor(input => input.ShortName)
                .NotEmpty().WithMessage("Route name cannot be empty")
                .MustAsync(async (shortName, cancellationToken) =>
                {
                    Route? u = await dbContext.Routes.FirstOrDefaultAsync(user => user.ShortName == shortName, cancellationToken);
                    return u is null;
                }).WithMessage("Route with this name already exists");
            RuleFor(input => input.Stops)
                .Must(stops => stops is not null && stops.Count >= 2).WithMessage("Route must have at least two stops")
                .Must(stops => stops.Count <= 10).WithMessage("Route cannot have more than 10 stops");
            RuleFor(input => input.Stops.Take(1))
                .ForEach(rules =>
                {
                    rules.MustAsync(async (stop, cancellationToken) =>
                    {
                        City? c = await dbContext.Cities.FirstOrDefaultAsync(user => user.Id == stop.CityId, cancellationToken);
                        return c is not null;
                    });
                    rules.Must(stop => stop.KmFromPrevStop == 0).WithMessage("Number of km from the previous stop for the first stop must be 0");
                    rules.Must(stop => stop.TimeFromPrevStop == TimeSpan.Zero).WithMessage("Number of minutes from the previous stop for the first stop must be 0");
                });
            RuleFor(input => input.Stops.Skip(1))
                .ForEach(rules =>
                {
                    rules.MustAsync(async (stop, cancellationToken) =>
                    {
                        City? c = await dbContext.Cities.FirstOrDefaultAsync(user => user.Id == stop.CityId, cancellationToken);
                        return c is not null;
                    });
                    rules.Must(stop => stop.KmFromPrevStop > 0).WithMessage("Value cannot be less than 0");
                    rules.NotNull().WithMessage("Value cannot be empty");
                    rules.Must(stop => stop.TimeFromPrevStop > TimeSpan.FromMinutes(0)).WithMessage("Value cannot be less than 0");
                });

    }
}