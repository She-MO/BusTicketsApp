namespace BusTicketsApp.Server.DataAggregation;

public class TripAggregatedDataByMonth
{
    public int Month { get; init; } 
    public int NumberOfTickets { get; init; }
    public int NumberOfTrips { get; init; }
    public int NumberOfPassengers { get; init; }
    public decimal TotalIncome { get; init; } 
    public decimal AveragePrice { get; init; } 

}
public class TripAggregatedDataByYear
{
    public int Year { get; init; } 
    public int NumberOfTickets { get; init; }
    public int NumberOfTrips { get; init; }
    public int NumberOfPassengers { get; init; }
    public decimal TotalIncome { get; init; } 
    public decimal AveragePrice { get; init; } 
    
}
