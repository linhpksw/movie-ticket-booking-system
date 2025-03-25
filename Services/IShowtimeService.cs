using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;

namespace G5_MovieTicketBookingSystem.Services
{
    public interface IShowtimeService
    {
        Task<ShowtimeDto?> GetByIdAsync(int id);

        Task<CinemaDto?> GetCinemaByShowtimeId(int showtimeId);
        Task<Showtime> GetShowtimeByScreenSeatId(int ScreenSeatId);

        Task<List<ShowtimeDto>> GetShowTimeByMovieAndCinemaWithinDay(int movieId, int cinemaId, DateOnly showDate);

        Task<List<Showtime>> GetUpcomingShowtimes();
    }
}
