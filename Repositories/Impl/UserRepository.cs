using G5_MovieTicketBookingSystem.Data;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;


        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> EmailExistsAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> LoginAsync(string emailOrUsername, string password)
        {
            User user = _dbContext.Users.FirstOrDefault(u => u.Email == emailOrUsername || u.Username == emailOrUsername);
            return null;


        }

        public Task<User> SignUpAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UsernameExistsAsync(string username)
        {
            throw new NotImplementedException();
        }
        static bool VerifyPassword(string password, string hashedPassword)
        {
            // So sánh mật khẩu nhập vào với mật khẩu đã mã hóa
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        static string HashPassword(string password)
        {
            // Mã hóa mật khẩu với salt ngẫu nhiên (work factor mặc định là 12)
            string hashed = BCrypt.Net.BCrypt.HashPassword(password);
            return hashed;
        }
    

    static void Main(string[] args)
        {
            // Giả sử đây là mật khẩu đã được hash và lưu trong database
            string storedHashedPassword = "$2a$12$..."; // Giá trị hash từ lần đăng ký

            // Kiểm tra mật khẩu nhập vào
            string userInputPassword = "mySecurePassword";
            bool isValid = UserRepository.VerifyPassword(userInputPassword, storedHashedPassword);

            Console.WriteLine("Mật khẩu đúng: " + isValid); // True

            // Thử với mật khẩu sai
            isValid = UserRepository.VerifyPassword("wrongPassword", storedHashedPassword);
            Console.WriteLine("Mật khẩu sai: " + isValid); // False
        }

      
    }
}
