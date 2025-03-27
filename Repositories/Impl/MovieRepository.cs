using G5_MovieTicketBookingSystem.Commons;
using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.DTOs.MovieBookingPlan;
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
                .FirstOrDefaultAsync(c => c.MovieId == id);
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

        public async Task<IEnumerable<MovieShowtimeDto>> GetMovieShowtimeDtos(MovieShowtimeFilterDto filter)
        {
            var query = await _dbContext.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Screen)
                    .ThenInclude(sc => sc.Cinema)
                .Where(s =>
                    (string.IsNullOrWhiteSpace(filter.City) || s.Screen.Cinema.City == filter.City) &&
                    (string.IsNullOrWhiteSpace(filter.CinemaName) || s.Screen.Cinema.CinemaName == filter.CinemaName) &&
                    (string.IsNullOrEmpty(filter.ExperienceType) || s.ExperienceType == filter.ExperienceType) &&
                    (!filter.ShowDate.HasValue || s.ShowDate == DateOnly.FromDateTime(filter.ShowDate.Value))
                )
                .Select(s => new
                {
                    s.ShowtimeId,
                    s.Movie.Title,
                    s.ShowTime,
                    s.IsSoldOut
                })
                .ToListAsync(); // ✅ Lấy dữ liệu trước khi xử lý ToTimeSpan

            var result = query
                .GroupBy(x => x.Title)
                .Select(g => new MovieShowtimeDto
                {
                    Title = g.Key,
                    Showtime = g
                        .Select(x => new ShowTimeDetailDto
                        {
                            ShowtimeId = x.ShowtimeId,
                            Showtimehour = x.ShowTime.ToTimeSpan(), // ✅ xử lý client-side
                            IsSoldOut = x.IsSoldOut
                        })
                        .OrderBy(s => s.Showtimehour)
                        .ToList()
                })
                .ToList();

            return result;
        }






    }
}
