using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Tickets.TicketInputs;

public record CancelTicketInput([property: ID<Ticket>] int Id);