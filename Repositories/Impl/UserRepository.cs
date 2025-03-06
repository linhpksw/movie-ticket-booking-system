using G5_MovieTicketBookingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class UserRepository :IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(int? userId)
        {
            return await _context.Users
                .Include(u => u.UserRoles)  // Load các vai trò của user
                .Include(u => u.Orders)     // Load các đơn hàng của user
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}
