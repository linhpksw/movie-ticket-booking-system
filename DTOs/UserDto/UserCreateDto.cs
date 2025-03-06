using System.ComponentModel.DataAnnotations;


namespace G5_MovieTicketBookingSystem.DTOs.UserDto

{
    public class UserCreateDto
    {
        public string? Email { get; set; }
        public string? username { get; set; }
        public string? Password { get; set; }
        public string? PasswordConfirm { get; set; }
        public string? fullname { get; set; }

      
    }


}
