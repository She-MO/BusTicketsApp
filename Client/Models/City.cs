using System.ComponentModel.DataAnnotations;

namespace Client.Models;

public class City
{
    public string Id { get; init; }
    [StringLength(50)] 
    public required string Name { get; set; } 
}