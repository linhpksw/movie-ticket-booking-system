using G5_MovieTicketBookingSystem.DTOs.UserDto;
using G5_MovieTicketBookingSystem.Models;
using G5_MovieTicketBookingSystem.Repositories;
using G5_MovieTicketBookingSystem.Util;
namespace G5_MovieTicketBookingSystem.Services.Impl
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ILogger<UserServices> _logger;

        public UserServices(IUserRepository userRepository, ILogger<UserServices> logger, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userRoleRepository = userRoleRepository ?? throw new ArgumentNullException(nameof(userRoleRepository));
        }

        public async Task<UserResponseDto> Login(UserRequestDto userRequestDto)
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

                var userResponse = UserMapper.MapToUserResponseDto(existingUser);


                _logger.LogInformation("User {Email} logged in successfully with UserId {UserId}.", userResponse.Email, userResponse.UserId);

                return userResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login for email {Email}.", userRequestDto?.Email);
                throw;
            }
        }
        public async Task<UserResponseDto> RegisterGoogle(UserCreateDto userCreateDto, List<int> roleIds)
        {
            var existingUser = await _userRepository.GetUserByEmail(userCreateDto.Email);
            if (existingUser != null)
            {

                return UserMapper.MapToUserResponseDto(existingUser);
            }

            if (!string.IsNullOrEmpty(userCreateDto.Password))
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
        public async Task<UserResponseDto?> Register(UserCreateDto userCreateDto, List<int> roleIds)
        {
            // Kiểm tra xem email đã tồn tại chưa
            var existingUser = await _userRepository.GetUserByEmail(userCreateDto.Email);
            if (existingUser != null)
            {
                throw new Exception("Email đã được sử dụng.");
            }

            // Mã hóa mật khẩu nếu có nhập
            if (!string.IsNullOrEmpty(userCreateDto.Password))
            {
                userCreateDto.Password = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);
            }

            // Tạo tên người dùng duy nhất dựa trên email
            string uniqueUsername = await GenerateUniqueUsernameAsync(userCreateDto.Email);
            userCreateDto.username = uniqueUsername;

            // Chuyển đổi DTO thành Entity
            User user = UserMapper.CreateToUser(userCreateDto);

            // Thêm user vào database
            User userInsert;
            try
            {
                userInsert = await _userRepository.SignUpAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Đăng ký thất bại: " + ex.Message);
            }

            // Gán các role cho user (chạy đồng thời để tối ưu hiệu suất)
            var roleAssignments = roleIds.Select(roleId =>
                _userRoleRepository.AssignRoleToUserAsync(userInsert.UserId, roleId));
            await Task.WhenAll(roleAssignments);

            // Trả về thông tin user đã đăng ký
            return UserMapper.MapToUserResponseDto(userInsert);
        }


        public async Task<string> GenerateUniqueUsernameAsync(string email)
        {
            string baseUsername = UserMapper.GenerateBaseUsername(email);
            string username = baseUsername;
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

        public async Task<UserResponseDto> ChangePasswordAsync(string email, string newPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentException("Email or password cannot be empty.");
            }

            // Tìm người dùng qua email
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Cập nhật mật khẩu mới
            user.Password = newPassword;  // Lưu ý: Mã hóa mật khẩu trong thực tế
            await _userRepository.UpdateUserAsync(user);

            // Chuyển đổi User thành UserResponseDto
            return UserMapper.MapToUserResponseDto(user);
        }
    }
}