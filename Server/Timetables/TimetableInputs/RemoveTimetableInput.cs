using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Timetables.TimetableInputs;

public record RemoveTimetableInput([property: ID<Timetable>] int Id);