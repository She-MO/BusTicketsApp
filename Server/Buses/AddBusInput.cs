using System.ComponentModel.DataAnnotations;
using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Buses;

public record AddBusInput : IValidatableObject
{
    [Required(ErrorMessage = "Bus number cannot be empty")]
    public required string BusNumber { get; init; }
    [Required(ErrorMessage = "Number of seats cannot be empty")]
    public required int NumberOfSeats { get; init; }
    [Required(ErrorMessage = "Carrier id cannot be empty")]
    [ID<Carrier>] 
    public required int CarrierId { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (NumberOfSeats is < 0 or > 1000)
        {
            yield return new ValidationResult("Invalid number of seats");
        }
    }
}