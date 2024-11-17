using System.ComponentModel.DataAnnotations;

namespace Client.Models;

public sealed class Carrier
{
    public string Id { get; init; }
    [StringLength((50))]
    public required string Name { get; set; }

    public ICollection<Bus> Buses { get; init; } = new List<Bus>();
}