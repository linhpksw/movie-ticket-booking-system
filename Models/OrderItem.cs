using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G5_MovieTicketBookingSystem;

public class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }

    [Required]
    public int OrderId { get; set; }

    [Required]
    public int ScreenSeatId { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(0, double.MaxValue)]
    public decimal PriceCharged { get; set; }

    // Navigation
    [ForeignKey(nameof(OrderId))]
    public required Order Order { get; set; }

    [ForeignKey(nameof(ScreenSeatId))]
    public required ScreenSeat ScreenSeat { get; set; }

    public Ticket? Ticket { get; set; }
}
