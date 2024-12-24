namespace BusTicketsApp.Server.Timetables;

public sealed class TimetableUsedInTripException() : Exception("This timetable is used in at least 1 trip. Delete the trip to delete timetable.");
public sealed class TimetableWithThisSettingsAlreadyExists() : Exception("Timetable with this settings already exists");