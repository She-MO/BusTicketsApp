using BusTicketsApp.Server.Users;
using BusTicketsApp.Server.Data;
using BusTicketsApp.Server.Trips;
using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;

namespace TicketTicketsApp.Server.Tickets;

[ObjectType<Ticket>]
public static partial class TicketType
{
    static partial void Configure(IObjectTypeDescriptor<Ticket> descriptor)
    {
        descriptor
            .Field(s => s.UserId)
            .ID<User>();
    }
    public static async Task<User?> GetUserAsync(
        [Parent(nameof(Ticket.UserId))] Ticket ticket,
        IUserByIdDataLoader userById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await userById
            .Select(selection)
            .LoadAsync(ticket.UserId, cancellationToken);
    }
    public static async Task<Trip?> GetTripAsync(
        [Parent(nameof(Ticket.TripId))] Ticket ticket,
        ITripByIdDataLoader tripById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await tripById
            .Select(selection)
            .LoadAsync(ticket.TripId, cancellationToken);
    }
    
}