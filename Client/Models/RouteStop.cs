
namespace Client.Models;

public class RouteStop
{
    public int RouteId { get; init; }
    public Route? Route { get; init; }
    public int CityId { get; init; }
    public City? City { get; init; }
    public byte Sequence { get; init; }
    public TimeSpan TimeFromPrevStop { get; set; }
    public int KmFromPrevStop { get; set; }
}