namespace Client.Models;

public class Route
{
    public string Id { get; init; }
    public string? ShortName { get; init; }
    public string? FullName { get; init; }
    public IList<RouteStop> RouteStops { get; init; }= new List<RouteStop>();
}