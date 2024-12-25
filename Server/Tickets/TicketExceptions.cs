namespace BusTicketsApp.Server.Tickets;

public sealed class OneOfTheCitiesIsNotPartOfTheRouteException() : Exception("One of the cities is not part of the route");
public sealed class IncorrectCitySequence() : Exception("Departure and arrival cities are in reverse order in the route");
public sealed class NotEnoughSeatsException() : Exception("Not enough seats available for specified number of passengers");
public sealed class UserIsNotAuthorizedToCancelTicketException() : Exception("User is not authorized to cancel this ticket");
public sealed class CannotCancelTicketForTripThatAlreadyHappened() : Exception("This ticket is for trip that has already happened. You cannot cancel it.");
public sealed class CannotBuyTicketsIfLessThanOneHourLeftBeforeTripException() : Exception("You cannot buy tickets for the trip that already departed or will depart within one hour");
