using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Carriers;
public sealed record RemoveCarrierInput([property: ID<Carrier>] int Id);
