using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G5_MovieTicketBookingSystem.Models
{
    public class SeatLock
    {
        [Key]
        public int SeatLockId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ScreenSeatId { get; set; }

        [Required]
        public DateTime LockStartTime { get; set; }

        [Required]
        public int ShowtimeId { get; set; }

        [Required]
        public DateTime LockExpiryTime { get; set; }

        // Navigation
        [ForeignKey(nameof(UserId))]
        public required User User { get; set; }

        [ForeignKey(nameof(ScreenSeatId))]
        public required ScreenSeat ScreenSeat { get; set; }

        [ForeignKey(nameof(ShowtimeId))]
        public required Showtime Showtime { get; set; }
    }
}