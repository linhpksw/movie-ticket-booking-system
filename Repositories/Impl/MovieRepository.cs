using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.DTOs;
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

        public async Task<IEnumerable<MovieShowtimeDto>> GetMovieShowtimeDtos(MovieShowtimeFilterDto filter)
        {
            var query = await (from s in _dbContext.Showtimes
                               join m in _dbContext.Movies on s.MovieId equals m.MovieId
                               join ss in _dbContext.ScreenSeats on s.ScreenSeatId equals ss.ScreenSeatId
                               join sc in _dbContext.Screens on ss.ScreenId equals sc.ScreenId
                               join c in _dbContext.Cinemas on sc.CinemaId equals c.CinemaId
                               where c.City == filter.City
                                     && c.CinemaName == filter.CinemaName
                                     && s.ExperienceType == filter.ExperienceType
                                     && s.ShowDate == filter.ShowDate
                               group s by m.Title into g
                               select new MovieShowtimeDto
                               {
                                   Title = g.Key,
                                   Showtime = g.Select(x => x.ShowTime).ToList()
                               }).ToListAsync();
            return query;
        }
    }
}
