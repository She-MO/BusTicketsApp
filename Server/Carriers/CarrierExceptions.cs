namespace BusTicketsApp.Server.Carriers;

public sealed class CarrierNotFoundException() : Exception("Carrier not found.");
public sealed class CarrierNameAlreadyInUseException() : Exception("Carrier name already in use");
public sealed class CarrierNameEmptyException() : Exception("Carrier name can not be empty");

public sealed class OneOfCarrierBusesUsedInTripException() : Exception("One of carrier buses is used and trip, the carrier cannot be deleted");
