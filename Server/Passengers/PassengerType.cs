using BusTicketsApp.Server.Data;
using BusTicketsApp.Server.Users;
using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;

namespace BusTicketsApp.Server.Passengers;

[ObjectType<Passenger>]
public static partial class PassengerType
{
    public static async Task<User?> GetUserAsync(
        [Parent(nameof(Passenger.UserId))] Passenger passenger,
        IUserByIdDataLoader userById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await userById
            .Select(selection)
            .LoadAsync(passenger.UserId, cancellationToken);
    }
}