using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Carrier> Carriers { get; init; }
    public DbSet<Bus> Buses { get; init; }
    public DbSet<User> Users { get; init; }
    public DbSet<Passenger> Passengers { get; init; }
    public DbSet<City> Cities { get; init; }
    public DbSet<Route> Routes { get; init; }
    public DbSet<RouteStop> RouteStops { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //user entity
        modelBuilder
            .Entity<User>()
            .Property(u => u.Role)
            .HasConversion(
                r => r.ToString(),
                r => (Roles)Enum.Parse(typeof(Roles), r));
        
        //passenger entity
        //modelBuilder
        //    .Entity<Passenger>()
        //    .HasIndex(p => new { p.FirstName, p.LastName, p.DateOfBirth, p.UserId })
        //    .IsUnique();
        
        
        //carrier entity
        modelBuilder
            .Entity<Carrier>()
            .HasIndex(c => c.Name)
            .IsUnique();
        
        
        //bus entity
        modelBuilder
            .Entity<Bus>()
            .HasIndex(b => b.BusNumber)
            .IsUnique();
        
        
        //city entity
        modelBuilder
            .Entity<City>()
            .HasIndex(c => c.Name)
            .IsUnique();
    }

}