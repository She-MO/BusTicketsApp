using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Passengers;

public record AddPassengerInput(string FirstName, string LastName, DateOnly BirthDate);