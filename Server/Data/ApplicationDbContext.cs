using Microsoft.EntityFrameworkCore;

namespace BusTicketsApp.Server.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Carrier> Carriers { get; init; }
    public DbSet<Bus> Buses { get; init; }
    public DbSet<User> Users { get; init; }
    public DbSet<City> Cities { get; init; }
    public DbSet<Route> Routes { get; init; }
    public DbSet<RouteStop> RouteStops { get; init; }
    public DbSet<Timetable> Timetables { get; init; }
    public DbSet<Trip> Trips { get; init; }
    public DbSet<TripSeats> TripSeats { get; init; }
    public DbSet<Ticket> Tickets { get; init; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //user entity
        modelBuilder
            .Entity<User>()
            .Property(u => u.Role)
            .HasConversion(
                r => r.ToString(),
                r => (Roles)Enum.Parse(typeof(Roles), r));
        modelBuilder
            .Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        
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
        
        
        //RouteStop entity
        modelBuilder
            .Entity<RouteStop>()
            .HasKey(rs => new { rs.RouteId, rs.Sequence });
        modelBuilder
            .Entity<RouteStop>()
            .HasMany(rs => rs.TripSeats)
            .WithOne(ts => ts.RouteStop)
            .HasForeignKey(rs => new { rs.RouteId, rs.Sequence });
        
        //TripSeats entity
        modelBuilder
            .Entity<TripSeats>()
            .HasKey(ts => new { ts.TripId, ts.RouteId, ts.Sequence });

        //Timetable entity
        modelBuilder
            .Entity<Timetable>()
            .Property(t => t.DayOfWeek)
            .HasConversion(
                day => day.ToString(),
                stringDay => (DayOfWeek)Enum.Parse<DayOfWeek>(stringDay));
    }

}