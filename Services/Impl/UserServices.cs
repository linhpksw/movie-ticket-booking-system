using G5_MovieTicketBookingSystem.Commons;
using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Mappers;
using G5_MovieTicketBookingSystem.Models;
using G5_MovieTicketBookingSystem.Repositories;
using System.Text.RegularExpressions;

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

        public async Task<UserDto> Login(UserDto userRequestDto)
        {
            try
            {
                if (userRequestDto == null)
                {
                    _logger.LogWarning("Login attempt with null user request.");
                    return null;
                }

                var existingUser = await _userRepository.GetUserByEmail(userRequestDto.Email);

                if (existingUser == null)
                {
                    _logger.LogWarning("Login failed: User with email {Email} not found.", userRequestDto.Email);
                    return null;
                }

                // Kiểm tra mật khẩu bằng BCrypt.Verify
                if (string.IsNullOrEmpty(userRequestDto.Password) || !BCrypt.Net.BCrypt.Verify(userRequestDto.Password, existingUser.Password))
                {
                    _logger.LogWarning("Login failed: Invalid password for user {Email}.", userRequestDto.Email);
                    return null;
                }

                var userResponse = UserMapper.toDto(existingUser);


                _logger.LogInformation("User {Email} logged in successfully with UserId {UserId}.", userResponse.Email, userResponse.UserId);

                return userResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login for email {Email}.", userRequestDto?.Email);
                throw;
            }
        }

        public async Task<UserDto?> Register(UserDto userCreateDto)
        {
            if (userCreateDto is null)
                throw new ArgumentNullException(nameof(userCreateDto));

            if (string.IsNullOrWhiteSpace(userCreateDto.Email))
                throw new Exception("Email is required.");

            if (!userCreateDto.Email.Contains("@"))
                throw new Exception("Invalid email format.");

            if (string.IsNullOrWhiteSpace(userCreateDto.Password))
                throw new Exception("Password is required.");

            if (userCreateDto.Password.Length < 8)
                throw new Exception("Password must be at least 8 characters.");

            if (userCreateDto.Password != userCreateDto.PasswordConfirm)
                throw new Exception("Passwords do not match.");

            var existingUser = await _userRepository.GetUserByEmail(userCreateDto.Email);

            if (existingUser != null)
            {
                throw new Exception("Email đã được sử dụng.");
            }

            if (!string.IsNullOrEmpty(userCreateDto.Password))
            {
                userCreateDto.Password = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);
            }

            userCreateDto.Username = await GenerateUniqueUsernameAsync(userCreateDto.Email);

            User user = UserMapper.toEntity(userCreateDto);

            User userInsert;
            try
            {
                userInsert = await _userRepository.SignUpAsync(user, CommonConstant.CUSTOMER_ROLE);
            }
            catch (Exception ex)
            {
                throw new Exception("Đăng ký thất bại: " + ex.Message);
            }

            // Trả về thông tin user đã đăng ký
            return UserMapper.toDto(userInsert);
        }


        private async Task<string> GenerateUniqueUsernameAsync(string email)
        {
            string baseUsername = email.Split('@')[0];

            // Chỉ giữ lại chữ cái, số, dấu . và _
            baseUsername = Regex.Replace(baseUsername, @"[^a-zA-Z0-9._]", "");

            string username = baseUsername.ToLower();

            int attempt = 1;

            while (await _userRepository.IsUsernameExistsAsync(username))
            {
                username = $"{baseUsername}{new Random().Next(1000, 9999)}";
                attempt++;
                if (attempt > 10) throw new Exception("Failed to generate a unique username.");
            }

            return username;
        }


        public async Task<User?> GetUserByIdAsync(int? userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<List<UserRole>> GetUserRolesAsync()
        {
            return await _userRoleRepository.GetUserRolesAsync();
        }
    }
}