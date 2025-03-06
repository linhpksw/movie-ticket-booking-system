
using G5_MovieTicketBookingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class ShowtimeRepository : IShowtimeRepository
    {
        private readonly AppDbContext _dbContext;

        public ShowtimeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Showtime?> GetByIdAsync(int id)
        {
            return await _dbContext.Showtimes.Where(s => s.ShowtimeId == id).FirstOrDefaultAsync();
        }

    }
}
