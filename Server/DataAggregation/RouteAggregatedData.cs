namespace BusTicketsApp.Server.DataAggregation;
using Route = BusTicketsApp.Server.Data.Route;
public class RouteAggregatedDataByMonth
{
    public Route? Route { get; init; } 
    public List<TripAggregatedDataByMonth>? AggregatedData { get; init; } 


}
public class RouteAggregatedDataByYear
{
    public Route? Route { get; init; } 
    public List<TripAggregatedDataByYear>? AggregatedData { get; init; } 


}
