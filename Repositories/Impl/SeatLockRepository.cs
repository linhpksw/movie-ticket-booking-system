using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class SeatLockRepository : ISeatLockRepository
    {
        private readonly AppDbContext _dbContext;

        public SeatLockRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SeatLock?> GetByUserIdAsync(int userId)
        {
            return await _dbContext.SeatLocks
                .Where(sl => sl.UserId == userId)
                .FirstOrDefaultAsync();
        }


        public async Task<List<SeatLock>> GetAllByUserIdAsync(int? userId)
        {
            return await _dbContext.SeatLocks
                .Where(sl => sl.UserId == userId)
                .AsNoTracking() 
                .ToListAsync(); 
        }


        public async Task<SeatLock?> GetLatestByUserIdAsync(int? userId)
        {
            return await _dbContext.SeatLocks
                .Where(sl => sl.UserId == userId)
                .OrderByDescending(sl => sl.LockStartTime) 
                .FirstOrDefaultAsync();
        }

        public async Task<SeatLock?> GetLatestByMovieIdAsync(int movieId)
        {
            return await _dbContext.SeatLocks
                .Where(sl => sl.ScreenSeat != null && sl.ScreenSeat.Showtimes.Any(s => s.MovieId == movieId))
                .OrderByDescending(sl => sl.LockStartTime)
                .FirstOrDefaultAsync();
        }

        public async Task<SeatLock> CreateAsync(SeatLock seatLock)
        {
            _dbContext.SeatLocks.Add(seatLock);
            await _dbContext.SaveChangesAsync();
            return seatLock;
        }

        public async Task<bool> UpdateAsync(SeatLock seatLock)
        {
            _dbContext.SeatLocks.Update(seatLock);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int seatLockId)
        {
            var seatLock = await _dbContext.SeatLocks
                .FirstOrDefaultAsync(sl => sl.SeatLockId == seatLockId);
            if (seatLock == null)
            {
                return false;
            }

            _dbContext.SeatLocks.Remove(seatLock);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

    }
}
