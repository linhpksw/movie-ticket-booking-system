using G5_MovieTicketBookingSystem.Commons;
using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Models;
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

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _dbContext.Movies
                .Include(m => m.Showtimes)
                    .ThenInclude(st => st.Screen)
                            .ThenInclude(s => s.Cinema) // Đảm bảo lấy cả Cinema
                .Include(m => m.Showtimes)
                    .ThenInclude(st => st.Screen)
                    .ThenInclude(s => s.ScreenSeats)
                        .ThenInclude(ss => ss.SeatType) // Đảm bảo lấy SeatType
                .FirstOrDefaultAsync(m => m.MovieId == id);
            return null;
        }

        // 1) Đang khởi chiếu (ReleaseDate <= Today)
        public async Task<List<Movie?>> GetNowShowingAsync()
        {
            return await _dbContext.Movies
                .Where(m => m.ReleaseDate <= DateTime.Now)
                .ToListAsync();
        }

        // 2) Sắp khởi chiếu (ReleaseDate > Today)
        public async Task<List<Movie?>> GetComingSoonAsync()
        {
            return await _dbContext.Movies
                .Where(m => m.ReleaseDate > DateTime.Now)
                .ToListAsync();
        }

        // 3) Phim HOT (Top x highest rated)
        public async Task<List<Movie?>> GetHotMoviesAsync()
        {
            return await _dbContext.Movies
                .OrderByDescending(m => m.Rating) // or whatever rating property you have
                .Take(CommonConstant.TOP_HOT_MOVIE)
                .ToListAsync();
        }
    }
}
