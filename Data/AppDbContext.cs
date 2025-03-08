using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ------------------------------
        // User_Roles Table (Join Table)
        // ------------------------------
        modelBuilder.Entity<UserRole>()
        .HasKey(ur => new { ur.UserId, ur.RoleId }); // Composite Primary Key

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);


        // ------------------------------
        // Ticket_Scan_Logs Table
        // ------------------------------
        // This will avoid multiple cascading paths error
        // If both Tickets and Users have cascade delete enabled, SQL Server prevents this because:
        // Deleting a User might delete all related TicketScanLogs.
        // Deleting a Ticket might also delete TicketScanLogs.
        // This creates multiple cascading delete paths,
        // which could lead to a cycle or unintended data deletion.

        modelBuilder.Entity<TicketScanLog>()
            .HasOne(t => t.Ticket)
            .WithMany(ti => ti.TicketScanLogs)
            .HasForeignKey(t => t.TicketId)
            .OnDelete(DeleteBehavior.Cascade); // Keep cascade delete for Tickets

        modelBuilder.Entity<TicketScanLog>()
            .HasOne(t => t.User)
            .WithMany(u => u.TicketScanLogs)
            .HasForeignKey(t => t.ScannedBy)
            .OnDelete(DeleteBehavior.Restrict); // Disables cascade delete

        modelBuilder.Entity<Showtime>()
            .HasOne(m => m.Movie)
            .WithMany(s => s.Showtimes)
            .HasForeignKey(m => m.MovieId)
            .OnDelete(DeleteBehavior.Cascade);  // Keep cascade delete for Movies

        modelBuilder.Entity<Showtime>()
            .HasOne(sc => sc.Screen)
            .WithMany(s => s.Showtimes)
            .HasForeignKey(sc => sc.ScreenId)
            .OnDelete(DeleteBehavior.Restrict);  // Disables cascade delete
    }

    // DbSets
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Screen> Screens { get; set; }
    public DbSet<ScreenSeat> ScreenSeats { get; set; }
    public DbSet<SeatType> SeatTypes { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Showtime> Showtimes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<SeatLock> SeatLocks { get; set; }
    public DbSet<TicketScanLog> TicketScanLogs { get; set; }
    public DbSet<TransactionLog> TransactionLogs { get; set; }
}
