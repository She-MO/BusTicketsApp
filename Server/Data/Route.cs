
namespace BusTicketsApp.Server.Data;

public class Route
{
    public int Id { get; init; }
    public string? ShortName { get; init; }
    public string? FullName { get; init; }
    public IList<RouteStop> RouteStops { get; init; }= new List<RouteStop>();
}