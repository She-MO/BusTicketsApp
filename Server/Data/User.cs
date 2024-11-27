using System.ComponentModel.DataAnnotations;

namespace BusTicketsApp.Server.Data;

public class User
{
    public int Id { get; init; }
    [StringLength(100)]
    [EmailAddress]
    public required string Email { get; set; } 
    public required string Password { get; set; } 
    [StringLength(40)]
    [Phone]
    public string? Phone { get; set; }

    public Roles Role { get; set; } = Roles.User;

}