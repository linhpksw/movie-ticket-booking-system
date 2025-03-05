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

        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        private readonly ILogger<UserServices> _logger;

        public UserServices(IUserRepository userRepository, ILogger<UserServices> logger, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _userRoleRepository = userRoleRepository;
        }

        public Task<UserResponseDto> Login(UserRequestDto UserRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponseDto> Register(UserCreateDto userCreateDto, List<int> roleIds)
        {
            var existingUser = await _userRepository.GetUserByEmail(userCreateDto.Email);
            if (existingUser != null)
            {
                throw new Exception("Email đã tồn tại");
            }

            User user = UserMapper.CreateToUser(userCreateDto);
            User userInsert;
            try
            {
                userInsert = await _userRepository.SignUpAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Đăng ký thất bại: " + ex.Message);
            }

            foreach (var roleId in roleIds)
            {
                await _userRoleRepository.AssignRoleToUserAsync(user.UserId, roleId);
            }

            return UserMapper.MapToUserResponseDto(userInsert);
        }
    }
}