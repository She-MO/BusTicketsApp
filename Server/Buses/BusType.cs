using BusTicketsApp.Server.Carriers;
using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;
namespace BusTicketsApp.Server.Buses;

[ObjectType<Bus>]
public static partial class BusType
{
    public static async Task<Carrier?> GetCarrierAsync(
        [Parent(nameof(Bus.CarrierId))] Bus bus,
        ICarrierByIdDataLoader carrierById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await carrierById
            .Select(selection)
            .LoadAsync(bus.CarrierId, cancellationToken);
    }
}