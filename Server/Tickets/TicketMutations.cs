using System.Security.Claims;
using BusTicketsApp.Server.Data;
using BusTicketsApp.Server.Tickets.TicketInputs;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BusTicketsApp.Server.Tickets;

[MutationType]
public static class TicketMutations
{
    [Authorize]
    [Error<OneOfTheCitiesIsNotPartOfTheRouteException>]
    [Error<IncorrectCitySequence>]
    [Error<NotEnoughSeatsException>]    
    public static async Task<Ticket> BuyTicket(
        BuyTicketInput input,
        ApplicationDbContext dbContext,
        ClaimsPrincipal principal,
        CancellationToken cancellationToken)
    {
        var trip = await dbContext.Trips.Where(trip => trip.Id == input.TripId).Include(trip => trip.TripSeats)
            .Include(trip => trip.Timetable).ThenInclude(timetable => timetable.Route)
            .ThenInclude(route => route.RouteStops).FirstAsync(cancellationToken);
        RouteStop? firstCityInRoute = trip.Timetable.Route.RouteStops.FirstOrDefault(rs => rs.CityId == input.FromCityId);
        RouteStop? secondCityInRoute = trip.Timetable.Route.RouteStops.FirstOrDefault(rs => rs.CityId == input.ToCityId);
        if (firstCityInRoute is null || secondCityInRoute is null)
        {
            throw new OneOfTheCitiesIsNotPartOfTheRouteException();
        }

        if (firstCityInRoute.Sequence >= secondCityInRoute.Sequence)
        {
            throw new IncorrectCitySequence();
        }
        foreach (var tripSeats in trip.TripSeats)
        {
            if (tripSeats.Sequence >= firstCityInRoute.Sequence && tripSeats.Sequence < secondCityInRoute.Sequence)
            {
                if (tripSeats.AvailableSeats - input.numberOfPassengers < 0)
                {
                    throw new NotEnoughSeatsException();
                }

                tripSeats.AvailableSeats -= (byte)input.numberOfPassengers;
            }
        }

        var ticket = new Ticket()
        {
            TripId = input.TripId,
            StartCityId = input.FromCityId,
            EndCityId = input.ToCityId,
            NumberOfPassengers = (byte)input.numberOfPassengers,
            UserId = Int32.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier)),
            TotalPrice = trip.Timetable.Route.RouteStops.Skip(firstCityInRoute.Sequence).SkipLast(trip.Timetable.Route.RouteStops.Count - secondCityInRoute.Sequence).Sum(rs => rs.KmFromPrevStop) * trip.PricePerKm * input.numberOfPassengers
        };
        dbContext.Tickets.Add(ticket);
        await dbContext.SaveChangesAsync(cancellationToken);
        return ticket;
    }
    [Authorize]
    [Error<UserIsNotAuthorizedToCancelTicketException>]
    [Error<CannotCancelTicketForTripThatAlreadyHappened>]
    public static async Task<bool> CancelTicket(
        CancelTicketInput input,
        ApplicationDbContext dbContext,
        ClaimsPrincipal principal,
        CancellationToken cancellationToken)
    {
        var ticket = await dbContext.Tickets.Where(ticket => ticket.Id == input.Id).Include(t => t.Trip)
            .ThenInclude(t => t.Timetable).Include(t => t.Trip).ThenInclude(t => t.TripSeats).Include(t => t.Trip)
            .ThenInclude(t => t.Timetable).ThenInclude(t => t.Route).ThenInclude(r => r.RouteStops).AsSplitQuery().FirstOrDefaultAsync(cancellationToken);
        RouteStop? firstCityInRoute = ticket.Trip.Timetable.Route.RouteStops.FirstOrDefault(rs => rs.CityId == ticket.StartCityId);
        RouteStop? secondCityInRoute = ticket.Trip.Timetable.Route.RouteStops.FirstOrDefault(rs => rs.CityId == ticket.EndCityId);
        if (Int32.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier)) != ticket.UserId)
        {
            throw new UserIsNotAuthorizedToCancelTicketException();
        }

        if (ticket.Trip.Date < DateOnly.FromDateTime(DateTime.Today))
        {
            throw new CannotCancelTicketForTripThatAlreadyHappened();
        }
        foreach (var tripSeats in ticket.Trip.TripSeats)
        {
            if (tripSeats.Sequence >= firstCityInRoute.Sequence && tripSeats.Sequence < secondCityInRoute.Sequence)
            {
                tripSeats.AvailableSeats += ticket.NumberOfPassengers;
            }
        }
        await dbContext.SaveChangesAsync(cancellationToken);
        dbContext.Tickets.Remove(ticket);
        int res = await dbContext.SaveChangesAsync(cancellationToken);
        return Convert.ToBoolean(res);
    }
}