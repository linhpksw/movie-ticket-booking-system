using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface IUserServices
    {
        Task<UserDto> Login(UserDto UserDto);

        Task<UserDto> Register(UserDto UserDto);

        Task<User?> GetUserByIdAsync(int? userId);

        Task<List<UserRole>> GetUserRolesAsync();
    }
}
