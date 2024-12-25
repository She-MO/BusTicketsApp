using BusTicketsApp.Server.Data;
using BusTicketsApp.Server.Trips.TripInputs;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Trips.TripInputValidators;

public class AddTripInputValidator : AbstractValidator<AddTripInput>
{
    public AddTripInputValidator(ApplicationDbContext dbContext)
    {
        RuleFor(input => new { timetableId = input.TimetableId, Date = input.Date })
            .MustAsync(async (tuple, cancellationToken) =>
            {
                var timetable = await dbContext.Timetables.FirstOrDefaultAsync(t => t.Id == tuple.timetableId);
                return timetable is not null && timetable.DayOfWeek == tuple.Date.DayOfWeek;
            }).WithMessage("Day of week of specified date is not equal to the day of week for this timetable");
        RuleFor(input => new { BusId = input.BusId, Date = input.Date, TimetableId = input.TimetableId})
            .MustAsync(async (tuple, cancellationToken) =>
            {
                var trips = await dbContext.Trips.AsNoTracking()
                    .Where(trip => trip.Date == tuple.Date && trip.BusId == tuple.BusId).Include(trip => trip.Timetable).ToListAsync();
                var timetable = await dbContext.Timetables.FirstOrDefaultAsync(t => t.Id == tuple.TimetableId);
                foreach (var trip in trips)
                {
                    if (trip.Timetable.TimeOfDeparture >= timetable.TimeOfDeparture &&
                        trip.Timetable.TimeOfDeparture <= timetable.TimeOfArrival ||
                        trip.Timetable.TimeOfArrival >= timetable.TimeOfDeparture &&
                        trip.Timetable.TimeOfArrival <= timetable.TimeOfArrival)
                    {
                        return false;
                    }
                }
                return true;
            }).WithMessage("Specified bus is used for another trip at this time");
        RuleFor(input => input.PricePerKm)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(input => input.Date)
            .GreaterThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Date must be greater than current date")
            .LessThan(DateOnly.FromDateTime(DateTime.Today.AddMonths(2))).WithMessage("Difference between current date and date of the trip has to be less than 2 months");
    }
    
}