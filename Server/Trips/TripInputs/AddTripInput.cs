using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Trips.TripInputs;

public record AddTripInput([property: ID<Bus>] int BusId, [property: ID<Timetable>] int TimetableId, DateOnly Date, Decimal PricePerKm);