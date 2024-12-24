using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Timetables;
[QueryType]
public static class TimetableQueries
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Timetable> GetTimetables(ApplicationDbContext dbContext)
    {
        return dbContext.Timetables.AsNoTracking();
    }

    [NodeResolver]
    public static async Task<Timetable?> GetTimetableByIdAsync(
        int id,
        ITimetableByIdDataLoader timetableById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await timetableById
            .Select(selection)
            .LoadAsync(id, cancellationToken);
    }

    public static async Task<IEnumerable<Timetable>> GetTimetablesByIdAsync(
        [ID<Timetable>] int[] ids,
        ITimetableByIdDataLoader timetableById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await timetableById
            .Select(selection)
            .LoadRequiredAsync(ids, cancellationToken);
    }

}
