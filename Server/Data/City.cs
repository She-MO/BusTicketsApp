using System.ComponentModel.DataAnnotations;

namespace BusTicketsApp.Server.Data;

public sealed class City
{
    [IsProjected(true)]
    public int Id { get; init; }
    [StringLength(50)] 
    public required string Name { get; set; } 
}