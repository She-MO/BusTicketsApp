using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Cities;
public record RemoveCityInput([property: ID<City>] int Id);
