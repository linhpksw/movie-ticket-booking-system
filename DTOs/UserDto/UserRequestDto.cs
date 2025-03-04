using System.ComponentModel.DataAnnotations;

namespace G5_MovieTicketBookingSystem.DTOs.UserDto
{
    public class UserRequestDto
    {
        [Required]
        [MaxLength(100)]
        public required string Email { get; set; }
        [Required]
        [MaxLength(255)]
        public required string Password { get; set; }     

    }
}
