using BusTicketsApp.Server.Cities;
using BusTicketsApp.Server.Data;
using BusTicketsApp.Server.Routes;
using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;
using Route = BusTicketsApp.Server.Data.Route;

namespace BusTicketsApp.Server.RouteStops;

[ObjectType<RouteStop>]
public static partial class RouteStopType
{
    static partial void Configure(IObjectTypeDescriptor<RouteStop> descriptor)
    {
        descriptor
            .Field(s => s.RouteId)
            .ID<Route>();
        descriptor
            .Field(s => s.CityId)
            .ID<City>();
    }
    public static async Task<City?> GetCityAsync(
        [Parent(nameof(RouteStop.CityId))] RouteStop stop,
        ICityByIdDataLoader cityById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await cityById
            .Select(selection)
            .LoadAsync(stop.CityId, cancellationToken);
    }
    public static async Task<Route?> GetRouteAsync(
        [Parent(nameof(RouteStop.RouteId))] RouteStop stop,
        IRouteByIdDataLoader routeById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await routeById
            .Select(selection)
            .LoadAsync(stop.RouteId, cancellationToken);
    }
}