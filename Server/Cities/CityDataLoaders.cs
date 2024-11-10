using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Cities;

public static class CityDataLoaders
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, City>> GetCityByIdAsync(
        IReadOnlyList<int> ids,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        CancellationToken cancellationToken)
    {
        return await dbContext.Cities
            .AsNoTracking()
            .Where(c => ids.Contains(c.Id))
            .Select(c => c.Id, selector)
            .ToDictionaryAsync(c => c.Id, cancellationToken);
    }
}