namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int? userId);
    }
}
