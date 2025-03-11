using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G5_MovieTicketBookingSystem.Models
{
    public class UserRole
    {
        [Key]
        public int UserId { get; set; }

        [Key]
        public int RoleId { get; set; }

        // Navigation
        [ForeignKey(nameof(UserId))]
        public required User User { get; set; }

        [ForeignKey(nameof(RoleId))]
        public required Role Role { get; set; }
    }
}