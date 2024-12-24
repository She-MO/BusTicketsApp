using BusTicketsApp.Server.Buses;
using BusTicketsApp.Server.Carriers;
using BusTicketsApp.Server.Data;
using BusTicketsApp.Server.Routes;
using BusTicketsApp.Server.Timetables;
using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;

namespace BusTicketsApp.Server.Trips;

[ObjectType<Trip>]
public static partial class TripType
{
    static partial void Configure(IObjectTypeDescriptor<Trip> descriptor)
    {
        descriptor
            .Field(s => s.BusId)
            .ID<Bus>();
        descriptor
            .Field(s => s.TimetableId)
            .ID<Timetable>();
    }
    public static async Task<Timetable?> GetTimetableAsync(
        [Parent(nameof(Trip.TimetableId))] Trip trip,
        ITimetableByIdDataLoader timetableById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await timetableById
            .Select(selection)
            .LoadAsync(trip.TimetableId, cancellationToken);
    }
    public static async Task<Bus?> GetBusAsync(
        [Parent(nameof(Trip.BusId))] Trip trip,
        IBusByIdDataLoader busById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await busById
            .Select(selection)
            .LoadAsync(trip.BusId, cancellationToken);
    }
    public static async Task<IEnumerable<Ticket>> GetTicketsAsync(
        [Parent] Trip trip,
        ITicketsByTripIdDataLoader ticketsByTripId,
        CancellationToken cancellationToken)
    {
        return await ticketsByTripId.LoadRequiredAsync(trip.Id, cancellationToken);
    }
    public static async Task<IEnumerable<TripSeats>> GetTripSeatsAsync(
        [Parent] Trip trip,
        ITripSeatsByTripIdDataLoader tripSeatsByTripId,
        CancellationToken cancellationToken)
    {
        return await tripSeatsByTripId.LoadRequiredAsync(trip.Id, cancellationToken);
    }
    
}