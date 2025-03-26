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

        public async Task<List<Showtime>> GetUpcomingShowtimes()
        {
            string query = @"
                SELECT *
                FROM Showtimes
                WHERE ShowDate >= CAST(GETDATE() AS DATE)
                  AND ShowDate < DATEADD(day, 7, CAST(GETDATE() AS DATE))
                ORDER BY ShowDate ASC
            ";

            return await _dbContext.Showtimes.FromSqlRaw(query).ToListAsync(); ;
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

        public Task<Showtime> getShowTimeById(int id)
        {
            return _dbContext.Showtimes.FirstOrDefaultAsync(s => s.ShowtimeId == id);
        }

        public async Task UpdateSoldOut(int id)
        {
            var showtime = await _dbContext.Showtimes.FirstOrDefaultAsync(s => s.ShowtimeId == id);

            if (showtime != null)
            {
                showtime.IsSoldOut = true;

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine($"Không tìm thấy Showtime với ShowtimeId = {id}");
            }
        }

        public Task<ScreenSeat> GetScreenSeatByShowtimeId(int showTimeId)
        {
            var showtime = _dbContext.Showtimes
                .Include(s => s.Screen)
                .ThenInclude(sc => sc.ScreenSeats)
                .FirstOrDefault(s => s.ShowtimeId == showTimeId);

            if (showtime != null && showtime.Screen != null && showtime.Screen.ScreenSeats.Any())
            {
                return Task.FromResult(showtime.Screen.ScreenSeats.LastOrDefault());
            }
            else
            {
                return Task.FromResult<ScreenSeat>(null);
            }
        }

    }
}