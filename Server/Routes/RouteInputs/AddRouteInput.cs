using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Routes;

public record AddRouteInput(string ShortName, List<RouteStopInput> Stops);