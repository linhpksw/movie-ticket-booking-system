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

    public async Task<List<SeatLock>> GetAllByUserIdAsync(int? userId)
    {
        if (!userId.HasValue)
        {
            return new List<SeatLock>(); 
        }

        return await _seatLockRepository.GetAllByUserIdAsync(userId.Value);
    }


    public async Task<SeatLock?> GetLatestSeatLockByUserIdAsync(int? userId)
    {
        var seatLock = await _seatLockRepository.GetLatestByUserIdAsync(userId);
        return seatLock;
    }

    public async Task UpdateExpirytimeByUserIdAsync(int? userId, DateTime expiryTime)
    {
        if (!userId.HasValue)
        {
            Console.WriteLine("User ID is null.");
            return;
        }

        var seatLock = await _seatLockRepository.GetLatestByUserIdAsync(userId);
        if (seatLock != null)
        {
            seatLock.LockExpiryTime = expiryTime;
            await _seatLockRepository.UpdateAsync(seatLock);
        }
        else
        {
            Console.WriteLine($"No seat lock found for User ID {userId}");
        }
    }

    public async Task UpdateStarttimeByUserIdAsync(int? userId, DateTime expiryTime)
    {
        if (!userId.HasValue)
        {
            Console.WriteLine("User ID is null.");
            return;
        }

        var seatLock = await _seatLockRepository.GetLatestByUserIdAsync(userId);
        if (seatLock != null)
        {
            seatLock.LockStartTime = expiryTime;
            await _seatLockRepository.UpdateAsync(seatLock);
        }
        else
        {
            Console.WriteLine($"No seat lock found for User ID {userId}");
        }
    }
}
