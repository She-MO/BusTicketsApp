using System.ComponentModel.DataAnnotations;

namespace Client.Models;

public class City
{
    public int Id { get; init; }
    [StringLength(50)] 
    public required string Name { get; set; } 
}