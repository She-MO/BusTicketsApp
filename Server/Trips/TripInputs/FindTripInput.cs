using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Trips.TripInputs;

public record FindTripInput([property: ID<City>] int FromCityId, [property: ID<City>] int ToCityId, DateOnly Date, int numberOfPassengers);