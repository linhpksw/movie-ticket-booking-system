
using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Models;
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

        public async Task<List<UserRole>> GetUserRolesAsync()
        {
            return await _dbContext.UserRoles.ToListAsync();
        }


    }
}
