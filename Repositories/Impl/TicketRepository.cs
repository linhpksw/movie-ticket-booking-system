using G5_MovieTicketBookingSystem.Components.Pages;
using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddTicketAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfTicketExistsAsync(string uniqueCode)
        {
            return await _context.Tickets.AnyAsync(t => t.UniqueCode == uniqueCode && t.TicketStatus == "Active");
        }

        public async Task<TicketInfoDto> GetTicketInfoAsync(string uniqueCode)
        {
            var ticket = await _context.Tickets
                .Where(t => t.UniqueCode == uniqueCode) // hoặc điều kiện khác bạn dùng
                .Join(_context.OrderItems,
                    t => t.OrderItemId,
                    oi => oi.OrderItemId,
                    (t, oi) => new { t, oi })
                .Join(_context.Showtimes,
                    temp => temp.oi.ShowtimeId,
                    s => s.ShowtimeId,
                    (temp, s) => new { temp.t, temp.oi, s })
                .Join(_context.ScreenSeats,
                    temp => temp.oi.ScreenSeatId,
                    ss => ss.ScreenSeatId,
                    (temp, ss) => new { temp.t, temp.s, ss })
                .Join(_context.Movies,
                    temp => temp.s.MovieId,
                    m => m.MovieId,
                    (temp, m) => new TicketInfoDto
                    {
                        MovieTitle = m.Title,
                        Showtime = $"{temp.s.ShowDate:yyyy-MM-dd} {temp.s.ShowTime:hh\\:mm}",
                        SeatNumber = temp.ss.SeatLabel
                    })
                .FirstOrDefaultAsync();

            if (ticket != null)
            {
                var ticketToUpdate = await _context.Tickets.FirstOrDefaultAsync(t => t.UniqueCode == uniqueCode);
                if (ticketToUpdate != null)
                {
                    ticketToUpdate.TicketStatus = "InActive";
                    await _context.SaveChangesAsync();
                }
            }

            return ticket; // ✅ luôn có return, kể cả là null
        }


    }
}
