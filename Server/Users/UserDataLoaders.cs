using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Users;

public static class UserDataLoaders
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, User>> UserByIdAsync(
        IReadOnlyList<int> ids,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .AsNoTracking()
            .Where(u => ids.Contains(u.Id))
            .Select(u => u.Id, selector)
            .ToDictionaryAsync(u => u.Id, cancellationToken);
    }

    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Passenger[]>> PassengersByUserIdAsync(
        IReadOnlyList<int> userIds,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .AsNoTracking()
            .Where(u => userIds.Contains(u.Id))
            .Select(u => u.Id, u => u.Passengers, selector)
            .ToDictionaryAsync(u => u.Key, u => u.Value.ToArray(), cancellationToken);
    }
}