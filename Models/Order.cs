using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G5_MovieTicketBookingSystem;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public DateTime OrderTimestamp { get; set; }

    [Required]
    [MaxLength(20)]
    public required string OrderStatus { get; set; } // e.g., "Pending", "Paid", "Cancelled"

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(0, double.MaxValue)]
    public decimal TotalAmount { get; set; }

    // Navigation
    [ForeignKey(nameof(UserId))]
    public required User User { get; set; }

    public ICollection<OrderItem>? OrderItems { get; set; }
    public ICollection<TransactionLog>? TransactionLogs { get; set; }
}
