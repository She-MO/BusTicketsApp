using BusTicketsApp.Server.Data;
using BusTicketsApp.Server.Routes;
using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;
using Route = BusTicketsApp.Server.Data.Route;

namespace BusTicketsApp.Server.Timetables;
[ObjectType<Timetable>]
public static partial class TimetableType
{
    static partial void Configure(IObjectTypeDescriptor<Timetable> descriptor)
    {
        descriptor
            .Field(s => s.RouteId)
            .ID<Route>();
    }
    public static async Task<Route?> GetRouteAsync(
        [Parent(nameof(Timetable.RouteId))] Timetable timetable,
        IRouteByIdDataLoader routeById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await routeById
            .Select(selection)
            .LoadAsync(timetable.RouteId, cancellationToken);
    }
}
