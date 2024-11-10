using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Buses;

public record AddBusInput(string BusNumber, int NumberOfSeats, [property: ID<Carrier>] int CarrierId);