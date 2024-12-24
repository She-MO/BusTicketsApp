using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using HotChocolate.Pagination;
using Microsoft.EntityFrameworkCore;
using Route = BusTicketsApp.Server.Data.Route;

namespace BusTicketsApp.Server.Routes;

public static class RouteDataLoaders
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Route>> RouteByIdAsync(
        IReadOnlyList<int> ids,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        CancellationToken cancellationToken)
    {
        return await dbContext.Routes
            .AsNoTracking()
            .Where(c => ids.Contains(c.Id))
            .Select(c => c.Id, selector)
            .ToDictionaryAsync(c => c.Id, cancellationToken);
    }

    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, RouteStop[]>> RouteStopsByRouteIdAsync(
        IReadOnlyList<int> routeIds,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        CancellationToken cancellationToken)
    {
        return await dbContext.Routes
            .AsNoTracking()
            .Where(b => routeIds.Contains(b.Id))
            .Select(b => b.Id, b => b.RouteStops, selector)
            .ToDictionaryAsync(r => r.Key, r => r.Value.ToArray(), cancellationToken);
    }
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Timetable[]>> TimetablesByRouteIdAsync(
        IReadOnlyList<int> routeIds,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        CancellationToken cancellationToken)
    {
        return await dbContext.Routes
            .AsNoTracking()
            .Where(b => routeIds.Contains(b.Id))
            .Select(b => b.Id, b => b.Timetables, selector)
            .ToDictionaryAsync(r => r.Key, r => r.Value.ToArray(), cancellationToken);
    }
}
