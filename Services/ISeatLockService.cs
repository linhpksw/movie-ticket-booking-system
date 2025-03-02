using G5_MovieTicketBookingSystem;

public interface ISeatLockService
{
    Task<SeatLock?> GetLatestSeatLockByUserIdAsync(int? userId);
}
