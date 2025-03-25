using G5_MovieTicketBookingSystem.Commons;
using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class SeatLockRepository : ISeatLockRepository
    {
        private readonly AppDbContext _dbContext;

        public SeatLockRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task LockSeatAsync(int showtimeId, int userId, int screenSeatId)
        {
            // Check for an existing active lock for the given showtime and user.
            var existingLock = await GetUserLockAsync(showtimeId, userId);

            if (existingLock != null)
            {
                // Use the existing lock's start and expiry times.
                DateTime lockStartTime = existingLock.LockStartTime;
                DateTime lockExpiryTime = existingLock.LockExpiryTime;
                string insertQuery = $@"
                    INSERT INTO SeatLocks (UserId, ScreenSeatId, ShowtimeId, LockStartTime, LockExpiryTime)
                    VALUES ('{userId}', {screenSeatId}, {showtimeId}, '{lockStartTime:yyyy-MM-dd HH:mm:ss}', '{lockExpiryTime:yyyy-MM-dd HH:mm:ss}')
                ";
                await _dbContext.Database.ExecuteSqlRawAsync(insertQuery);
            }
            else
            {
                // Create a new lock record.
                TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                DateTime utcNow = DateTime.UtcNow;
                DateTime lockStartTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, vietnamTimeZone);
                DateTime lockExpiryTime = lockStartTime.AddMinutes(CommonConstant.LOCK_EXPIRY_MINS);
                string insertQuery = $@"
                    INSERT INTO SeatLocks (UserId, ScreenSeatId, ShowtimeId, LockStartTime, LockExpiryTime)
                    VALUES ('{userId}', {screenSeatId}, {showtimeId}, '{lockStartTime:yyyy-MM-dd HH:mm:ss}', '{lockExpiryTime:yyyy-MM-dd HH:mm:ss}')
                ";
                await _dbContext.Database.ExecuteSqlRawAsync(insertQuery);
            }
        }

        public async Task<SeatLock?> GetUserLockAsync(int showtimeId, int userId)
        {
            // Query for an active lock (with LockExpiryTime in the future) for this user and showtime.
            string query = $@"
                 SELECT TOP 1 *
                 FROM SeatLocks
                 WHERE ShowtimeId = {showtimeId}
                 AND UserId = '{userId}'
                 AND LockExpiryTime > GETDATE()
                 ORDER BY LockStartTime ASC";

            return await _dbContext.SeatLocks.FromSqlRaw(query).FirstOrDefaultAsync();
        }

        public async Task UnlockAllSeatsByExpiryAsync(int showtimeId, int userId, DateTime expiryTime)
        {
            string deleteQuery = $@"
                 DELETE FROM SeatLocks
                 WHERE UserId = '{userId}'
                 AND ShowtimeId = {showtimeId}
                 AND LockExpiryTime = '{expiryTime:yyyy-MM-dd HH:mm:ss}'";

            await _dbContext.Database.ExecuteSqlRawAsync(deleteQuery);
        }

        public async Task UnlockSeatAsync(int showtimeId, int userId, int screenSeatId)
        {
            string deleteQuery = $@"
            DELETE FROM SeatLocks
            WHERE UserId = '{userId}'
            AND ScreenSeatId = {screenSeatId}
            AND ShowtimeId = {showtimeId}
            ";

            await _dbContext.Database.ExecuteSqlRawAsync(deleteQuery);
        }

        public async Task<HashSet<int>> GetSoldSeatsAsync(int showtimeId, List<int> screenSeatIds)
        {
            // Create a comma-separated string of seat IDs
            string seatIdsString = string.Join(",", screenSeatIds);

            // Interpolate the seat IDs directly into the query for the IN clause.
            string query = $@"
                SELECT DISTINCT OI.ScreenSeatId 
                FROM OrderItems OI
                JOIN Orders O ON OI.OrderId = O.OrderId
                WHERE OI.ShowtimeId = {{0}} 
                AND OI.ScreenSeatId IN ({seatIdsString})
                AND O.OrderStatus = 'PAID'";

            var result = await _dbContext.OrderItems
                .FromSqlRaw(query, showtimeId)
                .Select(oi => oi.ScreenSeatId)
                .ToListAsync();

            return result.ToHashSet();
        }

        public async Task<Dictionary<int, int>> GetLockedSeatsWithOwnersAsync(int showtimeId, List<int> screenSeatIds)
        {
            string seatIdsString = string.Join(",", screenSeatIds);
            string query = $@"
                SELECT ScreenSeatId, UserId
                FROM SeatLocks
                WHERE ShowtimeId = {{0}}
                  AND LockExpiryTime > GETDATE()
                  AND ScreenSeatId IN ({seatIdsString})";

            var results = await _dbContext.SeatLocks
                .FromSqlRaw(query, showtimeId)
                .Select(sl => new { sl.ScreenSeatId, sl.UserId })
                .ToListAsync();

            // Build a dictionary of seatId -> userId (if multiple rows exist for a seat, you can decide to take the first)
            return results.GroupBy(x => x.ScreenSeatId)
                          .ToDictionary(g => g.Key, g => g.First().UserId);
        }
    }
}
