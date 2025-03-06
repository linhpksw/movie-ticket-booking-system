using G5_MovieTicketBookingSystem.DTOs.UserDto;
using G5_MovieTicketBookingSystem.Repositories;
using G5_MovieTicketBookingSystem.Repositories.Impl;
using G5_MovieTicketBookingSystem.Util;
using Microsoft.Build.Framework;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
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
            Console.WriteLine("service");

            var existingUser = await _userRepository.GetUserByEmail(userCreateDto.Email);
            if (existingUser != null)
            {
                return null;
            }   

            if (!string.IsNullOrEmpty(userCreateDto.Password))// Assuming User has a Password property
            {
                userCreateDto.Password = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);

            }
            string uniqueUsername = await GenerateUniqueUsernameAsync(userCreateDto.Email);
            userCreateDto.username = uniqueUsername;
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

        public async Task<string> GenerateUniqueUsernameAsync(string email)
        {
            string baseUsername = UserMapper.GenerateBaseUsername(email);
            string username = baseUsername;
            int attempt = 1;

            while (await _userRepository.IsUsernameExistsAsync(username))
            {
                // Nếu trùng, thêm số ngẫu nhiên 4 chữ số phía sau
                username = $"{baseUsername}{new Random().Next(1000, 9999)}";

                // Giới hạn vòng lặp để tránh lỗi vô hạn
                attempt++;
                if (attempt > 10) throw new Exception("Failed to generate a unique username.");
            }

            return username;
        }
    }
}