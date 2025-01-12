using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using HotChocolate.Pagination;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Carriers;

public class CarrierDataLoaders
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Carrier>> CarrierByIdAsync(
        IReadOnlyList<int> ids,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        CancellationToken cancellationToken)
    {
        return await dbContext.Carriers
            .AsNoTracking()
            .Where(c => ids.Contains(c.Id))
            .Select(c => c.Id, selector)
            .ToDictionaryAsync(c => c.Id, cancellationToken);
    }

    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Page<Bus>>> BusesByCarrierIdAsync(
        IReadOnlyList<int> carrierIds,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        PagingArguments pagingArguments,
        CancellationToken cancellationToken)
    {
        return await dbContext.Buses
            .AsNoTracking()
            .Where(b => carrierIds.Contains(b.CarrierId))
            .OrderBy(b => b.Id)
            .Select(b => b.CarrierId, selector)
            .ToBatchPageAsync(b => b.CarrierId, pagingArguments, cancellationToken);
    }
}