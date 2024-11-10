using System.ComponentModel.DataAnnotations;

namespace BusTicketsApp.Server.Data;

public class Bus
{
    public int Id { get; init; }
    public required int NumberOfSeats { get; set; }
    [StringLength(20)]
    public required string BusNumber { get; set; }
    public required int CarrierId { get; init; }
    public Carrier? Carrier { get; init; }
}