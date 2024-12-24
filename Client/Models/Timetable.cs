
namespace Client.Models;

public sealed class Timetable
{
    public string Id { get; init; }
    public int RouteId { get; init; }
    public Route? Route { get; set; }
    public required TimeOnly TimeOfDeparture { get; set; }
    public required TimeOnly TimeOfArrival { get; set; }
    public required DayOfWeek DayOfWeek { get; set; }

    public IList<Trip> Trips { get; init; } = new List<Trip>();
}