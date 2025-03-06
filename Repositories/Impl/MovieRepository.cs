using G5_MovieTicketBookingSystem.Data;
using G5_MovieTicketBookingSystem.DTOs.MovieBookingPlan;
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
            var query = await _dbContext.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.ScreenSeat.Screen.Cinema)
                .Include(s => s.ScreenSeat.Screen.ScreenSeats)
                .Where(s =>
                    (string.IsNullOrWhiteSpace(filter.City) || s.ScreenSeat.Screen.Cinema.City == filter.City) &&
                    (string.IsNullOrWhiteSpace(filter.CinemaName) || s.ScreenSeat.Screen.Cinema.CinemaName == filter.CinemaName) &&
                    (string.IsNullOrEmpty(filter.ExperienceType) || s.ExperienceType == filter.ExperienceType) &&
                    (filter.ShowDate == null || s.ShowDate == filter.ShowDate)
                )
                .GroupBy(s => new { s.Movie.Title, s.ShowTime }) // Gom nhóm theo phim + suất chiếu
                .Select(g => new
                {
                    Title = g.Key.Title,
                    Showtimehour = g.Key.ShowTime,
                    TotalSeats = g.SelectMany(s => s.ScreenSeat.Screen.ScreenSeats).Count(),
                    BookedSeats = g.SelectMany(s => s.ScreenSeat.Screen.ScreenSeats)
                        .Count(ss => _dbContext.OrderItems
                            .Any(oi => oi.ScreenSeatId == ss.ScreenSeatId && oi.Order.OrderStatus == "Paid")) // Chỉ tính Order đã thanh toán
                })
                .ToListAsync(); // Thực thi trên DB trước khi nhóm lại lần nữa

            var result = query
                .GroupBy(x => x.Title)
                .Select(g => new MovieShowtimeDto
                {
                    Title = g.Key,
                    Showtime = g.Select(x => new ShowTimeDetailDto
                    {
                        Showtimehour = x.Showtimehour,
                        AvailableSeats = x.TotalSeats - x.BookedSeats
                    }).OrderBy(s => s.Showtimehour).ToList()
                })
                .ToList();

            return result;
        }


    }
}
