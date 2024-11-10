using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Passengers;

public record DeletePassengerInput([property: ID<Passenger>] int Id);