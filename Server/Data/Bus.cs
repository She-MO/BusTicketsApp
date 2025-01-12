using System.ComponentModel.DataAnnotations;

namespace BusTicketsApp.Server.Data;

public sealed class Bus
{
    [IsProjected(true)]
    public int Id { get; init; }
    public required int NumberOfSeats { get; set; }
    [StringLength(20)]
    public required string BusNumber { get; set; }
    [IsProjected(true)]
    public required int CarrierId { get; init; }
    public Carrier? Carrier { get; init; }
    public ICollection<Trip> Trips { get; init; } = new List<Trip>();
}