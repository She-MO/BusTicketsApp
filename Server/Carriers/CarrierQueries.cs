using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Carriers;

[QueryType]
public static class CarrierQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Carrier> GetCarriers(ApplicationDbContext dbContext)
    {
        return dbContext.Carriers.AsNoTracking();
    }

    [NodeResolver]
    public static async Task<Carrier?> GetCarrierByIdAsync(
        int id,
        ICarrierByIdDataLoader carrierById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await carrierById
            .Select(selection)
            .LoadAsync(id, cancellationToken);
    }

    public static async Task<IEnumerable<Carrier>> GetCarriersByIdAsync(
        [ID<Carrier>] int[] ids,
        ICarrierByIdDataLoader carrierById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await carrierById
            .Select(selection)
            .LoadRequiredAsync(ids, cancellationToken);
    }

}