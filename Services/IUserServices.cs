using G5_MovieTicketBookingSystem.DTOs.UserDto;
using G5_MovieTicketBookingSystem.DTOs.UserDto;
using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface IUserServices
    {

        public Task<UserResponseDto> Login(UserRequestDto UserRequestDto);
        public Task<UserResponseDto> Register(UserCreateDto UserCreateDto, List<int> Role);
        public Task<UserResponseDto> RegisterGoogle(UserCreateDto UserCreateDto, List<int> Role);
<<<<<<< HEAD
        Task<User?> GetUserByIdAsync(int? userId);
=======

>>>>>>> df1b590d3df0ffd82f7cada888caf4284a5e6349
    }
}
