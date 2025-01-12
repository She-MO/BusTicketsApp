
namespace BusTicketsApp.Server.Data;

public sealed class Route
{
    [IsProjected(true)]
    public int Id { get; init; }
    public string? ShortName { get; set; }
    public int LengthInKm { get; set; }
    public IList<RouteStop> RouteStops { get; init; }= new List<RouteStop>();
    public IList<Timetable> Timetables { get; init; }= new List<Timetable>();
}