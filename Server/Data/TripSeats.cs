namespace BusTicketsApp.Server.Data;

public sealed class TripSeats
{
    public required int TripId { get; init; }
    public required int RouteId { get; init; }
    public required byte Sequence { get; init; }
    public RouteStop? RouteStop { get; init; }
    public required byte AvailableSeats { get; set; }
}