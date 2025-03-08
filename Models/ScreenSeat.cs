using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G5_MovieTicketBookingSystem;

public class ScreenSeat
{
    [Key]
    public int ScreenSeatId { get; set; }

    [Required]
    public required int ScreenId { get; set; }

    [ForeignKey(nameof(ScreenId))]
    public required Screen Screen { get; set; }

    [Required]
    [MaxLength(10)]
    public required string SeatLabel { get; set; }

    [Required]
    public required int SeatTypeId { get; set; }

    [ForeignKey(nameof(SeatTypeId))]
    public required SeatType SeatType { get; set; }

    public ICollection<OrderItem>? OrderItems { get; set; }

    public ICollection<SeatLock>? SeatLocks { get; set; }
}
