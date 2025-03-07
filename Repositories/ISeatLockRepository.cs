using System.Threading.Tasks;

namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface ISeatLockRepository
    {
        Task<SeatLock?> GetByUserIdAsync(int userId);
        Task<SeatLock?> GetLatestByUserIdAsync(int? userId);
        Task<SeatLock?> GetLatestByMovieIdAsync(int movieId);
        Task<SeatLock> CreateAsync(SeatLock seatLock);
        Task<bool> UpdateAsync(SeatLock seatLock);
        Task<bool> DeleteAsync(int seatLockId);
        Task<List<SeatLock>> GetAllByUserIdAsync(int? userId);
    }
}
