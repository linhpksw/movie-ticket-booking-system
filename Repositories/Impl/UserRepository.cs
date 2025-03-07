using G5_MovieTicketBookingSystem.Data;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;


        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users
                .AsNoTracking() // Bỏ qua cache của DbContext
                .Include(u => u.UserRoles) // Load quan hệ UserRoles
                .Include(u => u.Orders)    // Load quan hệ Orders
                .Include(u => u.SeatLocks) // Load quan hệ SeatLocks
                .Include(u => u.TicketScanLogs) // Load quan hệ TicketScanLogs
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> SignUpAsync(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                Console.WriteLine("User successfully added to database."); // Debug log
                return user; // Trả về user sau khi lưu thành công
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw; // Giữ nguyên lỗi để debug
            }
        }


        public async Task<bool> IsUsernameExistsAsync(string username)
        {
            return await _dbContext.Users.AnyAsync(u => u.Username == username);
        }





    }
}
