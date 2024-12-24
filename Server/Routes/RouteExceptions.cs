namespace BusTicketsApp.Server.Routes;

public sealed class RouteIsUsedInTimetableException() : Exception("This route is used in at least 1 timetable. Delete the timetable to delete the route.");
public sealed class RouteNameIsAlreadyInUseException() : Exception("Route with this name already exists");