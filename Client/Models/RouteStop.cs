
namespace Client.Models;

public class RouteStop
{
    public int Id { get; init; }
    public required int RouteId { get; init; }
    public Route? Route { get; init; }
    public required int CityId { get; init; }
    public City? City { get; init; }
    public required byte Sequence { get; init; }
}