using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface IUserServices
    {
        Task<UserResponseDto> Login(UserLoginRequestDto request);

        Task<UserResponseDto> Register(UserRegisterRequestDto request);

        Task<User?> GetUserByIdAsync(int? userId);

        Task<List<UserRole>> GetUserRolesAsync();
    }
}
