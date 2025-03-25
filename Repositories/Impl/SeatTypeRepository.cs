using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class SeatTypeRepository : ISeatTypeRepository
    {
        private readonly AppDbContext _context;

        public SeatTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SeatType>> GetAllSeatTypesAsync()
        {
            return await _context.SeatTypes.ToListAsync();
        }
    }
}
