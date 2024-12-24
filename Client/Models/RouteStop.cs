
namespace Client.Models;

public class RouteStop
{
    public required int RouteId { get; init; }
    public Route? Route { get; init; }
    public required int CityId { get; init; }
    public City? City { get; init; }
    public required byte Sequence { get; init; }
    public required TimeSpan TimeFromPrevStop { get; set; }
    public required int KmFromPrevStop { get; set; }
}