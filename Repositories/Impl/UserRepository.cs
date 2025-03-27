
using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;


        public UserRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users
                .AsNoTracking() // Bỏ qua cache của DbContext
                .Include(u => u.UserRoles) // Load quan hệ UserRoles
                .Include(u => u.Orders)    // Load quan hệ Orders
                .Include(u => u.SeatLocks) // Load quan hệ SeatLocks
                .Include(u => u.TicketScanLogs) // Load quan hệ TicketScanLogs
                .FirstOrDefaultAsync(u => u.Email == email);
        }


        public async Task<User> SignUpAsync(UserRegisterRequestDto user, int roleId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1) Create a new User entity
                var newUser = new User
                {
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password,
                    Fullname = user.Fullname
                };

                // Add and save the new user
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                // 2) Insert into [UserRoles] using the new user's ID
                var userRole = new UserRole
                {
                    UserId = newUser.UserId,
                    RoleId = roleId
                };
                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();

                // Commit the transaction
                await transaction.CommitAsync();

                // Return the newly inserted user (with its generated UserId)
                return newUser;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task<bool> IsUsernameExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }


        public async Task<User?> GetUserByIdAsync(int? userId)
        {
            return await _context.Users
                .Include(u => u.UserRoles)  // Load các vai trò của user
                .Include(u => u.Orders)     // Load các đơn hàng của user
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);  // Cập nhật người dùng
            await _context.SaveChangesAsync();  // Lưu thay đổi vào cơ sở dữ liệu
            return user;  // Trả về người dùng đã được cập nhật
        }


        public async Task<ScreenSeat> GetScreenSeatByUserId(int? UserId)
        {
            var user = _context.Users
                .Include(u => u.SeatLocks)
                .ThenInclude(u => u.ScreenSeat)
                .LastOrDefault(u => u.UserId == UserId);
            return user?.SeatLocks.FirstOrDefault()?.ScreenSeat;
        }
    }
}