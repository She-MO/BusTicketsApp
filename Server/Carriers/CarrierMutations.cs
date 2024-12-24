using BusTicketsApp.Server.Data;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Carriers;

[MutationType]
public static class CarrierMutations
{
    [Error<CarrierNameAlreadyInUseException>]
    public static async Task<Carrier> AddCarrierAsync(
        AddCarrierInput input,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var carrier = await dbContext.Carriers.FirstOrDefaultAsync(c => c.Name == input.Name, cancellationToken);
        if (carrier is not null)
        {
            throw new CarrierNameAlreadyInUseException();
        }
        carrier = new Carrier {  Name = input.Name };
        dbContext.Carriers.Add(carrier);
        await dbContext.SaveChangesAsync(cancellationToken);
        return carrier;
    }
    
    [Error<CarrierNotFoundException>]
    public static async Task<Carrier> RenameCarrierAsync(
        RenameCarrierInput input,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var carrier = await dbContext.Carriers.FirstOrDefaultAsync(c => c.Id == input.Id, cancellationToken);
        if (carrier is null)
        {
            throw new CarrierNotFoundException();
        }

        carrier.Name = input.Name;
        await dbContext.SaveChangesAsync(cancellationToken);
        return carrier;
    }
    [Authorize(Policy = "IsManagerOrAdmin")]
    [Error<OneOfCarrierBusesUsedInTripException>]
    public static async Task<bool> RemoveCarrierAsync(
        RemoveCarrierInput input,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        try
        {
            int result = await dbContext.Carriers.Where(carrier => carrier.Id == input.Id).ExecuteDeleteAsync(cancellationToken);
            return Convert.ToBoolean(result);
        }
        catch (Exception e)
        {
            //Console.WriteLine(e);
            throw new OneOfCarrierBusesUsedInTripException();
        }
    }
}