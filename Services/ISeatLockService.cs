using G5_MovieTicketBookingSystem;
using G5_MovieTicketBookingSystem.Models;

public interface ISeatLockService
{
    Task<SeatLock?> GetLatestSeatLockByUserIdAsync(int? userId);
}
