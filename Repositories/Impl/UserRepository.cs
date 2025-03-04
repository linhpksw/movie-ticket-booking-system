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
                .Include(u => u.UserId)
                .Include(u => u.Email)
                .Include(u => u.Username)
                .Include(u => u.Password)
                .Include(u => u.UserRoles)
                .Include(u => u.Orders)
                .Include(u => u.SeatLocks)
                .Include(u => u.TicketScanLogs)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> SignUpAsync(User user, List<int> type)
        {
            //try
            //{
            //    // Thêm user vào DbContext và lưu trước để lấy UserId
            //    _dbContext.Users.Add(user);
            //    await _dbContext.SaveChangesAsync();

            //    // Nếu danh sách quyền (roles) không rỗng thì thêm vào UserRoles
            //    if (type != null && type.Any())
            //    {
            //        var userRoles = type.Select(x => new UserRole(user, x)); // Tạo danh sách UserRole
            //        _dbContext.UserRoles.AddRange(userRoles);
            //        await _dbContext.SaveChangesAsync();
            //    }

            //    return user;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Error occurred while signing up user", ex); // Ghi log lỗi nếu cần
            return null;
            //}
        }

        public Task<User> SignUpAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
