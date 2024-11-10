using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Users;

[QueryType]
public static class UserQueries
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<User> GetUsers(ApplicationDbContext dbContext)
    {
        return dbContext.Users
            .AsNoTracking()
            .OrderBy(u => u.Email);
    }

    [NodeResolver]
    public static async Task<User?> GetUserById(
        int id,
        IUserByIdDataLoader userById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await userById
            .Select(selection)
            .LoadAsync(id, cancellationToken);
    }

    public static async Task<IEnumerable<User>> GetUsersById(
        [ID<User>] int[] ids,
        IUserByIdDataLoader userById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await userById
            .Select(selection)
            .LoadRequiredAsync(ids, cancellationToken);
    }
}