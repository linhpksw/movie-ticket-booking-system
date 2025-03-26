namespace G5_MovieTicketBookingSystem.DTOs
{
    public class UserRegisterRequestDto
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        public string Fullname { get; set; }
    }
}
