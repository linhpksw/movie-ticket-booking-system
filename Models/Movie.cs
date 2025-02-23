using System.ComponentModel.DataAnnotations;

namespace G5_MovieTicketBookingSystem;

public class Movie
{
    [Key]
    public int MovieId { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Title { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Genre { get; set; }

    [Required]
    public int Duration { get; set; } // in minutes

    [Required]
    [MaxLength(50)]
    public required string Language { get; set; }

    [Required]
    public float Rating { get; set; }

    [Required]
    public DateTime ReleaseDate { get; set; }

    [Required]
    [MaxLength(1000)]
    public required string Description { get; set; }

    // Navigation
    public ICollection<Showtime>? Showtimes { get; set; }
}
