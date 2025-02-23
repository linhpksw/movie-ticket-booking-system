using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G5_MovieTicketBookingSystem;

public class Ticket
{
    [Key]
    public int TicketId { get; set; }

    [Required]
    public int OrderItemId { get; set; }

    [Required]
    [MaxLength(255)]
    public required string UniqueCode { get; set; } // e.g. "ABC123XYZ"

    [Required]
    public DateTime IssueTimestamp { get; set; }

    [Required]
    [MaxLength(20)]
    public required string TicketStatus { get; set; } // e.g., "Active", "Cancelled", "Used"

    public DateTime? ScannedTimestamp { get; set; }

    // Navigation
    [ForeignKey(nameof(OrderItemId))]
    public required OrderItem OrderItem { get; set; }

    public ICollection<TicketScanLog>? TicketScanLogs { get; set; }
}
