using BusTicketsApp.Server.Data;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Cities;

[MutationType]
public static class CityMutations
{
    [Error<CityNameAlreadyInUseException>]
    public static async Task<City> AddCityAsync(
        AddCityInput input,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var city = await dbContext.Cities.FirstOrDefaultAsync(c => c.Name == input.Name, cancellationToken);
        if (city is not null)
        {
            throw new CityNameAlreadyInUseException();
        }
        city = new City {  Name = input.Name };
        dbContext.Cities.Add(city);
        await dbContext.SaveChangesAsync(cancellationToken);
        return city;
    }
    
    [Error<CityNotFoundException>]
    public static async Task<City> RenameCityAsync(
        RenameCityInput input,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var city = await dbContext.Cities.FirstOrDefaultAsync(c => c.Id == input.Id, cancellationToken);
        if (city is null)
        {
            throw new CityNotFoundException();
        }

        city.Name = input.Name;
        await dbContext.SaveChangesAsync(cancellationToken);
        return city;
    }
    [Authorize(Policy = "IsManagerOrAdmin")]
    [Error<CityUsedInRouteException>]
    public static async Task<bool> RemoveCityAsync(
        RemoveCityInput input,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        try
        {
            int result = await dbContext.Cities.Where(city => city.Id == input.Id).ExecuteDeleteAsync(cancellationToken);
            return Convert.ToBoolean(result);
        }
        catch (Exception e)
        {
            //Console.WriteLine(e);
            throw new CityUsedInRouteException();
        }
    }
}