using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using HotChocolate.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Tickets;

public static class TicketsDataLoaders
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Ticket>> TicketByIdAsync(
        IReadOnlyList<int> ids,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        CancellationToken cancellationToken)
    {
        return await dbContext.Tickets
            .AsNoTracking()
            .Where(c => ids.Contains(c.Id))
            .Select(c => c.Id, selector)
            .ToDictionaryAsync(c => c.Id, cancellationToken);
    }
    
}