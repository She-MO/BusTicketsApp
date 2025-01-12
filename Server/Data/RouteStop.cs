
namespace BusTicketsApp.Server.Data;

public sealed class RouteStop
{
    [IsProjected(true)]
    public required int RouteId { get; init; }
    public Route? Route { get; init; }
    [IsProjected(true)]
    public required int CityId { get; init; }
    public City? City { get; init; }
    public required byte Sequence { get; init; }
    public required TimeSpan TimeFromPrevStop { get; set; }
    public required int KmFromPrevStop { get; set; }
    public IEnumerable<TripSeats> TripSeats { get; init; } = new List<TripSeats>();
}