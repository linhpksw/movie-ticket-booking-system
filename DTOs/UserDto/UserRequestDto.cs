using System.ComponentModel.DataAnnotations;

namespace G5_MovieTicketBookingSystem.DTOs.UserDto
{
    public class UserRequestDto
    {
        
        public  string? Email { get; set; }
       
        public  string? Password { get; set; }     

    }
}
