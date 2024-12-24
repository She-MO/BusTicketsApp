using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.DataAggregation;
public class TimetableAggregatedDataByMonth
{
    public Timetable? Timetable { get; init; } 
    public List<TripAggregatedDataByMonth>? AggregatedData { get; init; } 


}
public class TimetableAggregatedDataByYear
{
    public Timetable? Timetable { get; init; } 
    public List<TripAggregatedDataByYear>? AggregatedData { get; init; } 


}
