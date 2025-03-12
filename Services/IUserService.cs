using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(int? userId);
    }
}
