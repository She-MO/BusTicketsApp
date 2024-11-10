using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Buses;

public static class BusDataLoaders
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Bus>> BusByIdAsync(
        IReadOnlyList<int> ids,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        CancellationToken cancellationToken)
    {
        return await dbContext.Buses
            .AsNoTracking()
            .Where(b => ids.Contains(b.Id))
            .Select(b => b.Id, selector)
            .ToDictionaryAsync(b => b.Id, cancellationToken);
    }
}