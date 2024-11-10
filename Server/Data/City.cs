using System.ComponentModel.DataAnnotations;

namespace BusTicketsApp.Server.Data;

public class City
{
    public int Id { get; init; }
    [StringLength(50)] 
    public required string Name { get; set; } 
}