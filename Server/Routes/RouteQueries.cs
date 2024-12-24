using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;
using Microsoft.EntityFrameworkCore;
using Route = BusTicketsApp.Server.Data.Route;

namespace BusTicketsApp.Server.Routes;

[QueryType]
public static class RouteQueries
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Route> GetRoutes(ApplicationDbContext dbContext)
    {
        return dbContext.Routes.AsNoTracking();
    }

    [NodeResolver]
    public static async Task<Route?> GetRouteByIdAsync(
        int id,
        IRouteByIdDataLoader routeById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await routeById
            .Select(selection)
            .LoadAsync(id, cancellationToken);
    }

    public static async Task<IEnumerable<Route>> GetRoutesByIdAsync(
        [ID<Route>] int[] ids,
        IRouteByIdDataLoader routeById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await routeById
            .Select(selection)
            .LoadRequiredAsync(ids, cancellationToken);
    }

}
