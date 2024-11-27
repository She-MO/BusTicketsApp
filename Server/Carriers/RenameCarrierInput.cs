using System.ComponentModel.DataAnnotations;
using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Carriers;

public sealed record RenameCarrierInput([property: ID<Carrier>] int Id, 
    [property: Required(ErrorMessage = "Carrier name cannot be empty")]
    [property: Length(3, 30, ErrorMessage = "Carrier name should be 3-30 characters long")]
    string Name);