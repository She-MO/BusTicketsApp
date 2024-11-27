using System.ComponentModel.DataAnnotations;

namespace BusTicketsApp.Server.Data;

public sealed class User
{
    public int Id { get; init; }
    [StringLength(100)]
    public required string Email { get; set; } 
    public required string Password { get; set; } 
    [StringLength(40)]
    public string? Phone { get; set; }

    public Roles Role { get; set; } = Roles.User;
    public ICollection<Ticket> Tickets { get; init; } = new List<Ticket>();

}