namespace BusTicketsApp.Server.Cities;
public sealed class CityNotFoundException() : Exception("City not found.");
public sealed class CityNameAlreadyInUseException() : Exception("City name already in use");
public sealed class CityNameEmptyException() : Exception("City name can not be empty");

public sealed class CityUsedInRouteException() : Exception("City used in route and cannot be deleted");
