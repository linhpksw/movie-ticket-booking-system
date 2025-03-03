using G5_MovieTicketBookingSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
}
