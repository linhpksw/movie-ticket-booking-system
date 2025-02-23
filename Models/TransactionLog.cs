using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G5_MovieTicketBookingSystem;

public class TransactionLog
{
    [Key]
    public int PaymentId { get; set; }

    [Required]
    public int OrderId { get; set; }

    [Required]
    [MaxLength(50)]
    public required string PaymentGateway { get; set; } // e.g. "VNPAY", "PayPal"

    [Required]
    public DateTime TransactionTimestamp { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(0, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    [MaxLength(20)]
    public required string PaymentStatus { get; set; } // e.g., "Success", "Failed", "Pending"

    [MaxLength(1000)]
    public required string GatewayResponse { get; set; } // e.g. full JSON or textual response

    // Navigation
    [ForeignKey(nameof(OrderId))]
    public required Order Order { get; set; }
}
