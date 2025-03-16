using G5_MovieTicketBookingSystem.Models;
using System.Threading.Tasks;

namespace G5_MovieTicketBookingSystem.Repositories
{
    public interface ISeatLockRepository
    {
        Task LockSeatAsync(int showtimeId, int userId, int screenSeatId);

        Task UnlockSeatAsync(int showtimeId, int userId, int screenSeatId);

        Task<HashSet<int>> GetSoldSeatsAsync(int showtimeId, List<int> screenSeatIds);

        Task<Dictionary<int, int>> GetLockedSeatsWithOwnersAsync(int showtimeId, List<int> screenSeatIds);
    }
}
