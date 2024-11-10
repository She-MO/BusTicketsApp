namespace BusTicketsApp.Server.Users;

public sealed class EmailAlreadyInUseException() : Exception("Email already in use");
public sealed class InvalidEmailOrPasswordException() : Exception("Invalid email or password");
