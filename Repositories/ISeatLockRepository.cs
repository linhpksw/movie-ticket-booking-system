using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface ISeatLockRepository
    {
        Task LockSeatAsync(int showtimeId, int userId, int screenSeatId);

        Task UnlockSeatAsync(int showtimeId, int userId, int screenSeatId);

        Task<HashSet<int>> GetSoldSeatsAsync(int showtimeId, List<int> screenSeatIds);

        Task<Dictionary<int, int>> GetLockedSeatsWithOwnersAsync(int showtimeId, List<int> screenSeatIds);
        Task<SeatLock> GetLastByUserIdAsync(int? userId ,int? showTimeId);
        Task<SeatLock?> GetUserLockAsync(int showtimeId, int userId);
        Task<SeatLock?> GetLatestByUserIdAsync(int? userId);
        Task<bool> UpdateAsync(SeatLock seatLock);
        Task UnlockAllSeatsByExpiryAsync(int showtimeId, int userId, DateTime expiryTime);

        Task<List<SeatLock>> GetAllByUserIdAsync(int? userId);
    }
}
