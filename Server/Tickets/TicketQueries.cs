using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;

namespace BusTicketsApp.Server.Tickets;

[QueryType]
public static class TicketQueries
{
    [NodeResolver]
    public static async Task<Ticket?> GetTicketById(
        int id,
        ITicketByIdDataLoader ticketById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await ticketById
            .Select(selection)
            .LoadAsync(id, cancellationToken);
    }
    
}