using G5_MovieTicketBookingSystem;
using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;
using System.Threading.Tasks;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface ISeatLockService
    {
        Task LockSeatAsync(int showtimeId, int userId, int screenSeatId);
        Task UnlockSeatAsync(int showtimeId, int userId, int screenSeatId);
        Task<Dictionary<int, SeatStatus>> GetSeatsAvailabilityAsync(int showtimeId, List<int> screenSeatIds);
        Task<SeatLock?> GetUserLockAsync(int showtimeId, int userId);
        Task UnlockAllSeatsByExpiryAsync(int showtimeId, int userId, DateTime expiryTime);
        Task<SeatLock?> GetLatestSeatLockByUserIdAsync(int? userId);
        Task UpdateStarttimeByUserIdAsync(int? userId, DateTime expiryTime);
        Task UpdateExpirytimeByUserIdAsync(int? userId, DateTime expiryTime);
        Task<List<SeatLock>> GetAllByUserIdAsync(int? userId);
    }
}