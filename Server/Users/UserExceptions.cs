namespace BusTicketsApp.Server.Users;

public sealed class EmailAlreadyInUseException() : Exception("Email already in use");