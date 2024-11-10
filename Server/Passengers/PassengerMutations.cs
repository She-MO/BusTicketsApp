using System.Security.Claims;
using BusTicketsApp.Server.Data;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Passengers;

[MutationType]
public static class PassengerMutations
{

    //[Authorize]
    [Error<UserIdEmptyException>]
    [Error<PassengerNameEmptyException>]
    [Error<MaxNumberOfPassengersReachedException>]
    public static async Task<Passenger> AddPassengerAsync(
        AddPassengerInput input,
        ClaimsPrincipal claimsPrincipal,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        if (String.IsNullOrEmpty(input.FirstName) || String.IsNullOrEmpty(input.LastName))
        {
            throw new PassengerNameEmptyException();
        }
        var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (String.IsNullOrEmpty(userId))
        {
            throw new UserIdEmptyException();
        }
        int numOfPassengers = await dbContext.Passengers.CountAsync(p => p.UserId == Int32.Parse(userId), cancellationToken);
        if (numOfPassengers >= 10)
        {
            throw new MaxNumberOfPassengersReachedException();
        }
        var passenger = new Passenger {  FirstName = input.FirstName, LastName = input.LastName, DateOfBirth = input.BirthDate, UserId = int.Parse(userId) };
        dbContext.Passengers.Add(passenger);
        await dbContext.SaveChangesAsync(cancellationToken);
        return passenger;
    }
    
    [Error<PassengerNameEmptyException>]
    [Error<PassengerNotFoundException>]
    public static async Task<Passenger> DeletePassengerAsync(
        DeletePassengerInput input,
        ApplicationDbContext dbContext,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        var passenger = await dbContext.Passengers.FirstOrDefaultAsync(c => c.Id == input.Id, cancellationToken);
        if (passenger is null)
        {
            throw new PassengerNotFoundException();
        }
        var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (String.IsNullOrEmpty(userId))
        {
            throw new UserIdEmptyException();
        }

        if (Int32.Parse(userId) != passenger.UserId)
        {
            throw new CurrentUserIdDoesNotMatchPassengerIdException();
        }
        dbContext.Passengers.Remove(passenger);
        await dbContext.SaveChangesAsync(cancellationToken);
        return passenger;
    }
}