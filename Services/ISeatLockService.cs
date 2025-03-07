using G5_MovieTicketBookingSystem;
using G5_MovieTicketBookingSystem.Models;

public interface ISeatLockService
{
    Task<SeatLock?> GetLatestSeatLockByUserIdAsync(int? userId);

    Task UpdateExpirytimeByUserIdAsync(int? userId, DateTime expiryTime);
    Task UpdateStarttimeByUserIdAsync(int? userId, DateTime expiryTime);
}
