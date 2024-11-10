using System.ComponentModel.DataAnnotations;

namespace BusTicketsApp.Server.Users;

public record LogInUserInput(
    [property: Required]
    [property: EmailAddress]
    string Email, 
    [property: Required(ErrorMessage = "Password can not be empty.")]
    [property: Length(10, 20)] 
    string Password);