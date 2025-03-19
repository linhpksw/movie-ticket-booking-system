using G5_MovieTicketBookingSystem.Models;
using G5_MovieTicketBookingSystem.Repositories;

public class SeatLockService : ISeatLockService
{
    private readonly ISeatLockRepository _seatLockRepository;

    public SeatLockService(ISeatLockRepository seatLockRepository)
    {
        _seatLockRepository = seatLockRepository;
    }

    public async Task<SeatLock?> GetLatestSeatLockByUserIdAsync(int? userId)
    {
        var seatLock = await _seatLockRepository.GetLatestByUserIdAsync(userId);
        return seatLock;
    }


}
