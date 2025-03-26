using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Mappers
{
    public static class UserMapper
    {
        public static UserResponseDto toResponseDto(User user)
        {
            return new UserResponseDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Fullname = user.Fullname,
                Password = user.Password,
                Username = user.Username
            };
        }

        public static User toEntity(UserRegisterRequestDto request)
        {
            return new User
            {
                Email = request.Email,
                Fullname = request.Fullname,
                Password = request.Password,
                Username = request.Username
            };
        }
    }
}
