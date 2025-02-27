using G5_MovieTicketBookingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace G5_MovieTicketBookingSystem.Repositories.Impl
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _dbContext;

        public MovieRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Movie?> GetMovieByIdAsync(int movieId)
        {
            return await _dbContext.Movies
                .Include(m => m.Showtimes)
                    .ThenInclude(st => st.Screen)
                        .ThenInclude(s => s.ScreenSeats)
                            .ThenInclude(seat => seat.SeatType)
                .Include(m => m.Showtimes)
                    .ThenInclude(st => st.Screen)
                        .ThenInclude(s => s.Cinema)
                .FirstOrDefaultAsync(m => m.MovieId == movieId);
        }
    }
}
