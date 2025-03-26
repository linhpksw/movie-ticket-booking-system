using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Mappers
{
    public static class UserMapper
    {
        public static UserDto toDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Fullname = user.Fullname,
                Password = user.Password,
                Username = user.Username
            };
        }

        public static User toEntity(UserDto userDto)
        {
            return new User
            {
                UserId = userDto.UserId,
                Email = userDto.Email,
                Fullname = userDto.Fullname,
                Password = userDto.Password,
                Username = userDto.Username
            };
        }
    }
}
