using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using HotChocolate.Authorization;
using HotChocolate.Execution.Processing;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Buses;

[QueryType]
public static class BusQueries
{
    //[Authorize]
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Bus> GetBuses(ApplicationDbContext dbContext)
    {
        return dbContext.Buses
            .AsNoTracking()
            .OrderBy(b => b.Id);
    }

    [NodeResolver]
    public static async Task<Bus?> GetBusByIdAsync(
        int id,
        IBusByIdDataLoader busById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await busById
            .Select(selection)
            .LoadAsync(id, cancellationToken);
    }
    public static async Task<IEnumerable<Bus>> GetBusesByIdAsync(
        [ID<Bus>] int[] ids,
        IBusByIdDataLoader busById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await busById
            .Select(selection)
            .LoadRequiredAsync(ids, cancellationToken);
    }
}