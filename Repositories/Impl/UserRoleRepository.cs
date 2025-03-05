
using G5_MovieTicketBookingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class UserRoleRepository : IUserRoleRepository

    {
        private readonly AppDbContext _dbContext;


        public UserRoleRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<UserRole> AssignRoleToUserAsync(int userId, int roleId)
        {
            // Kiểm tra xem UserRole đã tồn tại chưa để tránh trùng lặp
            var existingUserRole = await _dbContext.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            if (existingUserRole != null)
            {
                return existingUserRole; // Trả về nếu đã tồn tại
            }

            // Lấy User và Role từ cơ sở dữ liệu
            var user = await _dbContext.Users.FindAsync(userId);
            var role = await _dbContext.Roles.FindAsync(roleId);
            if (user == null || role == null)
            {
                throw new Exception("User hoặc Role không tồn tại");
            }

            // Tạo UserRole với đầy đủ các thuộc tính bắt buộc
            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId,
                User = user,
                Role = role
            };

            _dbContext.UserRoles.Add(userRole);
            await _dbContext.SaveChangesAsync();
            return userRole;
        }
    }
}
