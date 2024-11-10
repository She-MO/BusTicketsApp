using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Carriers;

public sealed record RenameCarrierInput([property: ID<Carrier>] int Id, string Name);