using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G5_MovieTicketBookingSystem;

public class SeatType
{
    [Key]
    public int SeatTypeId { get; set; }

    [Required]
    [MaxLength(50)]
    public required string SeatTypeName { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(0, double.MaxValue)]
    public decimal BasePrice { get; set; }

    // Navigation
    public ICollection<ScreenSeat>? ScreenSeats { get; set; }
}
