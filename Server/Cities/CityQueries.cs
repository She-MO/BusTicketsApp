using BusTicketsApp.Server.Data;
using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Cities;

[QueryType]
public static class CityQueries
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<City> GetCities(ApplicationDbContext dbContext)
    {
        return dbContext.Cities.AsNoTracking().OrderBy(c => c.Name).ThenBy(c => c.Id);
    }

    public static async Task<City?> GetCityByIdAsync(
        int id,
        ICityByIdDataLoader cityById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await cityById
            .Select(selection)
            .LoadAsync(id, cancellationToken);
    }

    public static async Task<IEnumerable<City>> GetCitiesByIdAsync(
        [ID<City>] int[] ids,
        ICityByIdDataLoader cityById,
        ISelection selection,
        CancellationToken cancellationToken)
    {
        return await cityById
            .Select(selection)
            .LoadRequiredAsync(ids, cancellationToken);
    }
        
}