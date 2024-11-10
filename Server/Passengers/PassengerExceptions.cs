namespace BusTicketsApp.Server.Passengers;
public sealed class PassengerNotFoundException() : Exception("Passenger not found.");
public sealed class MaxNumberOfPassengersReachedException() : Exception("Max number of passengers reached.");
public sealed class PassengerNameEmptyException() : Exception("Passenger's first name and last name can not be empty");
public sealed class InvalidBirthDateException() : Exception("Invalid birth date");
public sealed class UserIdEmptyException() : Exception("User id can not be empty");

public sealed class CurrentUserIdDoesNotMatchPassengerIdException()
    : Exception("Specified passenger does not belong to current user");


