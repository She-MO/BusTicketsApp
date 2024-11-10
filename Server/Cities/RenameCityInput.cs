using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Cities;

public record RenameCityInput([property: ID<City>] int Id, string Name);