using System.ComponentModel.DataAnnotations;

namespace G5_MovieTicketBookingSystem;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Username { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Email { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Password { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Fullname { get; set; }

    // Navigation
    public ICollection<UserRole>? UserRoles { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public ICollection<SeatLock>? SeatLocks { get; set; }
    public ICollection<TicketScanLog>? TicketScanLogs { get; set; }
}
