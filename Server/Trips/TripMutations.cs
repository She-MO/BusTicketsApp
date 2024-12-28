using AppAny.HotChocolate.FluentValidation;
using BusTicketsApp.Server.Buses;
using BusTicketsApp.Server.Data;
using BusTicketsApp.Server.Trips.TripInputs;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Trips;

[MutationType]
public static class TripMutations
{
    [Authorize(Policy = "IsManagerOrAdmin")]
    [Error<TripWithThisSettingsAlreadyExists>]
    public static async Task<Trip> AddTrip(
        [UseFluentValidation] AddTripInput input,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var trip = new Trip
        {
            BusId = input.BusId,
            TimetableId = input.TimetableId,
            Date = input.Date,
            PricePerKm = input.PricePerKm
        };
        var routeStops = await dbContext.Timetables.AsNoTracking().Where(t => t.Id == input.TimetableId).Include(t => t.Route)
            .ThenInclude(r => r.RouteStops).SelectMany(t => t.Route.RouteStops).OrderBy(rs => rs.Sequence).ToListAsync(cancellationToken);
        var bus = await dbContext.Buses.FirstAsync(bus => bus.Id == input.BusId, cancellationToken);
        dbContext.Trips.Add(trip);
        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            throw new TripWithThisSettingsAlreadyExists();
        }
        for (int i = 0; i < routeStops.Count - 1; i++)
        {
            var tripSeats = new TripSeats()
            {
                TripId = trip.Id,
                Sequence = routeStops[i].Sequence,
                RouteId = routeStops[i].RouteId,
                AvailableSeats = (byte)bus.NumberOfSeats
            };
            dbContext.TripSeats.Add(tripSeats);
        }
        await dbContext.SaveChangesAsync(cancellationToken);
        return trip;
    }
    
    [Authorize(Policy = "IsManagerOrAdmin")]
    public static async Task<bool> RemoveTripAsync(
        RemoveTripInput input,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        int result = await dbContext.Trips.Where(trip => trip.Id == input.TripId).ExecuteDeleteAsync(cancellationToken);
        return Convert.ToBoolean(result);
    }
}