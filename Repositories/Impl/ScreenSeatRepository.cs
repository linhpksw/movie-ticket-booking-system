using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class ScreenSeatRepository : IScreenSeatRepository
    {
        private readonly AppDbContext _context;

        public ScreenSeatRepository(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Lấy danh sách tất cả ScreenSeats
        public async Task<List<ScreenSeat>> GetAllScreenSeatsAsync()
        {
            return await _context.ScreenSeats
                .Include(s => s.Screen)
                .Include(s => s.SeatType)
                .ToListAsync();
        }

        // 🔹 Lấy ScreenSeat theo ID
        public async Task<ScreenSeat?> GetScreenSeatByIdAsync(int screenSeatId)
        {
            return await _context.ScreenSeats
                .Include(s => s.Screen)
                .Include(s => s.SeatType)
                .FirstOrDefaultAsync(s => s.ScreenSeatId == screenSeatId);
        }

        // 🔹 Thêm mới một ScreenSeat
        public async Task<ScreenSeat> AddScreenSeatAsync(ScreenSeat screenSeat)
        {
            _context.ScreenSeats.Add(screenSeat);
            await _context.SaveChangesAsync();
            return screenSeat;
        }

        // 🔹 Cập nhật ScreenSeat
        public async Task UpdateScreenSeatAsync(ScreenSeat screenSeat)
        {
            _context.ScreenSeats.Update(screenSeat);
            await _context.SaveChangesAsync();
        }

        // 🔹 Xóa một ScreenSeat
        public async Task DeleteScreenSeatAsync(int screenSeatId)
        {
            var screenSeat = await GetScreenSeatByIdAsync(screenSeatId);
            if (screenSeat != null)
            {
                _context.ScreenSeats.Remove(screenSeat);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ScreenSeat>> GetScreenSeatsByShowtime(int movieId, int cinemaId, DateOnly showDate, TimeOnly showTime)
        {
            string query = @"
                SELECT SS.* 
                FROM ScreenSeats SS
                JOIN Screens S ON S.ScreenId = SS.ScreenId
                JOIN Showtimes ST ON ST.ScreenId = S.ScreenId
                JOIN Movies M ON M.MovieId = ST.MovieId
                JOIN Cinemas C ON C.CinemaId = S.CinemaId
                WHERE ST.ShowDate = {0} 
                AND ST.ShowTime = {1} 
                AND M.MovieId = {2} 
                AND C.CinemaId = {3}
                ORDER BY SS.SeatLabel";

            return await _context.ScreenSeats
                .FromSqlRaw(query, showDate, showTime, movieId, cinemaId)
                .ToListAsync();
        }

        public async Task<ScreenSeat> GetScreenSeatByUserIdAsync(int? userId)
        {
            var seatLock = await _context.SeatLocks
                .Where(sl => sl.UserId == userId)
                .OrderByDescending(sl => sl.SeatLockId)
                .FirstOrDefaultAsync();

            if (seatLock == null)
            {
                return null; // Nếu không tìm thấy SeatLock, trả về null
            }

            var screenSeat = await _context.ScreenSeats
                .FirstOrDefaultAsync(ss => ss.ScreenSeatId == seatLock.ScreenSeatId);

            return screenSeat;
        }

    }
}
