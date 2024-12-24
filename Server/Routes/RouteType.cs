using BusTicketsApp.Server.Data;
using Route = BusTicketsApp.Server.Data.Route;

namespace BusTicketsApp.Server.Routes;

[ObjectType<Route>]
public static partial class RouteType
{
    public static async Task<IEnumerable<RouteStop>> GetRouteStopsAsync(
        [Parent] Route route,
        IRouteStopsByRouteIdDataLoader routeStopsByRouteId,
        CancellationToken cancellationToken)
    {
        return await routeStopsByRouteId.LoadRequiredAsync(route.Id, cancellationToken);
    }
    public static async Task<IEnumerable<Timetable>> GetTimetablesAsync(
        [Parent] Route route,
        ITimetablesByRouteIdDataLoader timetablesByRouteId,
        CancellationToken cancellationToken)
    {
        return await timetablesByRouteId.LoadRequiredAsync(route.Id, cancellationToken);
    }
}
