using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Trips.TripInputs;

public record RemoveTripInput([property: ID<Trip>] int TripId);