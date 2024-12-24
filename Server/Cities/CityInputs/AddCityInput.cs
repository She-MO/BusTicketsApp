using System.ComponentModel.DataAnnotations;

namespace BusTicketsApp.Server.Cities;

public record AddCityInput(
    [property: Required(ErrorMessage = "City name cannot be empty")]
    [property: Length(3, 30, ErrorMessage = "City name should be 3-30 characters long")]
    string Name);