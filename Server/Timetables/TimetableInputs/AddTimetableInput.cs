namespace BusTicketsApp.Server.Timetables.TimetableInputs;

public record AddTimetableInput([property:ID<Route>] int RouteId, TimeOnly TimeOfDeparture, DayOfWeek DayOfWeek);