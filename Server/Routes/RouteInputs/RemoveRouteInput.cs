namespace BusTicketsApp.Server.Routes;
using BusTicketsApp.Server.Data;

public record RemoveRouteInput([property: ID<Route>] int Id);