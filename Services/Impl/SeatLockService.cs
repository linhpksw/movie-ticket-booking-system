using G5_MovieTicketBookingSystem.Repositories;
using G5_MovieTicketBookingSystem;
using Microsoft.AspNetCore.SignalR;

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
