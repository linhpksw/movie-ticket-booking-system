using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IUserRoleRepository
    {
        Task<List<UserRole>> GetUserRolesAsync();
    }
}
