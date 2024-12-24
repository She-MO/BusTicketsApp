﻿namespace BusTicketsApp.Server.Data;

public sealed class Trip
{
    public int Id { get; init; }
    public required int BusId { get; set; }
    public Bus? Bus { get; init; }
    public required int TimetableId { get; init; }
    public Timetable? Timetable { get; init; }
    public required DateOnly Date { get; init; }
    public required decimal PricePerKm { get; set; }
    public ICollection<Ticket> Tickets { get; init; } = new List<Ticket>();
    public ICollection<TripSeats> TripSeats { get; init; } = new List<TripSeats>();
}