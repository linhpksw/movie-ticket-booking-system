using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G5_MovieTicketBookingSystem;

public class Showtime
{
    [Key]
    public int ShowtimeId { get; set; }

    [Required]
    public int MovieId { get; set; }

    [Required]
    public int ScreenId { get; set; }

    /// <summary>
    /// Store the date portion (e.g., 2025-05-01).
    /// </summary>
    [Required]
    public DateTime ShowDate { get; set; }

    /// <summary>
    /// Store the time portion (e.g., 18:30:00).
    /// </summary>
    [Required]
    public TimeSpan ShowTime { get; set; }

    [Required]
    [MaxLength(20)]
    public required string ExperienceType { get; set; }

    // Navigation
    [ForeignKey(nameof(MovieId))]
    public required Movie Movie { get; set; }

    [ForeignKey(nameof(ScreenId))]
    public required Screen Screen { get; set; }
}
