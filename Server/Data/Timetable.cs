namespace BusTicketsApp.Server.Data;

public sealed class Timetable
{
    public int Id { get; init; }
    [IsProjected(true)]
    public required int RouteId { get; init; }
    public Route? Route { get; set; }
    public required TimeOnly TimeOfDeparture { get; set; }
    public required TimeOnly TimeOfArrival { get; set; }
    public required DayOfWeek DayOfWeek { get; set; }
    public ICollection<Trip> Trips { get; init; } = new List<Trip>();
    
}