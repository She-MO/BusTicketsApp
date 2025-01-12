namespace BusTicketsApp.Server.Data;

public sealed class Ticket
{
    public int Id { get; init; }
    public required byte NumberOfPassengers { get; init; }
    [IsProjected(true)]
    public required int TripId { get; init; }
    public Trip? Trip { get; init; }
    [IsProjected(true)]
    public required int UserId { get; init; }
    public User? User { get; init; }
    [IsProjected(true)]
    public required int StartCityId { get; init; }
    public City? StartCity { get; init; }
    [IsProjected(true)]
    public required int EndCityId { get; init; }
    public City? EndCity { get; init; }
    public required decimal TotalPrice { get; init; }
}