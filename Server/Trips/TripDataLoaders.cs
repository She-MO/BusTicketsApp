using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Trips;

public static class TripDataLoaders
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Trip>> TripByIdAsync(
        IReadOnlyList<int> ids,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        CancellationToken cancellationToken)
    {
        return await dbContext.Trips
            .AsNoTracking()
            .Where(b => ids.Contains(b.Id))
            .Select(b => b.Id, selector)
            .ToDictionaryAsync(b => b.Id, cancellationToken);
    }
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Ticket[]>> TicketsByTripIdAsync(
        IReadOnlyList<int> tripIds,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        CancellationToken cancellationToken)
    {
        return await dbContext.Trips
            .AsNoTracking()
            .Where(b => tripIds.Contains(b.Id))
            .Select(b => b.Id, b => b.Tickets, selector)
            .ToDictionaryAsync(r => r.Key, r => r.Value.ToArray(), cancellationToken);
    }
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, TripSeats[]>> TripSeatsByTripIdAsync(
        IReadOnlyList<int> tripIds,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        CancellationToken cancellationToken)
    {
        return await dbContext.Trips
            .AsNoTracking()
            .Where(b => tripIds.Contains(b.Id))
            .Select(b => b.Id, b => b.TripSeats, selector)
            .ToDictionaryAsync(r => r.Key, r => r.Value.ToArray(), cancellationToken);
    }
    
}