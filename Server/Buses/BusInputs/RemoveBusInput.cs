using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Buses;

public record RemoveBusInput([property: ID<Bus>] int Id);