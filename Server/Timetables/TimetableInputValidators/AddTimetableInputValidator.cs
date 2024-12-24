using BusTicketsApp.Server.Data;
using BusTicketsApp.Server.Timetables.TimetableInputs;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Route = BusTicketsApp.Server.Data.Route;

namespace BusTicketsApp.Server.Timetables.TimetableInputValidators;

public class AddTimetableInputValidator : AbstractValidator<AddTimetableInput>
{
    public AddTimetableInputValidator(ApplicationDbContext dbContext)
    {
        RuleFor(input => input.RouteId)
            .MustAsync(async (routeId, cancellationToken) =>
            {
                Route? r = await dbContext.Routes.FirstOrDefaultAsync(route => route.Id == routeId, cancellationToken);
                return r is not null;
            }).WithMessage("Route with specified id doesn't exist");
    }

}