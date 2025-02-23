using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G5_MovieTicketBookingSystem;

public class SeatLock
{
    [Key]
    public int SeatLockId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int ScreenSeatId { get; set; }

    [Required]
    public DateTime LockStartTime { get; set; }

    [Required]
    public DateTime LockExpiryTime { get; set; }

    [Required]
    [MaxLength(20)]
    public required string LockStatus { get; set; } // e.g., "Active", "Expired", "Released"

    // Navigation
    [ForeignKey(nameof(UserId))]
    public required User User { get; set; }

    [ForeignKey(nameof(ScreenSeatId))]
    public required ScreenSeat ScreenSeat { get; set; }
}
