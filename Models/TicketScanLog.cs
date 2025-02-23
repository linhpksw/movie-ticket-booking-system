using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G5_MovieTicketBookingSystem;

public class TicketScanLog
{
    [Key]
    public int ScanId { get; set; }

    [Required]
    public int TicketId { get; set; }

    [Required]
    public DateTime ScanTimestamp { get; set; }

    [Required]
    public int ScannedBy { get; set; }

    [Required]
    [MaxLength(20)]
    public required string ScanResult { get; set; } // e.g., "Valid", "Invalid", "Duplicate"

    // Navigation
    [ForeignKey(nameof(TicketId))]
    public required Ticket Ticket { get; set; }

    [ForeignKey(nameof(ScannedBy))]
    public required User User { get; set; }
}
