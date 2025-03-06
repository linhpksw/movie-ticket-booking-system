using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.DTOs.MovieBookingPlan;
using Microsoft.Data.SqlClient;
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
            var query = _dbContext.Showtimes.AsQueryable();

            // Kiểm tra cả null và chuỗi rỗng để bỏ qua điều kiện lọc
            if (!string.IsNullOrWhiteSpace(filter.City))
            {
                query = query.Where(s => s.ScreenSeat.Screen.Cinema.City == filter.City);
            }

            if (!string.IsNullOrWhiteSpace(filter.CinemaName))
            {
                query = query.Where(s => s.ScreenSeat.Screen.Cinema.CinemaName == filter.CinemaName);
            }

            if (!string.IsNullOrEmpty(filter.ExperienceType))
            {
                query = query.Where(s => s.ExperienceType == filter.ExperienceType);
            }

            if (filter.ShowDate != null)
            {
                query = query.Where(s => s.ShowDate == filter.ShowDate);
                /*query = query.Where(s => s.ShowDate == new DateTime(2025,3,7));*/
            }

            var rawResult = await query
                .Select(s => new
                {
                    Title = s.Movie.Title,
                    Showtime = s.ShowTime
                })
                .ToListAsync();

            var groupedResult = rawResult
                .GroupBy(r => r.Title)
                .Select(g => new MovieShowtimeDto
                {
                    Title = g.Key,
                    Showtime = g.Select(x => x.Showtime).ToList()
                })
                .ToList();

            return groupedResult;
        }



    }
}
