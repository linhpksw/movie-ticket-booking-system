using System.ComponentModel.DataAnnotations;

namespace G5_MovieTicketBookingSystem;

public class Cinema
{
    [Key]
    public int CinemaId { get; set; }

    [Required]
    [MaxLength(255)]
    public required string CinemaName { get; set; }

    [Required]
    [MaxLength(100)]
    public required string City { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Address { get; set; }

    // Navigation
    public ICollection<Screen>? Screens { get; set; }
}
