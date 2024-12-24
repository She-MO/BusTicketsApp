using System.Security.Claims;
using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;
using HotChocolate.Pagination;
using HotChocolate.Types.Pagination;
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
    [UsePaging]
    public static async Task<Connection<Ticket>> GetTicketsByUserIdAsync(
        ITicketsByUserIdDataLoader ticketsByUserId,
        PagingArguments pagingArguments,
        ISelection selection,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken cancellationToken)
    {
        var userId = Int32.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? "-1");
        return await ticketsByUserId
            .WithPagingArguments(pagingArguments)
            .Select(selection)
            .LoadAsync(userId, cancellationToken)
            .ToConnectionAsync();
    }
}