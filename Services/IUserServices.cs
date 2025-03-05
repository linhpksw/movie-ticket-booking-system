using G5_MovieTicketBookingSystem.DTOs.UserDto;
using G5_MovieTicketBookingSystem.DTOs.UserDto;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface IUserServices
    {
        public Task<UserResponseDto> Login(UserRequestDto UserRequestDto);
        public Task<UserResponseDto> Register(UserCreateDto UserCreateDto, List<int> Role);
    }
}
