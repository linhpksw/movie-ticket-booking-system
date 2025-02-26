using G5_MovieTicketBookingSystem.Repositories;
using G5_MovieTicketBookingSystem;
using Microsoft.AspNetCore.SignalR;
using G5_MovieTicketBookingSystem.Hubs;

public class SeatLockService : ISeatLockService
{
    private readonly ISeatLockRepository _seatLockRepository;
    private readonly IHubContext<CountdownHub> _hubContext;

    public SeatLockService(ISeatLockRepository seatLockRepository, IHubContext<CountdownHub> hubContext)
    {
        _seatLockRepository = seatLockRepository;
        _hubContext = hubContext;
    }

    public async Task<SeatLock?> GetLatestSeatLockByUserIdAsync(int userId)
    {
        var seatLock = await _seatLockRepository.GetLatestByUserIdAsync(userId);
        return seatLock;
    }

    public async Task SendTimeToClients(string timeRemaining)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveTime", timeRemaining);
    }
}
