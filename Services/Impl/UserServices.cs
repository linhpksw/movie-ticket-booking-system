using G5_MovieTicketBookingSystem.DTOs.UserDto;
using G5_MovieTicketBookingSystem.Repositories;
using G5_MovieTicketBookingSystem.Repositories.Impl;
using G5_MovieTicketBookingSystem.Util;
using Microsoft.Build.Framework;
using System.Security.Cryptography;
using System.Text;

namespace G5_MovieTicketBookingSystem.Services.Impl
{
    public class UserServices : IUserServices
    {

        private readonly IUserRepository   userRepository;
        private readonly ILogger<UserServices> _logger;

        public UserServices(IUserRepository userRepository, ILogger<UserServices> logger)
        {
            userRepository = userRepository;
            _logger = logger;
        }

        public Task<UserResponseDto> LoginAsync(UserRequestDto userRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponseDto> RegisterAsync(UseCreateDto UseCreateDto)
        {
            throw new NotImplementedException();
        }
    }
}
