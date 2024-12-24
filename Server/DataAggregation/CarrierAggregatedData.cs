using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.DataAggregation;

public class CarrierAggregatedDataByMonth
{
    public Carrier? Carrier { get; init; } 
    public List<TripAggregatedDataByMonth>? AggregatedData { get; init; } 


}
public class CarrierAggregatedDataByYear
{
    public Carrier? Carrier { get; init; } 
    public List<TripAggregatedDataByYear>? AggregatedData { get; init; } 


}
