using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G5_MovieTicketBookingSystem.Models
{
    public class Showtime
    {
        [Key]
        public int ShowtimeId { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        public int ScreenId { get; set; }

        
        [Required]
        public DateOnly ShowDate { get; set; }

   
        [Required]
        public TimeOnly ShowTime { get; set; }

        [Required]
        [MaxLength(20)]
        public required string ExperienceType { get; set; }

        [Required]
        public required bool IsSoldOut { get; set; }

        [ForeignKey(nameof(MovieId))]
        public required Movie Movie { get; set; }

        [ForeignKey(nameof(ScreenId))]
        public required Screen Screen { get; set; }

        public ICollection<SeatLock>? SeatLocks { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}