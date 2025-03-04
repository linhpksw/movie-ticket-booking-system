using G5_MovieTicketBookingSystem.DTOs.UserDto;
using G5_MovieTicketBookingSystem.DTOs.UserDto;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface IUserServices
    {
        Task<UserResponseDto> RegisterAsync(UseCreateDto UseCreateDto);
        Task<UserResponseDto>  LoginAsync(UserRequestDto userRequestDto);

    }
}
