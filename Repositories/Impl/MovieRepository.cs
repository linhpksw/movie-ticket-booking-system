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

        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            return await _dbContext.Movies
                .Include(m => m.Showtimes)
                    .ThenInclude(st => st.ScreenSeat)
                        .ThenInclude(ss => ss.Screen)
                            .ThenInclude(s => s.Cinema) // Đảm bảo lấy cả Cinema
                .Include(m => m.Showtimes)
                    .ThenInclude(st => st.ScreenSeat)
                        .ThenInclude(ss => ss.SeatType) // Đảm bảo lấy SeatType
                .FirstOrDefaultAsync(m => m.MovieId == id);
        }




    }
}
