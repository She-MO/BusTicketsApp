namespace Client.Models;

public sealed class Ticket
{
    public string Id { get; init; }
    public byte NumberOfPassengers { get; init; }
    public string TripId { get; init; }
    public Trip? Trip { get; init; }
    public string UserId { get; init; }
    public User? User { get; init; }
    public string StartCityId { get; init; }
    public City? StartCity { get; init; }
    public string EndCityId { get; init; }
    public City? EndCity { get; init; }
    public decimal TotalPrice { get; init; }
}