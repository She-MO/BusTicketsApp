using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Tickets.TicketInputs;

public record BuyTicketInput([property: ID<Trip>] int TripId, [property: ID<City>] int FromCityId, [property: ID<City>] int ToCityId, int numberOfPassengers);
