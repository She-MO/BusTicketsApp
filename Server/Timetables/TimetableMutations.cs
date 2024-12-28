using AppAny.HotChocolate.FluentValidation;
using BusTicketsApp.Server.Data;
using BusTicketsApp.Server.Routes;
using BusTicketsApp.Server.Timetables.TimetableInputs;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
using Route = Microsoft.AspNetCore.Routing.Route;

namespace BusTicketsApp.Server.Timetables;

[MutationType]
public static class TimetableMutations
{
    [Authorize(Policy = "IsManagerOrAdmin")]
    [Error<TimetableWithThisSettingsAlreadyExists>]
    public static async Task<Timetable> AddTimetable(
        [UseFluentValidation] AddTimetableInput input,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        long ticks = dbContext.RouteStops.AsNoTracking().Where(rs => rs.RouteId == input.RouteId).AsEnumerable()
            .Sum(rs => rs.TimeFromPrevStop.Ticks);
        var timeOfArrival = new TimeSpan(ticks);
        timeOfArrival += input.TimeOfDeparture.ToTimeSpan();
        var timetable = new Timetable
        {
            RouteId = input.RouteId,
            DayOfWeek = input.DayOfWeek,
            TimeOfDeparture = input.TimeOfDeparture,
            TimeOfArrival = TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(timeOfArrival.Hours * 60 + timeOfArrival.Minutes))
        };
        dbContext.Timetables.Add(timetable);
        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            throw new TimetableWithThisSettingsAlreadyExists();
        }
        return timetable;
    }
    
    [Authorize(Policy = "IsManagerOrAdmin")]
    [Error<TimetableUsedInTripException>]
    public static async Task<bool> RemoveTimetableAsync(
        RemoveTimetableInput input,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        try
        {
            int result = await dbContext.Timetables.Where(timetable => timetable.Id == input.Id).ExecuteDeleteAsync(cancellationToken);
            return Convert.ToBoolean(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new TimetableUsedInTripException();
        }
    }
}