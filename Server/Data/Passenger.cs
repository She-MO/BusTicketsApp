using System.ComponentModel.DataAnnotations;

namespace BusTicketsApp.Server.Data;

public class Passenger
{
    public int Id { get; init; }
    [StringLength(30)]
    public required string FirstName { get; set; } = default!;
    [StringLength(30)]
    public required string LastName { get; set; } = default!;
    public required DateOnly DateOfBirth { get; set; }
    public required int UserId { get; init; }
    public User? User { get; init; }
}