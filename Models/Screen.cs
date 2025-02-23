using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G5_MovieTicketBookingSystem;

public class Screen
{
    [Key]
    public int ScreenId { get; set; }

    [Required]
    public int CinemaId { get; set; }

    [Required]
    [MaxLength(100)]
    public required string ScreenName { get; set; }

    // Navigation
    [ForeignKey(nameof(CinemaId))]
    public required Cinema Cinema { get; set; }

    public ICollection<ScreenSeat>? ScreenSeats { get; set; }
    public ICollection<Showtime>? Showtimes { get; set; }
}
