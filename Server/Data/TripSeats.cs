namespace BusTicketsApp.Server.Data;

public sealed class TripSeats
{
    [IsProjected(true)]
    public required int TripId { get; init; }
    [IsProjected(true)]
    public required int RouteId { get; init; }
    [IsProjected(true)]
    public required byte Sequence { get; init; }
    public RouteStop? RouteStop { get; init; }
    public required byte AvailableSeats { get; set; }
}