namespace BusTicketsApp.Server.Buses;

public sealed class BusNumberAlreadyInUseException() : Exception("Bus number already in use");
public sealed class IncorrectNumberOfSeatsException() : Exception("Incorrect number of seats");
