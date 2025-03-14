using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.Models;
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
            return await _dbContext.Showtimes.FirstOrDefaultAsync(s => s.ShowtimeId == id);
        }

        public async Task<Cinema?> GetCinemaByShowtimeId(int showtimeId)
        {
            string query = @"
                SELECT C.*
                FROM Showtimes ST
                JOIN Screens S ON ST.ScreenId = S.ScreenId
                JOIN Cinemas C ON S.CinemaId = C.CinemaId
                WHERE ST.ShowtimeId = {0}";

            return await _dbContext.Cinemas
                .FromSqlRaw(query, showtimeId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Showtime>> GetShowTimeByMovieAndCinemaWithinDay(int movieId, int cinemaId, DateOnly showDate)
        {
            string query = @"
                SELECT ST.* 
                FROM Showtimes ST  
                JOIN dbo.Movies M ON M.MovieId = ST.MovieId
                JOIN Screens S ON S.ScreenId = ST.ScreenId
                JOIN Cinemas C ON C.CinemaId = S.CinemaId
                WHERE ST.ShowDate = {0} AND C.CinemaId = {1} AND M.MovieId = {2}";

            return await _dbContext.Showtimes
                .FromSqlRaw(query, showDate, cinemaId, movieId)
                .ToListAsync();
        }
        public async Task<Showtime?> GetShowtimeById(int showtimeId)
        {
            return await _dbContext.Showtimes.FirstOrDefaultAsync(s => s.ShowtimeId == showtimeId);
        }

        public async Task<Showtime> GetShowtimeByScreenSeatId(int screenSeatId)
        {
            return await _dbContext.Showtimes
                .AsNoTracking()  // Đảm bảo không theo dõi
                .Include(s => s.Screen)
                .ThenInclude(sc => sc.ScreenSeats)
                .FirstOrDefaultAsync(s => s.Screen.ScreenSeats
                    .Any(ss => ss.ScreenSeatId == screenSeatId));
        }


    }
}
