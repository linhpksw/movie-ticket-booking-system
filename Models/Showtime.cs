using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G5_MovieTicketBookingSystem.Models;

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
    public DateOnly ShowDate { get; set; }

    /// <summary>
    /// Store the time portion (e.g., 18:30:00).
    /// </summary>
    [Required]
    public TimeOnly ShowTime { get; set; }

    [Required]
    [MaxLength(20)]
    public required string ExperienceType { get; set; }

    [ForeignKey(nameof(MovieId))]
    public required Movie Movie { get; set; }

    [ForeignKey(nameof(ScreenId))]
    public required Screen Screen { get; set; }
}
