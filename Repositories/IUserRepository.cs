using G5_MovieTicketBookingSystem.DTOs.UserDto;

namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IUserRepository
    {
        // Đăng ký người dùng mới
        Task<User> SignUpAsync(User user);

        // Đăng nhập người dùng
        Task<User?> GetUserByEmail(string email);
        Task<bool> IsUsernameExistsAsync(string username);

       // Kiểm tra sự tồn tại của Username hoặc Email trước khi đăng ký
       //Task<bool> UsernameExistsAsync(string username);
       //Task<bool> EmailExistsAsync(string email);
    }
}
