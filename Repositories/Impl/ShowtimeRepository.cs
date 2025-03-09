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
            return await _dbContext.Showtimes
             .Where(st => st.ShowtimeId == showtimeId)
             .Join(_dbContext.ScreenSeats, st => st.ScreenSeatId, ss => ss.ScreenSeatId, (st, ss) => new { st, ss })
             .Join(_dbContext.Screens, temp => temp.ss.ScreenId, sc => sc.ScreenId, (temp, sc) => new { temp, sc })
             .Join(_dbContext.Cinemas, temp2 => temp2.sc.CinemaId, c => c.CinemaId, (temp2, c) => c)
             .FirstOrDefaultAsync();
        }

        public async Task<List<Showtime>> GetShowTimeByMovieAndCinemaWithinDay(int movieId, int cinemaId, DateOnly showDate)
        {
            string query = @"
                SELECT ST.* 
                FROM Showtimes ST  
                JOIN dbo.Movies M ON M.MovieId = ST.MovieId
                JOIN dbo.ScreenSeats SS ON SS.ScreenSeatId = ST.ScreenSeatId
                JOIN Screens S ON S.ScreenId = SS.ScreenId
                JOIN Cinemas C ON C.CinemaId = S.CinemaId
                WHERE ST.ShowDate = {0} AND C.CinemaId = {1} AND M.MovieId = {2}";

            return await _dbContext.Showtimes
                .FromSqlRaw(query, showDate, cinemaId, movieId)
                .ToListAsync();
        }
    }
}
