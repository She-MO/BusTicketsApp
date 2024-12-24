using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using HotChocolate.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Timetables;

public static class TimetableDataLoaders
{
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Timetable>> TimetableByIdAsync(
        IReadOnlyList<int> ids,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        CancellationToken cancellationToken)
    {
        return await dbContext.Timetables
            .AsNoTracking()
            .Where(b => ids.Contains(b.Id))
            .Select(b => b.Id, selector)
            .ToDictionaryAsync(b => b.Id, cancellationToken);
    }
    [DataLoader]
    public static async Task<IReadOnlyDictionary<int, Page<Trip>>> TripsByTimetableIdAsync(
        IReadOnlyList<int> timetableIds,
        ApplicationDbContext dbContext,
        ISelectorBuilder selector,
        PagingArguments pagingArguments,
        CancellationToken cancellationToken)
    {
        return await dbContext.Trips
            .AsNoTracking()
            .Where(b => timetableIds.Contains(b.TimetableId))
            .OrderBy(b => b.Id)
            .Select(b => b.Id, selector)
            .ToBatchPageAsync(b => b.TimetableId, pagingArguments, cancellationToken);
    }
    
}