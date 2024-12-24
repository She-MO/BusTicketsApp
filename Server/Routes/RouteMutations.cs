using AppAny.HotChocolate.FluentValidation;
using BusTicketsApp.Server.Buses;
using BusTicketsApp.Server.Data;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
using Route = BusTicketsApp.Server.Data.Route;

namespace BusTicketsApp.Server.Routes;

[MutationType]
public static class RouteMutations
{
    [Authorize(Policy = "IsAdmin")]
    [Error<RouteNameIsAlreadyInUseException>]
    public static async Task<Route> AddRoute(
        [UseFluentValidation] AddRouteInput input,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var route = new Route
        {
            ShortName = input.ShortName,
            LengthInKm = input.Stops.Sum(stop => stop.KmFromPrevStop)
        };
        dbContext.Routes.Add(route);
        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            throw new RouteNameIsAlreadyInUseException();
        }
        for (int i = 0; i < input.Stops.Count; i++)
        {
            var stop = new RouteStop()
            {
                CityId = input.Stops[i].CityId,
                Sequence = (byte)(i + 1),
                KmFromPrevStop = input.Stops[i].KmFromPrevStop,
                TimeFromPrevStop = input.Stops[i].TimeFromPrevStop,
                RouteId = route.Id
            };
            dbContext.RouteStops.Add(stop);
            route.RouteStops.Add(stop);
        }
        await dbContext.SaveChangesAsync(cancellationToken);
        return route;
    }
    [Authorize(Policy = "IsAdmin")]
    [Error<RouteIsUsedInTimetableException>]
    public static async Task<bool> RemoveRouteAsync(
        RemoveRouteInput input,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        try
        {
            int result = await dbContext.Routes.Where(route => route.Id == input.Id).ExecuteDeleteAsync(cancellationToken);
            return Convert.ToBoolean(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new RouteIsUsedInTimetableException();
        }
    }
}