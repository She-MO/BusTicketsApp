using BusTicketsApp.Server.Data;
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
}