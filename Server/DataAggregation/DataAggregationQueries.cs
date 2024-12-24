using BusTicketsApp.Server.Data;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
using Route = BusTicketsApp.Server.Data.Route;

namespace BusTicketsApp.Server.DataAggregation;

[QueryType]
public static class DataAggregationQueries
{
    
    [Authorize(Policy = "IsAdmin")]
    public static async Task<CarrierAggregatedDataByMonth> TripsDataByMonthsOfYearForOneCarrier(
        int? year,
        [ID<Carrier>] int carrierId,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var data = await dbContext.Carriers.Where(c => c.Id == carrierId).Select(c => new CarrierAggregatedDataByMonth()
        {
            Carrier = c,
            AggregatedData = c.Buses.SelectMany(b => b.Trips).Where(t => t.Date.Year == year).SelectMany(t => t.Tickets)
                .GroupBy(t => t.Trip.Date.Month, t => t, (i, tickets) => new TripAggregatedDataByMonth()
                {
                    Month = i,
                    NumberOfTickets = tickets.Count(),
                    TotalIncome = tickets.Sum(t => t.TotalPrice),
                    AveragePrice = tickets.Average(t => t.TotalPrice),
                    NumberOfTrips = tickets.Select(t => t.TripId).Distinct().Count(),
                    NumberOfPassengers = tickets.Sum(t => t.NumberOfPassengers)
                }).ToList()
        }).FirstAsync(cancellationToken);
        return data;
    }
    [Authorize(Policy = "IsAdmin")]
    public static async Task<CarrierAggregatedDataByYear> TripsDataByYearsForOneCarrier(
        [ID<Carrier>] int carrierId,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var data = await dbContext.Carriers.Where(c => c.Id == carrierId).Select(c => new CarrierAggregatedDataByYear()
        {
            Carrier = c,
            AggregatedData = c.Buses.SelectMany(b => b.Trips).SelectMany(t => t.Tickets)
                .GroupBy(t => t.Trip.Date.Year, t => t, (i, tickets) => new TripAggregatedDataByYear()
                {
                    Year = i,
                    NumberOfTickets = tickets.Count(),
                    TotalIncome = tickets.Sum(t => t.TotalPrice),
                    AveragePrice = tickets.Average(t => t.TotalPrice),
                    NumberOfTrips = tickets.Select(t => t.TripId).Distinct().Count(),
                    NumberOfPassengers = tickets.Sum(t => t.NumberOfPassengers)
                }).ToList()
        }).FirstAsync(cancellationToken);
        return data;
    }
    [Authorize(Policy = "IsAdmin")]
    public static async Task<List<TripAggregatedDataByMonth>> TripsDataByMonthsOfYear(
        int? year,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var data = dbContext.Trips.Where(t => t.Date.Year == year).SelectMany(c =>  c.Tickets)
                .GroupBy(t => t.Trip.Date.Month, t => t, (i, tickets) => new TripAggregatedDataByMonth()
                {
                    Month = i,
                    NumberOfTickets = tickets.Count(),
                    TotalIncome = tickets.Sum(t => t.TotalPrice),
                    AveragePrice = tickets.Average(t => t.TotalPrice),
                    NumberOfTrips = tickets.Select(t => t.TripId).Distinct().Count(),
                    NumberOfPassengers = tickets.Sum(t => t.NumberOfPassengers)
                }).ToList();
        return data;
    }
    [Authorize(Policy = "IsAdmin")]
    public static async Task<List<TripAggregatedDataByYear>> TripsDataByYears(
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var data = dbContext.Trips.SelectMany(c =>  c.Tickets)
                .GroupBy(t => t.Trip.Date.Year, t => t, (i, tickets) => new TripAggregatedDataByYear()
                {
                    Year = i,
                    NumberOfTickets = tickets.Count(),
                    TotalIncome = tickets.Sum(t => t.TotalPrice),
                    AveragePrice = tickets.Average(t => t.TotalPrice),
                    NumberOfTrips = tickets.Select(t => t.TripId).Distinct().Count(),
                    NumberOfPassengers = tickets.Sum(t => t.NumberOfPassengers)
                }).ToList();
        return data;
    }
    [Authorize(Policy = "IsAdmin")]
    public static async Task<RouteAggregatedDataByMonth> TripsDataByMonthsOfYearForOneRoute(
        int? year,
        [ID<Route>] int routeId,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var data = await dbContext.Routes.Where(c => c.Id == routeId).Select(c => new RouteAggregatedDataByMonth()
        {
            Route = c,
            AggregatedData = c.Timetables.SelectMany(b => b.Trips).Where(t => t.Date.Year == year).SelectMany(t => t.Tickets)
                .GroupBy(t => t.Trip.Date.Month, t => t, (i, tickets) => new TripAggregatedDataByMonth()
                {
                    Month = i,
                    NumberOfTickets = tickets.Count(),
                    TotalIncome = tickets.Sum(t => t.TotalPrice),
                    AveragePrice = tickets.Average(t => t.TotalPrice),
                    NumberOfTrips = tickets.Select(t => t.TripId).Distinct().Count(),
                    NumberOfPassengers = tickets.Sum(t => t.NumberOfPassengers)
                }).ToList()
        }).FirstAsync(cancellationToken);
        return data;
    }
    [Authorize(Policy = "IsAdmin")]
    public static async Task<RouteAggregatedDataByYear> TripsDataByYearsForOneRoute(
        [ID<Route>] int routeId,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var data = await dbContext.Routes.Where(c => c.Id == routeId).Select(c => new RouteAggregatedDataByYear()
        {
            Route = c,
            AggregatedData = c.Timetables.SelectMany(b => b.Trips).SelectMany(t => t.Tickets)
                .GroupBy(t => t.Trip.Date.Year, t => t, (i, tickets) => new TripAggregatedDataByYear()
                {
                    Year = i,
                    NumberOfTickets = tickets.Count(),
                    TotalIncome = tickets.Sum(t => t.TotalPrice),
                    AveragePrice = tickets.Average(t => t.TotalPrice),
                    NumberOfTrips = tickets.Select(t => t.TripId).Distinct().Count(),
                    NumberOfPassengers = tickets.Sum(t => t.NumberOfPassengers)
                }).ToList()
        }).FirstAsync(cancellationToken);
        return data;
    }
    [Authorize(Policy = "IsAdmin")]
    public static async Task<TimetableAggregatedDataByMonth> TripsDataByMonthsOfYearForOneTimetable(
        int? year,
        [ID<Timetable>] int timetableId,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var data = await dbContext.Timetables.Where(c => c.Id == timetableId).Select(c => new TimetableAggregatedDataByMonth()
        {
            Timetable = c,
            AggregatedData = c.Trips.Where(t => t.Date.Year == year).SelectMany(t => t.Tickets)
                .GroupBy(t => t.Trip.Date.Month, t => t, (i, tickets) => new TripAggregatedDataByMonth()
                {
                    Month = i,
                    NumberOfTickets = tickets.Count(),
                    TotalIncome = tickets.Sum(t => t.TotalPrice),
                    AveragePrice = tickets.Average(t => t.TotalPrice),
                    NumberOfTrips = tickets.Select(t => t.TripId).Distinct().Count(),
                    NumberOfPassengers = tickets.Sum(t => t.NumberOfPassengers)
                }).ToList()
        }).FirstAsync(cancellationToken);
        return data;
    }
    [Authorize(Policy = "IsAdmin")]
    public static async Task<TimetableAggregatedDataByYear> TripsDataByYearsForOneTimetable(
        [ID<Timetable>] int timetableId,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var data = await dbContext.Timetables.Where(c => c.Id == timetableId).Select(c => new TimetableAggregatedDataByYear()
        {
            Timetable = c,
            AggregatedData = c.Trips.SelectMany(t => t.Tickets)
                .GroupBy(t => t.Trip.Date.Year, t => t, (i, tickets) => new TripAggregatedDataByYear()
                {
                    Year = i,
                    NumberOfTickets = tickets.Count(),
                    TotalIncome = tickets.Sum(t => t.TotalPrice),
                    AveragePrice = tickets.Average(t => t.TotalPrice),
                    NumberOfTrips = tickets.Select(t => t.TripId).Distinct().Count(),
                    NumberOfPassengers = tickets.Sum(t => t.NumberOfPassengers)
                }).ToList()
        }).FirstAsync(cancellationToken);
        return data;
    }
}