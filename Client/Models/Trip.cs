namespace Client.Models;

public class Trip
{
    public string Id { get; init; }
    public Bus? Bus { get; init; }
    public City? DepartureCity { get; init; }
    public City? ArrivalCity { get; init; }
    public DateTime DepartureDateTime { get; init; }
    public DateTime ArrivalDateTime { get; init; }
    public decimal Price{ get; set; }
    public decimal PricePerKm { get; set; }
    public int NumberOfAvailableSeats { get; init; }
    public Timetable? Timetable { get; set; }
}