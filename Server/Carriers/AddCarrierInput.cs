using System.ComponentModel.DataAnnotations;

namespace BusTicketsApp.Server.Carriers;

public sealed record AddCarrierInput(
    [property: Required(ErrorMessage = "Carrier name cannot be empty")]
    [property: Length(3, 30, ErrorMessage = "Carrier name should be 3-30 characters long")]
    string Name);