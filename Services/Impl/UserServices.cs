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

        public async Task<UserResponseDto> Login(UserLoginRequestDto request)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("Login attempt with null user request.");
                    return null;
                }

                var existingUser = await _userRepository.GetUserByEmail(request.Email);

                if (existingUser == null)
                {
                    _logger.LogWarning("Login failed: User with email {Email} not found.", request.Email);
                    return null;
                }

                // Kiểm tra mật khẩu bằng BCrypt.Verify
                if (string.IsNullOrEmpty(request.Password) || !BCrypt.Net.BCrypt.Verify(request.Password, existingUser.Password))
                {
                    _logger.LogWarning("Login failed: Invalid password for user {Email}.", request.Email);
                    return null;
                }

                UserResponseDto userResponse = UserMapper.toResponseDto(existingUser);


                _logger.LogInformation("User {Email} logged in successfully with UserId {UserId}.", userResponse.Email, userResponse.UserId);

                return userResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login for email {Email}.", request?.Email);
                throw;
            }
        }

        public async Task<UserResponseDto?> Register(UserRegisterRequestDto request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Email))
                throw new Exception("Email is required.");

            if (!request.Email.Contains("@"))
                throw new Exception("Invalid email format.");

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new Exception("Password is required.");

            if (request.Password.Length < 8)
                throw new Exception("Password must be at least 8 characters.");

            if (request.Password != request.PasswordConfirm)
                throw new Exception("Passwords do not match.");

            var existingUser = await _userRepository.GetUserByEmail(request.Email);

            if (existingUser != null)
            {
                throw new Exception("Email đã được sử dụng.");
            }

            if (!string.IsNullOrEmpty(request.Password))
            {
                request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }

            request.Username = await GenerateUniqueUsernameAsync(request.Email);

            try
            {
                User user = await _userRepository.SignUpAsync(request, CommonConstant.CUSTOMER_ROLE);

                // Trả về thông tin user đã đăng ký
                return UserMapper.toResponseDto(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Đăng ký thất bại: " + ex.Message);
            }
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

        public Task<ScreenSeat> GetScreenSeatByUserId(int? userid)
        {
            return _userRepository.GetScreenSeatByUserId(userid);
        }
    }
}