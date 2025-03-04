using G5_MovieTicketBookingSystem.DTOs.AuthorizationDto;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface IUserServices
    {
        Task<UserResponseDto> RegisterAsync(UserCreateDto UserCreateDto);
        Task<UserResponseDto> LoginAsync(UserRequestDto userRequestDto);

    }
}
