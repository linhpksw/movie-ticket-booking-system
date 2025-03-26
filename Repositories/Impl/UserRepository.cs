
using G5_MovieTicketBookingSystem.Data;
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


        public async Task<User> SignUpAsync(User user, int roleId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1) Insert into [Users] with OUTPUT to get the newly inserted row
                var sqlInsertUser = @"
                    INSERT INTO [Users] (Username, Email, Password, Fullname)
                    OUTPUT 
                        INSERTED.UserId, 
                        INSERTED.Username, 
                        INSERTED.Email, 
                        INSERTED.Password, 
                        INSERTED.Fullname
                    VALUES ({0}, {1}, {2}, {3});
                ";

                // EF will map the OUTPUT columns back into a User entity
                var insertedUser = await _context.Users
                    .FromSqlRaw(
                        sqlInsertUser,
                        user.Username,
                        user.Email,
                        user.Password,
                        user.Fullname
                    )
                    .FirstOrDefaultAsync();

                if (insertedUser == null)
                {
                    // Handle the unlikely case of an insert returning no rows
                    throw new Exception("Failed to insert user.");
                }

                // 2) Insert into [UserRoles] using the new user_id
                await _context.Database.ExecuteSqlRawAsync(
                    "INSERT INTO [UserRoles] (UserId, RoleId) VALUES ({0}, {1});",
                    insertedUser.UserId,
                    roleId
                );

                // If needed, commit the transaction
                await transaction.CommitAsync();

                // Return the user with the new primary key
                return user;
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
    }
}