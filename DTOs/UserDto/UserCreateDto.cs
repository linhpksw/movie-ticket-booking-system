using System.ComponentModel.DataAnnotations;

namespace G5_MovieTicketBookingSystem.DTOs.AuthorizationDto
{
    public class UserCreateDto
    {
        [Required]
        [MaxLength(100)]
        public required string Email { get; set; }
        [Required]
        [MaxLength(255)]
        public required string Password { get; set; }
            [Required]
        [MaxLength(255)]
       public required string PasswordConfirm { get; set; }

    }   
}
