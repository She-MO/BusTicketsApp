using BusTicketsApp.Server.Data;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Buses;

[MutationType]
public static class BusMutations
{
    [Error<IncorrectNumberOfSeatsException>]
    [Error<BusNumberAlreadyInUseException>]
    public static async Task<Bus> AddBusAsync(
        AddBusInput input,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        if (input.NumberOfSeats < 1)
        {
            throw new IncorrectNumberOfSeatsException();
        }
        var bus = await dbContext.Buses.FirstOrDefaultAsync(b => b.BusNumber == input.BusNumber, cancellationToken);
        if (bus is not null)
        {
            throw new BusNumberAlreadyInUseException();
        }
        bus = new Bus { BusNumber = input.BusNumber, NumberOfSeats = input.NumberOfSeats, CarrierId = input.CarrierId };
        dbContext.Add(bus);
        await dbContext.SaveChangesAsync(cancellationToken);
        return bus;
    }
    [Authorize(Policy = "IsManagerOrAdmin")]
    [Error<BusUsedInTripException>]
    public static async Task<bool> RemoveBusAsync(
        RemoveBusInput input,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        try
        {
            int result = await dbContext.Buses.Where(bus => bus.Id == input.Id).ExecuteDeleteAsync(cancellationToken);
            return Convert.ToBoolean(result);
        }
        catch (Exception e)
        {
            //Console.WriteLine(e);
            throw new BusUsedInTripException();
        }
    }
}