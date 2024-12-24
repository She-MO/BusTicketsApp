using System.ComponentModel.DataAnnotations;
using BusTicketsApp.Server.Data;

namespace BusTicketsApp.Server.Routes;

public record RouteStopInput(
    [property: ID<City>]int CityId, 
    TimeSpan TimeFromPrevStop, 
    int KmFromPrevStop 
    );