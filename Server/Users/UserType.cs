using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;

namespace BusTicketsApp.Server.Users;

[ObjectType<User>]
public static partial class UserType
{
    public static async Task<Passenger[]?> GetPassengersAsync(
        [Parent] User user,
        IPassengersByUserIdDataLoader passengersByUserId,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await passengersByUserId
            .Select(selection)
            .LoadAsync(user.Id, cancellationToken);
    }
}