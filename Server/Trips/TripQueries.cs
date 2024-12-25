using AppAny.HotChocolate.FluentValidation;
using BusTicketsApp.Server.Data;
using BusTicketsApp.Server.Trips.TripInputs;
using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Trips;
[QueryType]
public static class TripQueries
{
    [UsePaging]
    //[UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Trip> GetTrips(ApplicationDbContext dbContext)
    {
        return dbContext.Trips.AsNoTracking();
    }
    public static async Task<IEnumerable<Trip>> FindTrips(
        [UseFluentValidation] FindTripInput input,
        ITripByIdDataLoader tripById,
        ISelection selection,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        /*
        var routes = await dbContext.RouteStops.Join(dbContext.RouteStops,
            rs1 => rs1.RouteId,
            rs2 => rs2.RouteId,
            (rs1, rs2) => new
            {
                RouteId = rs1.RouteId,
                RouteStop1Sequence = rs1.Sequence,
                RouteStop2Sequence = rs2.Sequence,
                RouteStop1City = rs1.CityId,
                RouteStop2City = rs2.CityId
            })
            .Where(res => 
                res.RouteStop1Sequence < res.RouteStop2Sequence && 
                res.RouteStop1City == input.FromCityId && 
                res.RouteStop2City == input.ToCityId)
            .ToDictionaryAsync(res => 
                res.RouteId, 
                res => new
                {
                    rsc1 = res.RouteStop1City, 
                    rsc2 = res.RouteStop2City, 
                    rss1 = res.RouteStop1Sequence , 
                    rss2 = res.RouteStop2Sequence
                });
        var trips = dbContext.Trips
            .Where(trip => trip.Date == input.Date)
            .Include(trip => trip.TripSeats)
            .Include(trip => trip.Timetable)
            .AsEnumerable()
            .Where(trip =>
            {
                if (routes.TryGetValue(trip.Timetable.RouteId, out var value))
                {
                    return trip.TripSeats.Skip(value.rss1 - 1).SkipLast(trip.TripSeats.NumberOfTickets - (value.rss2 - 1)).Min(rs => rs.AvailableSeats) > input.numberOfPassengers;
                }
                return false;
            }).ToList();
        */
        var tripIds = dbContext.RouteStops.Join(dbContext.RouteStops,
                rs1 => rs1.RouteId,
                rs2 => rs2.RouteId,
                (rs1, rs2) => new
                {
                    RouteId = rs1.RouteId,
                    RouteStop1Sequence = rs1.Sequence,
                    RouteStop2Sequence = rs2.Sequence,
                    RouteStop1City = rs1.CityId,
                    RouteStop2City = rs2.CityId
                })
            .Where(res =>
                res.RouteStop1Sequence < res.RouteStop2Sequence &&
                res.RouteStop1City == input.FromCityId &&
                res.RouteStop2City == input.ToCityId)
            .Join(dbContext.Timetables,
                r => r.RouteId,
                t => t.RouteId,
                (route, timetable) => new
                {
                    //RouteId = route.RouteId,
                    RouteStop1Sequence = route.RouteStop1Sequence,
                    RouteStop2Sequence = route.RouteStop2Sequence,
                    RouteStop1City = route.RouteStop1City,
                    RouteStop2City = route.RouteStop2City,
                    TimetableId = timetable.Id
                })
            .Join(dbContext.Trips,
                route => route.TimetableId,
                trip => trip.TimetableId,
                (route, trip) => new
                {
                    RouteStop1Sequence = route.RouteStop1Sequence,
                    RouteStop2Sequence = route.RouteStop2Sequence,
                    RouteStop1City = route.RouteStop1City,
                    RouteStop2City = route.RouteStop2City,
                    TripId = trip.Id,
                    TripDate = trip.Date,
                    TripSeats = trip.TripSeats.OrderBy(ts => ts.Sequence).ToList()
                })
            .AsEnumerable()
            .Where(trip =>
                trip.TripDate == input.Date &&
                trip.TripSeats.Skip(trip.RouteStop1Sequence - 1)
                    .SkipLast(trip.TripSeats.Count - (trip.RouteStop2Sequence - 1)).Min(rs => rs.AvailableSeats) >=
                input.numberOfPassengers)
            .Select(trip => trip.TripId)
            .ToArray();
        return await tripById
            .Select(selection)
            .LoadRequiredAsync(tripIds ,cancellationToken);
    }

    [NodeResolver]
    public static async Task<Trip?> GetTripByIdAsync(
        int id,
        ITripByIdDataLoader carrierById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await carrierById
            .Select(selection)
            .LoadAsync(id, cancellationToken);
    }

    public static async Task<IEnumerable<Trip>> GetTripsByIdAsync(
        [ID<Trip>] int[] ids,
        ITripByIdDataLoader carrierById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await carrierById
            .Select(selection)
            .LoadRequiredAsync(ids, cancellationToken);
    }

}
