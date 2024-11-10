using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;
using HotChocolate.Pagination;
using HotChocolate.Types.Pagination;

namespace BusTicketsApp.Server.Carriers;

[ObjectType<Carrier>]
public static partial class CarrierType
{
    [UsePaging]
    public static async Task<Connection<Bus>> GetBusesAsync(
        [Parent] Carrier carrier,
        IBusesByCarrierIdDataLoader busesByCarrierId,
        PagingArguments pagingArguments,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await busesByCarrierId
            .WithPagingArguments(pagingArguments)
            .Select(selection)
            .LoadAsync(carrier.Id, cancellationToken)
            .ToConnectionAsync();
    }
}