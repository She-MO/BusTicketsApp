using System.ComponentModel.DataAnnotations;

namespace BusTicketsApp.Server.Data;

public sealed class Carrier
{
    [IsProjected(true)]
    public int Id { get; init; }
    [StringLength((50))]
    public required string Name { get; set; }

    public ICollection<Bus> Buses { get; init; } = new List<Bus>();
}