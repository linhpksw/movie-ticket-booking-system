

using G5_MovieTicketBookingSystem.Models;


namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IUserRepository
    {
        Task<User> SignUpAsync(User user, int roleId);
        Task<User?> GetUserByEmail(string email);
        Task<bool> IsUsernameExistsAsync(string username);
        Task<User?> GetUserByIdAsync(int? userId);
        public Task<User?> UpdateUserAsync(User user);


    }
}